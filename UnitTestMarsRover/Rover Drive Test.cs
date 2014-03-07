using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MarsRover;
using System.Linq;

namespace UnitTestMarsRover
{
    [TestClass]
    public class Rover_Drive_Test
    {
        [TestMethod]
        public void Test_Rover_Drives_North_One_Space()
        {
            Rover rover = new Rover();
            rover.PositionHeading = "N";
            rover.PositionX = 0;
            rover.PositionY = 0;

            rover.LandscapeWidth = 2;
            rover.LandscapeHeight = 2;
            Landscape landscape = new Landscape(rover);
            landscape.BuildLandscapeGrid(false);

            rover.Command_Receiver("f");
            //rover.Drive_Forward();
            Assert.AreEqual(1, rover.PositionY);
        }


        [TestMethod]
        public void Test_Rover_Drives_East_One_Space()
        {
            Rover rover = new Rover();
            rover.PositionHeading = "E";
            rover.PositionX = 1;
            rover.PositionY = 0;
            rover.LandscapeWidth = 3;
            rover.LandscapeHeight = 3;
            Landscape landscape = new Landscape(rover);
            landscape.BuildLandscapeGrid(false);

            rover.Command_Receiver("F");

            Assert.AreEqual(2, rover.PositionX);
        }

        [TestMethod]
        public void Test_Rover_Drives_South_One_Space()
        {
            Rover rover = new Rover();
            rover.PositionHeading = "S";
            rover.PositionX = 0;
            rover.PositionY = 0;

            rover.LandscapeWidth = 2;
            rover.LandscapeHeight = 2;
            Landscape landscape = new Landscape(rover);
            landscape.BuildLandscapeGrid(false);

            rover.Command_Receiver("F");

            Assert.AreEqual(1, rover.PositionY);
        }

        [TestMethod]
        public void Test_Rover_Drives_West_One_Space()
        {
            Rover rover = new Rover();
            rover.PositionHeading = "W";
            rover.PositionX = 0;
            rover.PositionY = 0;

            rover.LandscapeWidth = 2;
            rover.LandscapeHeight = 2;
            Landscape landscape = new Landscape(rover);
            landscape.BuildLandscapeGrid(false);

            rover.Command_Receiver("F");

            Assert.AreEqual(1, rover.PositionX);
        }

        [TestMethod]
        public void Test_Rover_Reverses_North_One_Space()
        {
            Rover rover = new Rover();
            rover.PositionHeading = "S";
            rover.PositionX = 0;
            rover.PositionY = 0;

            rover.LandscapeWidth = 2;
            rover.LandscapeHeight = 2;
            Landscape landscape = new Landscape(rover);
            landscape.BuildLandscapeGrid(false);

            rover.Command_Receiver("b");

            Assert.AreEqual(1, rover.PositionY);
        }

        [TestMethod]
        public void Test_Rover_Reverses_East_One_Space()
        {
            Rover rover = new Rover();
            rover.PositionHeading = "W";
            rover.PositionX = 0;
            rover.PositionY = 0;

            rover.LandscapeWidth = 10;
            rover.LandscapeHeight = 2;
            Landscape landscape = new Landscape(rover);
            landscape.BuildLandscapeGrid(false);

            rover.Command_Receiver("B");

            Assert.AreEqual(1, rover.PositionX);
        }

        [TestMethod]
        public void Test_Rover_Reverses_South_One_Space()
        {
            Rover rover = new Rover();
            rover.PositionHeading = "N";
            rover.PositionX = 0;
            rover.PositionY = 2;

            rover.LandscapeWidth = 3;
            rover.LandscapeHeight = 3;
            Landscape landscape = new Landscape(rover);
            landscape.BuildLandscapeGrid(false);

            rover.Command_Receiver("B");

            Assert.AreEqual(1, rover.PositionY);
        }

        [TestMethod]
        public void Test_Rover_Reverses_West_One_Space()
        {
            Rover rover = new Rover();
            rover.PositionHeading = "E";
            rover.PositionX = 0;
            rover.PositionY = 0;

            rover.LandscapeWidth = 10;
            rover.LandscapeHeight = 2;
            Landscape landscape = new Landscape(rover);
            landscape.BuildLandscapeGrid(false);

            rover.Command_Receiver("b");

            Assert.AreEqual(9, rover.PositionX);
        }

    }
}
