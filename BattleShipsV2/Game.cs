using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipsV2
{
    public class Game
    {
        public Player Player { get; set; }
        public Player AiPlayer { get; set; }

        public Game()
        {
            Player = new Player("Uncle Bob");
            AiPlayer = new Player("AI");

            //uncomment for multiplayer game -------------------------------------------------------------------------

            //Player.PlaceShips();
            AiPlayer.PlaceShips();

            //uncomment for multiplayer game -------------------------------------------------------------------------

            //Player.OutputBoards();
            AiPlayer.OutputBoard();
        }

        public void PlayRound()
        {
            
  
            var coordinates = Player.FireShot();
            var result = AiPlayer.ProcessShot(coordinates);
            Player.ProcessShotResult(coordinates, result);

            //uncomment for multiplayer game -------------------------------------------------------------------------

            //if (!AiPlayer.HasLost)
            //{
            //    coordinates = AiPlayer.FireShot();
            //    result = Player.ProcessShot(coordinates);
            //    AiPlayer.ProcessShotResult(coordinates, result);
            //}
        }

    }
}
