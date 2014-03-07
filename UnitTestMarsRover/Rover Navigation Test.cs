using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MarsRover;
using System.Linq;


namespace UnitTestMarsRover
{
    [TestClass]
    public class Rover_Navigation_Test
    {
        [TestMethod]
        public void Test_Get_Rover_Directional_Heading()
        {
            Rover rover = new Rover();
            rover.PositionHeading = "N";
            string headingReturned;
            Navigation navigation = new Navigation(rover);
            headingReturned = navigation.GetDirectionalHeading();

            Assert.AreEqual("N", headingReturned);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void Test_Give_Rover_A_Non_Directional_Heading()
        {
            Rover rover = new Rover();
            rover.PositionHeading = "%";
            string headingReturned;

            Navigation navigation = new Navigation(rover);
            headingReturned = navigation.GetDirectionalHeading();

            Assert.AreEqual(null, headingReturned);
        }
    }
}
