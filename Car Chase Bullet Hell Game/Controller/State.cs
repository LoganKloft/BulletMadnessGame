using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Chase_Bullet_Hell_Game.Controller
{
    public interface State
    {
        public void startGame();
        public void playGame();
        public void endGame();
    }
    public class StartState : State
    {
        public void startGame()
        {
            Game1.setState(this);
        }
        public void playGame()
        {
            Game1.setState(new PlayingState());
        }
        public void endGame()
        {
            Game1.setState(new EndState());
        }

    }

    public class PlayingState : State
    {
        public void startGame()
        {
            Game1.setState(new StartState());
        }
        public void playGame()
        {
            Game1.setState(this);
        }
        public void endGame()
        {
            Game1.setState(new EndState());
        }

    }

    public class EndState : State
    {
        public void startGame()
        {
            Game1.setState(new StartState());
        }
        public void playGame()
        {
            Game1.setState(new PlayingState());
        }
        public void endGame()
        {
            Game1.setState(this);
        }

    }
}
