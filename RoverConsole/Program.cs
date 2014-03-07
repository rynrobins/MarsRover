using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MarsRover;


namespace RoverConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string commands = "'F','B','L','R', or 'Q' to quit";
            Console.WriteLine("Hello Rover\r");
            Console.WriteLine("Enter 'Q' to quit.\n");
            Console.WriteLine(string.Format("Accepted commands are: {0}\n", commands));
            Console.WriteLine("Please enter Rover's first command: \r");
            string keyInput = Console.ReadLine();
            
            //init rover
            Rover rover = new Rover();

            rover.PositionHeading = "N";
            rover.PositionX = 0;
            rover.PositionY = 0;

            rover.LandscapeWidth = 15;
            rover.LandscapeHeight = 5;
            rover.BuildLandscapeGrid(true);
            
            while (keyInput != "q" && keyInput != "Q")
            {
                char[] cmds = keyInput.ToCharArray();
                string outputMessage = "";
                while (!rover.ObstacleFound)
                {
                    foreach (char c in cmds)
                    {
                        outputMessage = rover.Command_Parser(c.ToString());
                    }
                    break;
                }
                rover.ObstacleFound = false;
                Console.WriteLine(outputMessage + "\n");
                Console.WriteLine(string.Format("\rRover ready for command ({0}):", commands));
                keyInput = Console.ReadLine();
                                
            }

            Console.WriteLine("\nEXIT");

        }
    }
}
