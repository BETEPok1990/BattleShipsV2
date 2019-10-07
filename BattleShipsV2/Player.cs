using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShipsV2.Board;
using BattleShipsV2.Helpers;
using BattleShipsV2.Ships;
using System.Text.RegularExpressions;

namespace BattleShipsV2
{
    public class Player
    {
        public string Name { get; }
        private GameBoard GameBoard;
        private RealBoard RealBoard;
        private List<Ship> Ships;
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
            for (byte row = 1; row <= 10; row++)
            {
                //uncomment for multiplayer game -------------------------------------------------------------------------

                //for (int ownColumn = 1; ownColumn <= 10; ownColumn++)
                //{
                //    Console.Write(GameBoard.Cells.At(row, ownColumn).Status + " ");
                //}
                //Console.Write("                ");


                for (byte realColumn = 1; realColumn <= 10; realColumn++)
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
                    byte startcolumn = (byte)rand.Next(1, 11);
                    byte startrow = (byte)rand.Next(1, 11);
                    byte endrow = startrow;
                    byte endcolumn = startcolumn;
                    byte orientation = (byte)(rand.Next(1, 101) % 2);

                    if (orientation == 0)
                    {
                        for (byte i = 1; i < ship.Width; i++)
                        {
                            endrow++;
                        }
                    }
                    else
                    {
                        for (byte i = 1; i < ship.Width; i++)
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
            string inputValue = null; 
            var pattern = new Regex(@"^[A-J][1-9]0?$");
            
            do
            {
                Console.WriteLine($@"{Name} please enter coordinates in right format (letter from A to J and number from 1 to 10. exp: B10)!");
                inputValue = Console.ReadLine();
            } while (!pattern.IsMatch(inputValue));

            Dictionary<char, byte> dict = new Dictionary<char, byte>()
            {
                {'A', 1},
                {'B', 2},
                {'C', 3},
                {'D', 4},
                {'E', 5},
                {'F', 6},
                {'G', 7},
                {'H', 8},
                {'I', 9},
                {'J', 10}
            };
            Coordinates coords = new Coordinates(
                (byte)(inputValue[1]-48),
                dict[inputValue[0]]
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
