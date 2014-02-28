using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover
{
    public class Rover
    {
        List<string> acceptedCommands = new List<string>() {"f", "F", "b", "B", "n", "N", "e", "E", "s", "S", "w", "W" };
        public List<string> _headingDirectionalOrder = new List<string>() { "N", "E", "S", "W" };
       
        public string _positionHeading;
        public string PositionHeading
        {
            get { return _positionHeading; }
            set 
            {
                _positionHeading = value;
                if (_positionHeadingIndex == null)
                {
                    if (_headingDirectionalOrder.Contains(value))
                    {
                        _positionHeadingIndex = _headingDirectionalOrder.IndexOf(value);
                    }
                    else
                    {
                        _positionHeadingIndex = 0;
                    }
                }
            }
        }

        
        public int _positionHeadingIndex;
        public int PositionHeadingIndex
        {
            get { return _positionHeadingIndex; }

            set
            {
                if (value < _headingDirectionalOrder.Count)
                {
                    _positionHeadingIndex = value;
                }
                else
                {
                    throw new IndexOutOfRangeException("Cannont set Rover in the positional direction because it is out of the accepted range.");
                }
            }
        }

        public int _position_X;
        public int PositionX
        {
            get { return _position_X; }
            set { value = _position_X; }
        }

        public int _position_Y;
        public int PositionY
        {
            get { return _position_Y; }
            set { value = _position_Y; }
        }
        
        public string GetDirectionalHeading()
        {
            try
            {
                return _headingDirectionalOrder[_positionHeadingIndex];
            }
            catch (IndexOutOfRangeException ioore)
            {
                throw new IndexOutOfRangeException("Direction is not int the range of accepted positions", ioore);
               
            }
        }


        public void Command_Receiver(string command)
        {
            if (acceptedCommands.Contains(command))
            {
                try
                {
                    switch(command.ToUpper())
                    {
                        case "F":
                            Drive _driveF = new Drive(command.ToUpper());
                            break;
                        case"B":
                            Drive _driveB = new Drive(command.ToUpper());
                            break;
                        case "R":
                            Turn _turnR = new Turn(command.ToUpper());
                            break;
                        case "L":
                            Turn _turnL = new Turn(command.ToUpper());
                            break;
                        default:
                            throw new Exception(string.Format("Command receiver does not have a process for command: {0}", command));
                    }
                }
                catch
                {
                    throw new Exception(string.Format("Command receiver invalid command: {0}", command));
                }
            }
            else
            {
                throw new IndexOutOfRangeException("Command is not part of the accepted commands.");
            }

        }
    }

    public class Drive : Rover
    {
        List<string> acceptedDriveCommands = new List<string>() { "f", "F", "b", "B" };
        public Drive(string command)
        {
            if (acceptedDriveCommands.Contains(command))
            {
                switch (command)
                {
                    case "F":
                        Drive_Forward();
                        break;
                    case "D":
                        Drive_Reverse();
                        break;
                    default:
                        //todo throw exception
                        return;
                }
            }
            else
            {
                throw new Exception(string.Format("Rover drive does not have a process for command: {0}", command));
            }
        }
        public void Drive_Forward()
        {            
            switch (GetDirectionalHeading())
            {
                case "N":
                    _position_Y++;
                    break;
                case "E":
                    _position_X++;
                    break;
                case "S":
                    _position_Y--;
                    break;
                case "W":
                    _position_X--;
                    break;
                default:
                   
                    break;
            }
        }

        public void Drive_Reverse()
        {
            switch (GetDirectionalHeading())
            {
                case "N":
                    _position_Y--;
                    break;
                case "E":
                    _position_X--;
                    break;
                case "S":
                    _position_Y++;
                    break;
                case "W":
                    _position_X++;
                    break;
                default:
                    //todo throw exception
                    break;
            }
        }

    }

    public class Turn : Rover
    {
        List<string> acceptedTurnCommands = new List<string>() { "n", "N", "e", "E", "s", "S", "w", "W" };
              
        public Turn(string command)
        {
            if (acceptedTurnCommands.Contains(command))
            {
                switch (command)
                {
                    case "R":
                        Turn_Right();
                        break;
                    case "L":
                        Turn_Left();
                        break;
                    default:
                        //todo throw exception
                        return;
                }
            }
            else 
            {
                throw new Exception(string.Format("Rover turn does not have a process for command: {0}", command));
            }

        }
        public void Turn_Right()
        {
            _positionHeadingIndex++;

            if(_positionHeadingIndex > _headingDirectionalOrder.Count -1)
            {
                //treat the directional order like a loop and reset when it hits the end
                _positionHeadingIndex = 0;
            }



            //switch (_positionHeadingIndex)
            //{
            //    case "N":
            //        _positionHeadingIndex = "E";
            //        break;
            //    case "E":
            //        _positionHeadingIndex = "S";
            //        break;
            //    case "S":
            //        _positionHeadingIndex = "W";
            //        break;
            //    case "W":
            //        _positionHeadingIndex = "N";
            //        break;
            //    default:
            //        //todo throw exception
            //        break;
            //}
        }

        public void Turn_Left()
        {
            _positionHeadingIndex--;
            if(_positionHeadingIndex < 0)
            {
                //treat the directional order like a loop and when it drops below set it the Last position in the array
                _positionHeadingIndex = _headingDirectionalOrder.Count - 1;
            }
            //switch (_positionHeadingIndex)
            //{
            //    case "N":
            //        _positionHeadingIndex = "W";
            //        break;
            //    case "E":
            //        _positionHeadingIndex = "N";
            //        break;
            //    case "S":
            //        _positionHeadingIndex = "E";
            //        break;
            //    case "W":
            //        _positionHeadingIndex = "S";
            //        break;
            //    default:
            //        //todo throw exception
            //        break;
            //}
        }
    }
}
