using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MarsRover;
using System.Linq;

namespace UnitTestMarsRover
{
    [TestClass]
    public class Rover_Landscape_Test
    {
        [TestMethod]
        public void Test_Get_Set_The_Grid()
        {
            Rover rover = new Rover();
            rover.PositionHeading = "E";
            rover.PositionY = 4;
            rover.PositionX = 3;

            rover.LandscapeHeight = 3;
            rover.LandscapeWidth = 3;

            Assert.AreEqual(4, rover.PositionY);
            Assert.AreEqual(3, rover.PositionX);
        }

        [TestMethod]
        public void Test_Confirm_Grid_Has_Obstacles()
        {
            Rover rover = new Rover();
            rover.PositionHeading = "E";
            rover.PositionY = 0;
            rover.PositionX = 0;

            rover.LandscapeHeight = 5;
            rover.LandscapeWidth = 5;
            Landscape landscape = new Landscape(rover);
            landscape.BuildLandscapeGrid(true);
            bool containsObstacle = false;

            foreach (Coordinates c in rover.gridCoordinates)
            {
                if (c.containsObstacle)
                {
                    containsObstacle = true;
                }
            }

            Assert.AreEqual(true, containsObstacle);
        }

        [TestMethod]
        public void Test_Check_Grid_Is_Creating_The_Correct_Number_Of_Spaces()
        {
            Rover rover = new Rover();
            rover.PositionHeading = "E";
            Navigation navigation = new Navigation(rover);
            string headingIndex = navigation.GetDirectionalHeading();
            rover.PositionY = 0;
            rover.PositionX = 0;

            rover.LandscapeHeight = 5;
            rover.LandscapeWidth = 5;
            Landscape landscape = new Landscape(rover);
            landscape.BuildLandscapeGrid(false);

            Assert.AreEqual(25, rover.gridCoordinates.Count());
        }
    }
}
