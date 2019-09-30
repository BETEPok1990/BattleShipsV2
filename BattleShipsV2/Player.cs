using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShipsV2.Board;
using BattleShipsV2.Helpers;
using BattleShipsV2.Ships;

namespace BattleShipsV2
{
    public class Player
    {
        public string Name { get; set; }
        public GameBoard GameBoard { get; set; }
        public RealBoard RealBoard { get; set; }
        public List<Ship> Ships { get; set; }
        public bool HasLost
        {
            get
            {
                return Ships.All(x => x.IsSunk);
            }
        }

        public Player(string name)
        {
            Name = name;
            Ships = new List<Ship>()
            {
                new Battleship(),
                new Destroyer(),
                new Destroyer(),

            };
            GameBoard = new GameBoard();
            RealBoard = new RealBoard();
        }

        public void OutputBoard()
        {
            Console.WriteLine(Name);


            //uncomment for multiplayer game -------------------------------------------------------------------------

            //Console.WriteLine("Own Board:                          Real Board:");
            Console.WriteLine("Real Board:");
            for (int row = 1; row <= 10; row++)
            {
                //uncomment for multiplayer game -------------------------------------------------------------------------

                //for (int ownColumn = 1; ownColumn <= 10; ownColumn++)
                //{
                //    Console.Write(GameBoard.Cells.At(row, ownColumn).Status + " ");
                //}
                //Console.Write("                ");


                for (int realColumn = 1; realColumn <= 10; realColumn++)
                {
                    Console.Write(RealBoard.Cells.At(row, realColumn).Status + " ");
                }
                Console.WriteLine(Environment.NewLine);
            }
            Console.WriteLine(Environment.NewLine);
        }

        public void PlaceShips()
        {
            
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            foreach (var ship in Ships)
            {

                bool ifOccupied = true;
                while (ifOccupied)
                {
                    var startcolumn = rand.Next(1, 11);
                    var startrow = rand.Next(1, 11);
                    int endrow = startrow, endcolumn = startcolumn;
                    var orientation = rand.Next(1, 101) % 2;

                    if (orientation == 0)
                    {
                        for (int i = 1; i < ship.Width; i++)
                        {
                            endrow++;
                        }
                    }
                    else
                    {
                        for (int i = 1; i < ship.Width; i++)
                        {
                            endcolumn++;
                        }
                    }

                    if (endrow > 10 || endcolumn > 10)
                    {
                        ifOccupied = true;
                        continue;
                    }

                    var cellToCheck = GameBoard.Cells.Range(startrow, startcolumn, endrow, endcolumn);
                    if (cellToCheck.Any(x => x.IsOccupied))
                    {
                        ifOccupied = true;
                        continue;
                    }

                    foreach (var cell in cellToCheck)
                    {
                        cell.OccupationType = ship.OccupationType;
                    }
                    ifOccupied = false;
                }
            }
        }

        public Coordinates FireShot()
        {
            Console.WriteLine($"{Name} please enter coordinates!");
            var inputValue = Console.ReadLine();
            
            Dictionary<string, int> dict = new Dictionary<string, int>()
            {
                {"A", 1}, {"B", 2}, {"C", 3}, {"D", 4}, {"E", 5}, {"F", 6}, {"G", 7}, {"H", 8}, {"I", 9}, {"J", 10}
            };
            Coordinates coords = new Coordinates(
                Convert.ToInt32(inputValue.ToCharArray()[1]) - 48,
                dict.FirstOrDefault(x => x.Key == inputValue.ToCharArray()[0].ToString()).Value
            );

            Console.WriteLine($"{Name} Firing shot at {coords.Column} , {coords.Row} !");
            return coords;
        }


       

        public ShotResult ProcessShot(Coordinates coords)
        {
            var cell = GameBoard.Cells.At(coords.Row, coords.Column);
            if (!cell.IsOccupied)
            {
                Console.WriteLine($"{Name} says: Miss!");
                return ShotResult.Miss;
            }
            var ship = Ships.First(x => x.OccupationType == cell.OccupationType);
            ship.Hits++;
            Console.WriteLine($"{Name} says: Hit!");
            if (ship.IsSunk)
            {
                Console.WriteLine($"{Name} says: You sunk my {ship.Name}!");
            }
            return ShotResult.Hit;
        }

        public void ProcessShotResult(Coordinates coords, ShotResult result)
        {
            var panel = RealBoard.Cells.At(coords.Row, coords.Column);
            switch (result)
            {
                case ShotResult.Hit:
                    panel.OccupationType = OccupationType.Hit;
                    break;

                default:
                    panel.OccupationType = OccupationType.Miss;
                    break;
            }
        }
    }
}
