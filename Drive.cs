using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover
{
    public class Drive
    {
         Rover _rover = new Rover();
         public Drive(Rover rover)
         {
             _rover = rover;
         }

        public void Drive_Commands(string command)
        {
            if (_rover.acceptedDriveCommands.Contains(command))
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
                _rover.ReturnMessage = msg;
                throw new Exception(msg);
            }
        }
        public void Drive_Forward()
        {
            Navigation _navigation = new Navigation(_rover);
            try
            {
                switch (_navigation.GetDirectionalHeading())
                {
                    case "N":
                        int testCoordinate_N = _rover.PositionY + 1;
                        if (testCoordinate_N > _rover._landscapeHeight)
                        {
                            testCoordinate_N = 0;
                        }
                        if (_navigation.IsDestinationFreeFromObstacle(_rover.PositionX, testCoordinate_N))
                        {
                            _rover.PositionY++;
                        }
                        if (_rover.PositionY > _rover._landscapeHeight)
                        {
                            _rover.PositionY = 0;
                        }
                        break;
                    case "E":
                        int testCoordinate_E = _rover.PositionX + 1;
                        if (testCoordinate_E > _rover._landscapeWidth)
                        {
                            testCoordinate_E = 0;
                        }
                        if (_navigation.IsDestinationFreeFromObstacle(testCoordinate_E, _rover.PositionY))
                        {
                            _rover.PositionX++;
                        }
                        if (_rover.PositionX > _rover._landscapeWidth)
                        {
                            _rover.PositionX = 0;
                        }
                        break;
                    case "S":
                        int testCoordinate_S = _rover.PositionY - 1;
                        if (testCoordinate_S < 0)
                        {
                            testCoordinate_S = _rover._landscapeHeight;
                        }
                        if (_navigation.IsDestinationFreeFromObstacle(_rover.PositionX, testCoordinate_S))
                        {
                            _rover.PositionY--;
                        }
                        if (_rover.PositionY < 0)
                        {
                            _rover.PositionY = _rover._landscapeHeight;
                        }
                        break;
                    case "W":
                        int testCoordinate_W = _rover.PositionX - 1;
                        if (testCoordinate_W < 0)
                        {
                            testCoordinate_W = _rover._landscapeWidth;
                        }
                        if (_navigation.IsDestinationFreeFromObstacle(testCoordinate_W, _rover.PositionY))
                        {
                            _rover.PositionX--;
                        }
                        if (_rover.PositionX < 0)
                        {
                            _rover.PositionX = _rover._landscapeWidth;
                        }
                        break;
                    default:

                        break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Drive_Forward error", ex);
            }
        }

        public void Drive_Reverse()
        {
            Navigation _navigation = new Navigation(_rover);
            switch (_navigation.GetDirectionalHeading())
            {
                case "N":
                    int testCoordinate_N = _rover.PositionY - 1;
                    if (testCoordinate_N < 0)
                    {
                        testCoordinate_N = _rover._landscapeHeight;
                    }
                    if (_navigation.IsDestinationFreeFromObstacle(_rover.PositionX, testCoordinate_N))
                    {
                        _rover.PositionY--;
                    }
                    if (_rover.PositionY < 0)
                    {
                        _rover.PositionY = _rover._landscapeHeight;
                    }
                    break;
                case "E":
                    int testCoordinate_E = _rover.PositionX - 1;
                    if (testCoordinate_E < 0)
                    {
                        testCoordinate_E = _rover._landscapeWidth;
                    }
                    if (_navigation.IsDestinationFreeFromObstacle(testCoordinate_E, _rover.PositionY))
                    {
                        _rover.PositionX--;
                    }
                    if (_rover.PositionX < 0)
                    {
                        _rover.PositionX = _rover._landscapeWidth;
                    }
                    break;
                case "S":
                    int testCoordinate_S = _rover.PositionY + 1;
                    if (testCoordinate_S > _rover._landscapeHeight)
                    {
                        testCoordinate_S = 0;
                    }
                    if (_navigation.IsDestinationFreeFromObstacle(_rover._position_X, testCoordinate_S))
                    {
                        _rover._position_Y++;
                    }
                    if (_rover._position_Y > _rover._landscapeHeight)
                    {
                        _rover._position_Y = 0;
                    }
                    break;
                case "W":
                    int testCoordinate_W = _rover._position_X + 1;
                    if (testCoordinate_W > _rover._landscapeWidth)
                    {
                        testCoordinate_W = 0;
                    }
                    if (_navigation.IsDestinationFreeFromObstacle(testCoordinate_W, _rover.PositionY))
                    {
                        _rover.PositionX++;
                    }
                    if (_rover.PositionX > _rover._landscapeWidth)
                    {
                        _rover.PositionX = 0;
                    }
                    break;
                default:
                    //todo throw exception
                    break;
            }
        }

    }

}
