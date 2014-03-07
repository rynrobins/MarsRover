using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MarsRover;
using System.Linq;

namespace UnitTestMarsRover
{
    [TestClass]
    public class RoverTest
    {       
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void Test_Rover_Only_Accepts_Vaild_Commands()
        {
            Rover rover = new Rover();

            rover.Command_Receiver("GO");
        }


        [TestMethod]
        public void Test_Rovers_Position_From_Multiple_Drive_Commands()
        {
            Rover rover = new Rover();
            rover.PositionHeading = "N";
            rover.PositionY = 0;
            rover.PositionX = 0;

            rover.LandscapeHeight = 2;
            rover.LandscapeWidth = 2;
            Landscape landscape = new Landscape(rover);
            landscape.BuildLandscapeGrid(false);

            string[] commands = new string[] { "F", "F", "F" };

            foreach (string s in commands)
            {
                rover.Command_Receiver(s);
            }

            Assert.AreEqual(1, rover.PositionY);
            Assert.AreEqual(0, rover.PositionX);
        }

        [TestMethod]
        public void Test_Rovers_Position_From_Multiple_Drive_And_Turn_Commands()
        {
            Rover rover = new Rover();
            rover.PositionHeading = "W";
            rover.PositionY = 0;
            rover.PositionX = 3;

            rover.LandscapeHeight = 4;
            rover.LandscapeWidth = 4;
            Landscape landscape = new Landscape(rover);
            landscape.BuildLandscapeGrid(false);

            string[] commands = new string[] { "F", "F","R", "F", "F" };

            foreach (string s in commands)
            {
                rover.Command_Receiver(s);
            }

            Assert.AreEqual(2, rover.PositionY);
            Assert.AreEqual(1, rover.PositionX);
        }

        [TestMethod]
        public void Test_Rovers_Position_From_Multiple_Reverse_Commands()
        {
            Rover rover = new Rover();
            rover.PositionHeading = "N";
            rover.PositionY = 1;
            rover.PositionX = 1;

            rover.LandscapeHeight = 3;
            rover.LandscapeWidth = 3;
            Landscape landscape = new Landscape(rover);
            landscape.BuildLandscapeGrid(false);

            string[] commands = new string[] { "B", "B", "B", "L", "F" };

            foreach (string s in commands)
            {
                rover.Command_Receiver(s);
            }

            Assert.AreEqual(1, rover.PositionY);
            Assert.AreEqual(0, rover.PositionX);
        }

        [TestMethod]
        public void Test_Wrapping_On_3_X_3_Grid()
        {
            Rover rover = new Rover();
            rover.PositionHeading = "E";
            rover.PositionY = 0;
            rover.PositionX = 0;

            rover.LandscapeHeight = 3;
            rover.LandscapeWidth = 3;
            Landscape landscape = new Landscape(rover);
            landscape.BuildLandscapeGrid(false);

            string[] commands = new string[] { "F", "F", "F", "F" };

            foreach (string s in commands)
            {
                rover.Command_Receiver(s);
                //rover.Drive_Forward();
            }

            Assert.AreEqual(0, rover.PositionY);
            Assert.AreEqual(1, rover.PositionX);

        }
        //[TestMethod]
        ////[ExpectedException(typeof(Exception))]
        //public void Test_Wrapping_On_3_X_3_Grid_Catch_Exception()
        //{
        //    Rover rover = new Rover();
        //    rover.PositionHeading = "E";
        //    rover.PositionY = 0;
        //    rover.PositionX = 0;

        //    rover.LandscapeHeight = 3;
        //    rover.LandscapeWidth = 3;
        //    Landscape landscape = new Landscape(rover);
        //    landscape.BuildLandscapeGrid(false);

        //    string[] commands = new string[] { "F", "F", "F", "F" };

        //    foreach (string s in commands)
        //    {
        //        rover.Command_Receiver(s);
        //        //rover.Drive_Forward();
        //    }           

        //}       

        [TestMethod]
        public void Test_For_Obstacle_Detection()
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

            //set obstacle
            Coordinates crd = new Coordinates{
                 
                 xCoordinate = 1,
                 yCoordinate =0
            };
            Coordinates coor = (from Coordinates in rover.gridCoordinates
                                where Coordinates.xCoordinate == 1 && Coordinates.yCoordinate == 0
                                select Coordinates).FirstOrDefault<Coordinates>();
            bool notNull = coor != null;
            int indexOf = -1;
            if(coor != null)
            {
                indexOf = rover.gridCoordinates.IndexOf(coor);
            }
           
            
            //int indexOf = rover.gridCoordinates.IndexOf(crd);
            bool hasIndex = indexOf > 0;
            if (hasIndex)
            {
                rover.gridCoordinates[indexOf].containsObstacle = true;
            }

            string rtnMessage = "";
            char[] cmds = ("f").ToCharArray();
            while (!rover.ReportObstacle)
            {
                foreach (char c in cmds)
                {
                    rtnMessage = rover.Command_Parser(c.ToString());
                }
                //break;
            }

            Assert.AreEqual(true, rtnMessage.Contains("Obst"));
            
        }
       

    }
}
