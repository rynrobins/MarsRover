using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover
{
    public class Turn
    {
        Rover _rover = new Rover();
        public Turn(Rover rover)
        {
            _rover = rover;            
        }

        public void Turn_Commands(string command)
        {
            if (_rover.acceptedTurnCommands.Contains(command))
            {
                switch (command.ToUpper())
                {
                    case "R":
                        Turn_Right();
                        break;
                    case "L":
                        Turn_Left();
                        break;
                    default:
                        //todo throw exception
                        return;
                }
            }
            else
            {
                string msg = string.Format("Rover turn does not have a process for command: {0}", command);
                _rover.ReturnMessage = msg;
                throw new Exception(msg);
            }

        }

        private void Turn_Right()
        {
            _rover._positionHeadingIndex++;

            if (_rover._positionHeadingIndex > _rover._headingDirectionalOrder.Count - 1)
            {
                //treat the directional order like a loop and reset when it hits the end
                _rover._positionHeadingIndex = 0;
            }
        }

        private void Turn_Left()
        {
            _rover._positionHeadingIndex--;
            if (_rover._positionHeadingIndex < 0)
            {
                //treat the directional order like a loop and when it drops below set it the Last position in the array
                _rover._positionHeadingIndex = _rover._headingDirectionalOrder.Count - 1;
            }
        }
    }
}
