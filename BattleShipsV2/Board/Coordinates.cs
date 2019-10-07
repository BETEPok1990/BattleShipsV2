using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipsV2.Board
{
    public struct Coordinates
    {
        public byte Row { get; }
        public byte Column { get; }

        public Coordinates(byte row, byte column)
        {
            Row = row;
            Column = column;
        }
    }
}
