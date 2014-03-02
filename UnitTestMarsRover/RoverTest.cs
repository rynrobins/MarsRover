using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MarsRover;

namespace UnitTestMarsRover
{
    [TestClass]
    public class RoverTest
    {
        [TestMethod]
        public void Test_Get_Rover_Directional_Heading()
        {
            Rover rover = new Rover();
            rover.PositionHeading = "N";
            string headingReturned;

            headingReturned = rover.GetDirectionalHeading();

            Assert.AreEqual("N", headingReturned);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]        
        public void Test_Give_Rover_A_Non_Directional_Heading()
        {
            Rover rover = new Rover();
            rover.PositionHeading = "%";
            string headingReturned;

            headingReturned = rover.GetDirectionalHeading();

            Assert.AreEqual(null, headingReturned);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void Test_Rover_Only_Accepts_Vaild_Commands()
        {
            Rover rover = new Rover();

            rover.Command_Receiver("GO");
        }

        [TestMethod]
        public void Test_Rover_Drives_North_One_Space()
        {
            Rover rover = new Rover();
            rover.PositionHeading = "N";
            rover.PositionX = 0;
            rover.PositionY = 0;

            rover.Command_Receiver("F");

            Assert.AreEqual(1, rover._position_Y);
        }

        
        [TestMethod]
        public void Test_Rover_Drives_East_One_Space()
        {
            Rover rover = new Rover();
            rover.PositionHeading = "E";
            rover.PositionX = 0;
            rover.PositionY = 0;

            rover.Command_Receiver("F");

            Assert.AreEqual(1, rover._position_X);
        }

        [TestMethod]
        public void Test_Rover_Drives_South_One_Space()
        {
            Rover rover = new Rover();
            rover.PositionHeading = "S";
            rover.PositionX = 0;
            rover.PositionY = 0;

            rover.Command_Receiver("F");

            Assert.AreEqual(-1, rover._position_Y);
        }

        [TestMethod]
        public void Test_Rover_Drives_West_One_Space()
        {
            Rover rover = new Rover();
            rover.PositionHeading = "W";
            rover.PositionX = 0;
            rover.PositionY = 0;

            rover.Command_Receiver("F");

            Assert.AreEqual(-1, rover._position_X);
        }

        [TestMethod]
        public void Test_Rover_Reverses_North_One_Space()
        {
            Rover rover = new Rover();
            rover.PositionHeading = "S";
            rover.PositionX = 0;
            rover.PositionY = 0;

            rover.Command_Receiver("b");

            Assert.AreEqual(1, rover._position_Y);
        }

        [TestMethod]
        public void Test_Rover_Reverses_East_One_Space()
        {
            Rover rover = new Rover();
            rover.PositionHeading = "W";
            rover.PositionX = 0;
            rover.PositionY = 0;

            rover.Command_Receiver("B");

            Assert.AreEqual(1, rover._position_X);
        }

        [TestMethod]
        public void Test_Rover_Reverses_South_One_Space()
        {
            Rover rover = new Rover();
            rover.PositionHeading = "N";
            rover.PositionX = 0;
            rover.PositionY = 0;

            rover.Command_Receiver("B");

            Assert.AreEqual(-1, rover._position_Y);
        }

        [TestMethod]
        public void Test_Rover_Reverses_West_One_Space()
        {
            Rover rover = new Rover();
            rover.PositionHeading = "E";
            rover.PositionX = 0;
            rover.PositionY = 0;

            rover.Command_Receiver("b");

            Assert.AreEqual(-1, rover._position_X);
        }

        [TestMethod]
        public void Test_Rover_Turns_North()
        {
            Rover rover = new Rover();
            rover.PositionHeading = "W";

            rover.Command_Receiver("R");

            Assert.AreEqual("N", rover.GetDirectionalHeading());
        }

        [TestMethod]
        public void Test_Rover_Turns_East()
        {
            Rover rover = new Rover();
            rover.PositionHeading = "n";

            rover.Command_Receiver("R");

            Assert.AreEqual("E", rover.GetDirectionalHeading());
        }

        [TestMethod]
        public void Test_Rover_Turns_South()
        {
            Rover rover = new Rover();
            rover.PositionHeading = "E";

            rover.Command_Receiver("R");

            Assert.AreEqual("S", rover.GetDirectionalHeading());
        }

        [TestMethod]
        public void Test_Rover_Turns_West()
        {
            Rover rover = new Rover();
            rover.PositionHeading = "S";

            rover.Command_Receiver("R");

            Assert.AreEqual("W", rover.GetDirectionalHeading());
        }

        [TestMethod]
        public void Test_Rover_Left_Turn_North()
        {
            Rover rover = new Rover();
            rover.PositionHeading = "E";

            rover.Command_Receiver("L");

            Assert.AreEqual("N", rover.GetDirectionalHeading());
        }

        [TestMethod]
        public void Test_Rover_Left_Turn_East()
        {
            Rover rover = new Rover();
            rover.PositionHeading = "s";

            rover.Command_Receiver("L");

            Assert.AreEqual("E", rover.GetDirectionalHeading());
        }

        [TestMethod]
        public void Test_Rover_Left_Turn_South()
        {
            Rover rover = new Rover();
            rover.PositionHeading = "W";

            rover.Command_Receiver("L");

            Assert.AreEqual("S", rover.GetDirectionalHeading());
        }

        [TestMethod]
        public void Test_Rover_Left_Turn_West_()
        {
            Rover rover = new Rover();
            rover.PositionHeading = "N";

            rover.Command_Receiver("L");

            Assert.AreEqual("W", rover.GetDirectionalHeading());
        }

        [TestMethod]
        public void Test_Rovers_Position_From_Multiple_Drive_Commands()
        {
            Rover rover = new Rover();
            rover.PositionHeading = "N";
            rover.PositionY = 0;
            rover.PositionX = 0;

            string[] commands = new string[] { "F", "F", "F" };

            foreach (string s in commands)
            {
                rover.Command_Receiver(s);
            }

            Assert.AreEqual(3, rover.PositionY);
            Assert.AreEqual(0, rover.PositionX);
        }

        [TestMethod]
        public void Test_Rovers_Position_From_Multiple_Drive_And_Turn_Commands()
        {
            Rover rover = new Rover();
            rover.PositionHeading = "N";
            rover.PositionY = 0;
            rover.PositionX = 0;

            string[] commands = new string[] { "F", "F","R", "F", "F" };

            foreach (string s in commands)
            {
                rover.Command_Receiver(s);
            }

            Assert.AreEqual(2, rover.PositionY);
            Assert.AreEqual(2, rover.PositionX);
        }

        [TestMethod]
        public void Test_Rovers_Position_From_Multiple_Reverse_Commands()
        {
            Rover rover = new Rover();
            rover.PositionHeading = "N";
            rover.PositionY = 0;
            rover.PositionX = 0;

            string[] commands = new string[] { "B", "B", "B", "L", "F" };

            foreach (string s in commands)
            {
                rover.Command_Receiver(s);
            }

            Assert.AreEqual(-3, rover.PositionY);
            Assert.AreEqual(-1, rover.PositionX);
        }

        [TestMethod]
        public void Test_Wrapping_On_3_X_3_Grid()
        {
            Rover rover = new Rover();
            rover.PositionHeading = "N";
            rover.PositionY = 0;
            rover.PositionX = 0;

            rover.LandscapeHeight = 3;
            rover.LandscapeWidth = 3;

            string[] commands = new string[] { "B", "B", "B", "L", "F" };

            foreach (string s in commands)
            {
                rover.Command_Receiver(s);
            }

            Assert.AreEqual(0, rover.PositionY);
            Assert.AreEqual(3, rover.PositionX);

        }

    }
}
