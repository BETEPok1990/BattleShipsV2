using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipsV2.Board
{
    /// <summary>
    /// Represents a collection of Cells.
    /// </summary>
    public class GameBoard
    {
        public List<Cell> Cells { get; set; }

        public GameBoard()
        {
            Cells = new List<Cell>();
            for (int i = 1; i <= 10; i++)
            {
                for (int k = 1; k <= 10; k++)
                {
                    Cells.Add(new Cell(i, k));
                }
            }
        }
    }
}
