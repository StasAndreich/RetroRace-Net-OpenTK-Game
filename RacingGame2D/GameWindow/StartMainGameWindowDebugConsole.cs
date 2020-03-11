using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainGameWindow
{
    class StartMainGameWindowDebugConsole
    {
        static void Main(string[] args)
        {
            using (Game game = new Game(800, 600, "Racing Game"))
            {
                game.Run(60f);
            }
        }
    }
}
