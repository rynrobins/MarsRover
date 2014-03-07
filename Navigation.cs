using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover
{
    public class Navigation
    {
        Rover _rover = new Rover();

        public Navigation(Rover rover)
        {
            _rover = rover;
        }

        public bool IsDestinationFreeFromObstacle(int xPos, int yPos)
        {
            //try
            //{

            Coordinates coor = (from Coordinates in _rover.gridCoordinates
                                where Coordinates.xCoordinate == xPos && Coordinates.yCoordinate == yPos
                                select Coordinates).FirstOrDefault<Coordinates>();
            if (coor.containsObstacle)
            {
                //report obstacle
                _rover.ReturnMessage = string.Format("Obstacle found at coordinate: {0},{1}, current position: {2},{3}, heading: {4}", xPos, yPos, _rover.PositionX, _rover.PositionY, GetDirectionalHeading());
                _rover.ObstacleFound = true;
                return false;
            }
            else
            {
                return true;
            }
            //}
            //catch
            //{
            //    return true;
            //}
        }

        public string GetDirectionalHeading()
        {
            try
            {
                if (_rover._positionHeadingIndex != null)
                {
                    _rover.PositionHeading = _rover._headingDirectionalOrder[(int)_rover._positionHeadingIndex];
                    return _rover.PositionHeading;
                }
                else
                {
                    throw new NullReferenceException("Directional heading is null.");
                }
            }
            catch (IndexOutOfRangeException ioore)
            {
                string msg = string.Format("Direction is not int the range of accepted positions");
                _rover.ReturnMessage = msg;
                throw new IndexOutOfRangeException(msg, ioore);

            }
        }

    }
}
