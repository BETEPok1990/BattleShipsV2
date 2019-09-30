using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipsV2
{
    class Program
    {
        static void Main(string[] args)
        {

                Game game = new Game();

                do
                {
                    game.PlayRound();
                    game.Player.OutputBoard();
                }
                while (!game.AiPlayer.HasLost);
                
            Console.WriteLine($"{game.Player.Name} has won!!");
            Console.ReadLine();
        }
    }
}
