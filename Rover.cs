using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover
{
    public class Rover
    {
        List<string> acceptedCommands = new List<string>() {"f", "F", "b", "B", "r", "R", "l", "L" };
        public List<string> _headingDirectionalOrder = new List<string>() { "N", "E", "S", "W" };
        List<string> acceptedDriveCommands = new List<string>() { "f", "F", "b", "B" };
        List<string> acceptedTurnCommands = new List<string>() { "r", "R", "l" ,"L" };
       
        public string _positionHeading;
        public string PositionHeading
        {
            get { return _positionHeading; }
            set 
            {
                _positionHeading = value.ToUpper();
                if (_positionHeadingIndex == null)
                {
                    if (_headingDirectionalOrder.Contains(value.ToUpper()))
                    {
                        _positionHeadingIndex = _headingDirectionalOrder.IndexOf(value.ToUpper());
                    }
                    else
                    {
                        //RR do not set a default heading.
                        //_positionHeadingIndex = 0;
                        throw new IndexOutOfRangeException(string.Format("Heading is not part of the accepted headings. Heading {0}", value));
                    }
                }
            }
        }

        
        public int? _positionHeadingIndex;
        public int? PositionHeadingIndex
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
            set { _position_X = value; }
        }

        public int _position_Y;
        public int PositionY
        {
            get { return _position_Y; }
            set { _position_Y = value; }
        }

        public int _landscapeWidth;
        public int LandscapeWidth 
        {
            get { return _landscapeWidth; }
            set { _landscapeWidth = value - 1; }
        }

        public int _landscapeHeight;
        public int LandscapeHeight
        {
            get { return _landscapeHeight; }
            set { _landscapeHeight = value - 1; }
        }

        public string GetDirectionalHeading()
        {
            try
            {
                if (_positionHeadingIndex != null)
                    return _headingDirectionalOrder[(int)_positionHeadingIndex];
                else
                    throw new NullReferenceException("Directional heading is null.");
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
                            Drive(command);
                            break;
                        case"B":
                            Drive(command);
                            break;
                        case "R":
                            Turn(command);
                            break;
                        case "L":
                            Turn(command);
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

        public void Drive(string command)
        {
            if (acceptedDriveCommands.Contains(command))
            {
                switch (command.ToUpper())
                {
                    case "F":
                        Drive_Forward();
                        break;
                    case "B":
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
                    if (PositionY > _landscapeHeight)
                    {
                        PositionY = 0;
                    }
                    break;
                case "E":
                    _position_X++;
                    if (PositionX > _landscapeWidth)
                    {
                        PositionX = 0;
                    }
                    break;
                case "S":
                    _position_Y--;
                    if (_position_Y < 0)
                    {
                        _position_Y = _landscapeHeight;
                    }
                    break;
                case "W":
                    _position_X--;
                    if (_position_X < 0)
                    {
                        _position_X = _landscapeWidth;
                    }
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
                    if (_position_Y < 0)
                    {
                        _position_Y = _landscapeHeight;
                    }
                    break;
                case "E":
                    _position_X--;
                    if (_position_X < 0)
                    {
                        _position_X = _landscapeWidth;
                    }
                    break;
                case "S":
                    _position_Y++;
                    if (_position_Y > _landscapeHeight)
                    {
                        _position_Y = 0;
                    }
                    break;
                case "W":
                    _position_X++;
                    if (_position_X > _landscapeWidth)
                    {
                        _position_X = 0;
                    }
                    break;
                default:
                    //todo throw exception
                    break;
            }
        }

        public void Turn(string command)
        {
            if (acceptedTurnCommands.Contains(command))
            {
                switch (command.ToUpper())
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
        }

        public void Turn_Left()
        {
            _positionHeadingIndex--;
            if(_positionHeadingIndex < 0)
            {
                //treat the directional order like a loop and when it drops below set it the Last position in the array
                _positionHeadingIndex = _headingDirectionalOrder.Count - 1;
            }
         }

        


    }

   
}
