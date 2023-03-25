using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Chase_Bullet_Hell_Game.Controller.Commands
{
    internal class CollisionDetector
    {
        private static List<Command> _commands = new List<Command>();

        public static void AddCommand(Command command)
        {
            _commands.Add(command);
            command.DestroyEvent += DestroyCommandEventHandler;
        }

        public static void RemoveCommand(Command command)
        {
            command.DestroyEvent -= DestroyCommandEventHandler;
            _commands.Remove(command);
        }

        public static void DestroyCommandEventHandler(Command command)
        {
            RemoveCommand(command);
        }

        public static void DetectCollisions()
        {
            for (int i = 0; i < _commands.Count; i++)
            {
                int count = _commands.Count;
                Command command = _commands[i];
                command.execute();
                if (_commands.Count < count) i--;
            }
        }
    }
}
