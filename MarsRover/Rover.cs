using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover
{
    public class Rover
    {
        public List<string> acceptedCommands = new List<string>() {"f", "F", "b", "B", "r", "R", "l", "L" };
        public List<string> _headingDirectionalOrder = new List<string>() { "N", "E", "S", "W" };
        public List<string> acceptedDriveCommands = new List<string>() { "f", "F", "b", "B" };
        public List<string> acceptedTurnCommands = new List<string>() { "r", "R", "l" ,"L" };
        public List<Coordinates> gridCoordinates = new List<Coordinates>();
       
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
                        string msg = string.Format("Heading is not part of the accepted headings. Heading {0}", value);
                        ReturnMessage = msg;
                        throw new IndexOutOfRangeException(msg);
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
                    string msg = string.Format("Cannont set Rover in the positional direction because it is out of the accepted range.");
                    ReturnMessage = msg;
                    throw new IndexOutOfRangeException(msg);
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

        public object[] _fullLandscapeGrid;
        public object[] FullLandscapeGrid
        {
            get { return _fullLandscapeGrid; }
            set { _fullLandscapeGrid = value; }
        }

        public string _returnMessage = "";
        public string ReturnMessage
        {
            get { return _returnMessage; }
            set { _returnMessage = value; }
        }

        private bool _obstacleFound;
        public bool ObstacleFound
        {
            get { return _obstacleFound; }
            set { _obstacleFound = value; }
        }

        private bool _reportObstacle;
        public bool ReportObstacle
        {
            get { return _reportObstacle; }
            set { _reportObstacle = value; }
        }


       

        public string Command_Parser(string command)
        {
            try
            {
                Command_Receiver(command);
                
            }
            catch
            {
                //return error message
                return ReturnMessage;
                
            }
            //return coordinates and heading
            if(ObstacleFound)
            {
                ReportObstacle = true;
                return ReturnMessage;
            }
            Navigation navigation = new Navigation(this);
            return string.Format("Rover's current position is: coordinate({0},{1}), heading {2}", PositionX, PositionY, navigation.GetDirectionalHeading());
        }

        public void Command_Receiver(string command)
        {
            if (acceptedCommands.Contains(command))
            {
                try
                {
                    Drive _driveRover = new Drive(this);
                    Turn _turnRover = new Turn(this);
                    switch(command.ToUpper())
                    {
                        case "F":
                            _driveRover.Drive_Commands(command);
                            break;
                        case"B":
                            _driveRover.Drive_Commands(command);
                            break;
                        case "R":
                            _turnRover.Turn_Commands(command);
                            break;
                        case "L":
                            _turnRover.Turn_Commands(command);
                            break;
                        default:
                            string msg = string.Format("Command receiver does not have a process for command: {0}", command);
                            ReturnMessage = msg;
                            throw new Exception(msg);
                    }
                }
                catch
                {
                    string msg = string.Format("Command receiver error with command: {0}", command);
                    ReturnMessage = msg;
                    throw new Exception(msg);
                }
            }
            else
            {
                string msg = string.Format("Command is not part of the accepted commands.");
                ReturnMessage = msg;
                throw new IndexOutOfRangeException(msg);
            }

        }

       
       

       
        
      
    }


}
