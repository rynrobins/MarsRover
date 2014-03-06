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
                        //RR do not set a default heading.
                        //_positionHeadingIndex = 0;
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

        public string _returnMessage;
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


        public string GetDirectionalHeading()
        {
            try
            {
                if (_positionHeadingIndex != null)
                {
                    PositionHeading = _headingDirectionalOrder[(int)_positionHeadingIndex];
                    return PositionHeading;
                }
                else
                {
                    throw new NullReferenceException("Directional heading is null.");
                }
            }
            catch (IndexOutOfRangeException ioore)
            {
                string msg = string.Format("Direction is not int the range of accepted positions");
                ReturnMessage = msg;
                throw new IndexOutOfRangeException(msg, ioore);
               
            }
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
                return ReturnMessage;
            }
            return string.Format("Rover's current position is: coordinate({0},{1}), heading {2}", PositionX, PositionY, GetDirectionalHeading());
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
                            string msg = string.Format("Command receiver does not have a process for command: {0}", command);
                            ReturnMessage = msg;
                            throw new Exception(msg);
                    }
                }
                catch
                {
                    string msg = string.Format("Command receiver invalid command: {0}", command);
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
                string msg = string.Format("Rover drive does not have a process for command: {0}", command);
                ReturnMessage = msg;
                throw new Exception(msg);
            }
        }
        public void Drive_Forward()
        {
            try
            {
                switch (GetDirectionalHeading())
                {
                    case "N":
                        int testCoordinate_N = (_position_Y + 1) > _landscapeHeight ? 0 : _position_Y + 1;
                        if (IsDestinationFreeFromObstacle(_position_X, testCoordinate_N))
                        {
                            _position_Y++;
                        }
                        if (PositionY > _landscapeHeight)
                        {
                            PositionY = 0;
                        }
                        break;
                    case "E":
                        int testCoordinate_E = (_position_X + 1) > LandscapeWidth ? 0 : _position_X + 1;
                        if (IsDestinationFreeFromObstacle(testCoordinate_E, _position_Y))
                        {
                            _position_X++;
                        }
                        if (PositionX > _landscapeWidth)
                        {
                            PositionX = 0;
                        }
                        break;
                    case "S":
                        int testCoordinate_S = (_position_Y - 1 < 0) ? _landscapeHeight : _position_Y - 1;
                        if (IsDestinationFreeFromObstacle(_position_X, testCoordinate_S))
                        {
                            _position_Y--;
                        }
                        if (_position_Y < 0)
                        {
                            _position_Y = _landscapeHeight;
                        }
                        break;
                    case "W":
                        int testCoordinate_X = (_position_X - 1 < 0) ? _landscapeWidth : _position_X - 1;
                        if (IsDestinationFreeFromObstacle(testCoordinate_X, _position_Y))
                        {
                            _position_X--;
                        }
                        if (_position_X < 0)
                        {
                            _position_X = _landscapeWidth;
                        }
                        break;
                    default:

                        break;
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Drive_Forward error", ex);
            }
        }

        public void Drive_Reverse()
        {
            int testCoordinate;
            switch (GetDirectionalHeading())
            {
                case "N":
                    testCoordinate = _position_Y - 1 < 0 ? _landscapeHeight : _position_Y - 1;
                    if (IsDestinationFreeFromObstacle(_position_X, testCoordinate))
                    {
                        _position_Y--;
                    }                  
                    if (_position_Y < 0)
                    {
                        _position_Y = _landscapeHeight;
                    }
                    break;
                case "E":
                    testCoordinate = _position_X - 1 < 0 ? _landscapeWidth : _position_X - 1;
                    if (IsDestinationFreeFromObstacle(testCoordinate, _position_Y))
                    {
                        _position_X--;
                    }
                    if (_position_X < 0)
                    {
                        _position_X = _landscapeWidth;
                    }
                    break;
                case "S":
                    testCoordinate = _position_Y + 1 > _landscapeHeight ? 0 : _position_Y + 1;
                    if (IsDestinationFreeFromObstacle(_position_X, testCoordinate))
                    {
                        _position_Y++;
                    }
                    if (_position_Y > _landscapeHeight)
                    {
                        _position_Y = 0;
                    }
                    break;
                case "W":
                    testCoordinate = _position_X + 1 > _landscapeWidth ? 0 : _position_X + 1;
                    if (IsDestinationFreeFromObstacle(testCoordinate, _position_Y))
                    {
                        _position_X++;
                    }
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
                string msg = string.Format("Rover turn does not have a process for command: {0}", command);
                ReturnMessage = msg;
                throw new Exception(msg);
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

        public void BuildLandscapeGrid(bool buildWithRandomObstacles)
        {
            gridCoordinates = new List<Coordinates>();

            for (int i = 0; i < _landscapeWidth; i++)
            {
                for (int ii = 0; ii < _landscapeHeight; ii++)
                {
                    Coordinates crd = new Coordinates();
                    crd.xCoordinate = i;
                    crd.yCoordinate = ii;
                    //for now set random obstacles
                    if (buildWithRandomObstacles)
                    {
                        Random random = new Random();
                        if (random.Next(11) > 3)
                        {
                            crd.containsObstacle = true;
                        }
                        else
                        {
                            crd.containsObstacle = false;//todo set obstacles
                        }
                    }
                    
                    gridCoordinates.Add(crd);
                }                
            }
        }

        public bool IsDestinationFreeFromObstacle(int xPos, int yPos)
        {
            try
            {

                Coordinates coor = (from Coordinates in gridCoordinates
                                    where Coordinates.xCoordinate == xPos && Coordinates.yCoordinate == yPos
                                    select Coordinates).FirstOrDefault<Coordinates>();
                if (coor.containsObstacle)
                {
                    //report obstacle
                    ReturnMessage = string.Format("Obstacle found at coordinate: ({0},{1}), current position: ({2},{3}), heading: {4}", xPos, yPos, PositionX, PositionY, PositionHeading);
                    ObstacleFound = true;
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                return true;
            }
        }
        
        //execute alert
        public void ReportAnObstacle()
        {
            //System.Diagnostics.Process.Start(@"cscript //B //Nologo c:\scripts\vbscript.vbs");
        }
    }

   public class Coordinates
   {
       public int xCoordinate { get; set; }
       public int yCoordinate { get; set; }
       public bool containsObstacle { get; set; }
   }
}
