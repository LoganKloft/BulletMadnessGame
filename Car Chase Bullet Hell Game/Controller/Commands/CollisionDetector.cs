using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Chase_Bullet_Hell_Game.Controller.Commands
{
    internal class CollisionDetector
    {
        private List<Command> _commands = new List<Command>();

        public void AddCommand(Command command)
        {
            _commands.Add(command);
        }

        public void DetectCollisions()
        {
            foreach (Command command in _commands)
            {
                command.execute();
            }
        }
    }
}
