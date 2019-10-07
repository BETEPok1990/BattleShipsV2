using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShipsV2.Board;

namespace BattleShipsV2.Helpers
{
    public static class CellHelper
    {
        public static Cell At(this List<Cell> cells, byte row, byte column)
        {
            return cells.First(x => x.Coordinates.Row == row && x.Coordinates.Column == column);
        }

        public static List<Cell> Range(this List<Cell> cells, byte startRow, byte startColumn, byte endRow, byte endColumn)
        {
            return cells.Where(x => x.Coordinates.Row >= startRow
                                     && x.Coordinates.Column >= startColumn
                                     && x.Coordinates.Row <= endRow
                                     && x.Coordinates.Column <= endColumn).ToList();
        }
    }
}
