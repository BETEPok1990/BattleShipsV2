using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShipsV2.Helpers;

namespace BattleShipsV2.Board
{
    public class RealBoard : GameBoard
    {
        public List<Coordinates> GetOpenRandomCells()
        {
            return Cells
            .Where(x => x.OccupationType == OccupationType.Empty && x.IsRandomAvailable)
            .Select(x => x.Coordinates)
            .ToList();
        }

        public List<Coordinates> GetHitNeighbors()
        {
            List<Cell> panels = new List<Cell>();
            var hits = Cells.Where(x => x.OccupationType == OccupationType.Hit);
            
            foreach (var hit in hits)
            {
                panels.AddRange(GetNeighbors(hit.Coordinates).ToList());
            }

            return panels
            .Distinct()
            .Where(x => x.OccupationType == OccupationType.Empty)
            .Select(x => x.Coordinates)
            .ToList();
        }

        public List<Cell> GetNeighbors(Coordinates coordinates)
        {
            byte one = 1;
            byte ten = 10;
            byte row = coordinates.Row;
            byte column = coordinates.Column;
            List<Cell> cells = new List<Cell>();
            if (column > one)
            {
                cells.Add(Cells.At(row, column--));
            }
            if (row > one)
            {
                cells.Add(Cells.At(row--, column));
            }
            if (row < ten)
            {
                cells.Add(Cells.At(row++, column));
            }
            if (column < ten)
            {
                cells.Add(Cells.At(row, column++));
            }
            return cells;
        }
    }
}

