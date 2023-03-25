using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Chase_Bullet_Hell_Game.Controller.Commands
{
    internal abstract class Command
    {
        public delegate void DestroyCommandEventHandler(Command command);
        public abstract event DestroyCommandEventHandler DestroyEvent;

        public abstract void execute();
    }
}
