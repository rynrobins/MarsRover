using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover
{
    public class Landscape
    {
        Rover _rover = new Rover();
        public Landscape(Rover rover)
        {
            _rover = rover;
        }

        public void BuildLandscapeGrid(bool buildWithRandomObstacles)
        {
            _rover.gridCoordinates = new List<Coordinates>();

            for (int i = 0; i <= _rover._landscapeWidth; i++)
            {
                for (int ii = 0; ii <= _rover._landscapeHeight; ii++)
                {
                    Coordinates crd = new Coordinates();
                    crd.xCoordinate = i;
                    crd.yCoordinate = ii;
                    //for now set random obstacles
                    if (buildWithRandomObstacles)
                    {
                        Random random = new Random();
                        if (random.Next(11) > 4)
                        {
                            crd.containsObstacle = true;
                        }
                        else
                        {
                            crd.containsObstacle = false;
                        }
                    }
                    else
                    {
                        crd.containsObstacle = false;
                    }

                    _rover.gridCoordinates.Add(crd);
                }
            }

            //make sure there is at least one obstacle if set to random
            if(buildWithRandomObstacles)
            {
                Coordinates _setCoordinate = new Coordinates
                {
                    xCoordinate = 1,
                    yCoordinate = 0,
                    containsObstacle = true
                };
                Coordinates coor = (from Coordinates in _rover.gridCoordinates
                                    where Coordinates.xCoordinate == 1 && Coordinates.yCoordinate == 0
                                    select Coordinates).FirstOrDefault<Coordinates>();
                int indexOf = -1;
                if (coor != null)
                {
                    indexOf = _rover.gridCoordinates.IndexOf(coor);
                }
                if(indexOf >= 0)
                {
                    _rover.gridCoordinates[indexOf].containsObstacle = true;
                }
            }
        }
    }
}
