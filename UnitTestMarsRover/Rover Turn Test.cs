using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MarsRover;
using System.Linq;

namespace UnitTestMarsRover
{
    [TestClass]
    public class Rover_Turn_Test
    {
        [TestMethod]
        public void Test_Rover_Turns_North()
        {
            Rover rover = new Rover();
            rover.PositionHeading = "W";
            Navigation navigation = new Navigation(rover);

            rover.Command_Parser("R");

            Assert.AreEqual("N", navigation.GetDirectionalHeading());
        }

        [TestMethod]
        public void Test_Rover_Turns_East()
        {
            Rover rover = new Rover();
            rover.PositionHeading = "n";
            Navigation navigation = new Navigation(rover);

            rover.Command_Parser("R");

            Assert.AreEqual("E", navigation.GetDirectionalHeading());
        }

        [TestMethod]
        public void Test_Rover_Turns_South()
        {
            Rover rover = new Rover();
            rover.PositionHeading = "E";
            Navigation navigation = new Navigation(rover);

            rover.Command_Receiver("R");

            Assert.AreEqual("S", navigation.GetDirectionalHeading());
        }

        [TestMethod]
        public void Test_Rover_Turns_West()
        {
            Rover rover = new Rover();
            rover.PositionHeading = "S";
            Navigation navigation = new Navigation(rover);

            rover.Command_Receiver("R");

            Assert.AreEqual("W", navigation.GetDirectionalHeading());
        }

        [TestMethod]
        public void Test_Rover_Left_Turn_North()
        {
            Rover rover = new Rover();
            rover.PositionHeading = "E";
            Navigation navigation = new Navigation(rover);

            rover.Command_Receiver("L");

            Assert.AreEqual("N", navigation.GetDirectionalHeading());
        }

        [TestMethod]
        public void Test_Rover_Left_Turn_East()
        {
            Rover rover = new Rover();
            rover.PositionHeading = "s";
            Navigation navigation = new Navigation(rover);

            rover.Command_Receiver("L");

            Assert.AreEqual("E", navigation.GetDirectionalHeading());
        }

        [TestMethod]
        public void Test_Rover_Left_Turn_South()
        {
            Rover rover = new Rover();
            rover.PositionHeading = "W";
            Navigation navigation = new Navigation(rover);

            rover.Command_Receiver("L");

            Assert.AreEqual("S", navigation.GetDirectionalHeading());
        }

        [TestMethod]
        public void Test_Rover_Left_Turn_West_()
        {
            Rover rover = new Rover();
            rover.PositionHeading = "N";
            Navigation navigation = new Navigation(rover);

            rover.Command_Receiver("L");

            Assert.AreEqual("W", navigation.GetDirectionalHeading());
        }
    }
}
