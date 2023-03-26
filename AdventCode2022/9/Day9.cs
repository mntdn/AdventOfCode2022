using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AdventCode2022
{
    public class Day9
    {
        public class Position
        {
            public int X { get; set; }
            public int Y { get; set; }
            public Position(int x, int y)
            {
                X = x; Y = y;
            }
            public string P()
            {
                return $"{X},{Y}";
            }
        }
        public class Snake
        {
            public HashSet<string> Positions { get; set; }
            public List<Position> Tail { get; set; }
            public List<Position> OldPos { get; set; }
            public int Length { get; set; }

            public Snake(int l)
            {
                // on a visité la position 0,0 par défaut
                Positions = new HashSet<string>() { "0,0" };
                Length = l;
                Tail = new List<Position>();
                OldPos = new List<Position>();
                for (int i = 0; i < Length; i++)
                {
                    Tail.Add(new Position(0, 0));
                    OldPos.Add(new Position(0, 0));
                }
            }

            public void Move(string dir, int nb)
            {
                for (int i = 0; i < nb; i++)
                {
                    OldPos[0].X = Tail[0].X;
                    OldPos[0].Y = Tail[0].Y;
                    if (dir == "U") Tail[0].Y++;
                    else if (dir == "D") Tail[0].Y--;
                    else if (dir == "L") Tail[0].X--;
                    else if (dir == "R") Tail[0].X++;
                    MoveTail();
                }
            }
            private void MoveTail()
            {
                for (int i = 1; i < Length; i++)
                {
                    int diffX = Tail[i].X - Tail[i - 1].X;
                    int diffY = Tail[i].Y - Tail[i - 1].Y;
                    if (Math.Abs(diffX) >= 2 || Math.Abs(diffY) >= 2)
                    {
                        OldPos[i].X = Tail[i].X;
                        OldPos[i].Y = Tail[i].Y;

                        if (Tail[i].X != Tail[i - 1].X && Tail[i].Y != Tail[i - 1].Y)
                        {
                            // dans ce cas là mvmt en diagonale pour se rapprocher
                            if (Math.Abs(diffX) >= 2 && Math.Abs(diffY) < 2)
                            {
                                Tail[i].Y = Tail[i - 1].Y;
                                Tail[i].X -= diffX / Math.Abs(diffX);
                            }
                            else if (Math.Abs(diffY) >= 2 && Math.Abs(diffX) < 2)
                            {
                                Tail[i].X = Tail[i - 1].X;
                                Tail[i].Y -= diffY / Math.Abs(diffY);
                            }
                            else
                            {
                                Tail[i].X -= diffX / Math.Abs(diffX);
                                Tail[i].Y -= diffY / Math.Abs(diffY);
                            }
                        }
                        else if (Tail[i].X != Tail[i - 1].X)
                        {
                            Tail[i].X -= diffX / Math.Abs(diffX);
                        }
                        else 
                        { 
                            Tail[i].Y -= diffY / Math.Abs(diffY);
                        }
                        if (i == Length - 1 && !Positions.Contains(Tail[i].P()))
                            Positions.Add(Tail[i].P());
                    }
                }
            }
            public void Print()
            {
                int XMax = Math.Max(5, Tail.Select(t => Math.Abs(t.X)).Max() + 1);
                int YMax = Math.Max(5, Tail.Select(t => Math.Abs(t.Y)).Max() + 1);
                for (int y = YMax; y > -1 * YMax; y--)
                {
                    for (int x = -1 * XMax; x < XMax; x++)
                    {
                        int pos = -1;
                        for (int i = 0; i < Length; i++)
                        {
                            if (Tail[i].X == x && Tail[i].Y == y)
                            {
                                pos = i;
                                break;
                            }
                        }

                        Console.ForegroundColor = ConsoleColor.White;
                        if(x == 0 && y == 0)
                            Console.ForegroundColor = ConsoleColor.Red;

                        Console.Write(pos == -1 ? "." : pos);
                    }
                    Console.WriteLine();
                }
            }
        }

        public void Solution1()
        {
            string[] lines = File.ReadAllLines("9\\input.txt");
            int result = 0;
            Snake s = new Snake(2);

            foreach (string line in lines)
            {
                Console.WriteLine($"== {line} ==");
                string[] cmd = line.Split(" ");
                s.Move(cmd[0], int.Parse(cmd[1]));
                s.Print();
            }

            result = s.Positions.Count();
            Console.WriteLine(result);
        }

        public void Solution2()
        {
            string[] lines = File.ReadAllLines("9\\input.txt");
            int result = 0;
            Snake s = new Snake(10);

            foreach (string line in lines)
            {
                //Console.WriteLine($"== {line} ==");
                string[] cmd = line.Split(" ");
                s.Move(cmd[0], int.Parse(cmd[1]));
                //s.Print();
            }

            result = s.Positions.Count();
            Console.WriteLine(result);
        }
    }
}
