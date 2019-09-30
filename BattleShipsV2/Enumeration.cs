using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipsV2
{
    
        public enum OccupationType
        {
            [Description("o")]
            Empty,

            [Description("B")]
            Battleship,

            [Description("D")]
            Destroyer,

            [Description("X")]
            Hit,

            [Description("M")]
            Miss
        }

        public enum ShotResult
        {
            Miss,
            Hit
        }
    
}
