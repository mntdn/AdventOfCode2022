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
            public HashSet<string> Positions { get; set; }
            public int X { get; set; }
            public int Y { get; set; }
            public int Xt { get; set; }
            public int Yt { get; set; }
            public Tuple<int, int> OldPos { get; set; }

            public Position()
            {
                X = 0; Y = 0;
                Xt = 0; Yt = 0;
                // on a visité la position 0,0 par défaut
                Positions = new HashSet<string>() { "0,0" };
                OldPos = new Tuple<int, int>(0, 0);
            }

            public void Move(string dir, int nb)
            {
                for (int i = 0; i < nb; i++)
                {
                    OldPos = new Tuple<int, int>(X, Y);
                    if(dir == "U") Y++;
                    else if(dir == "D") Y--;
                    else if(dir == "L") X--;
                    else if(dir == "R") X++;
                    MoveTail();
                }
            }
            public void Up(int nb)
            {
                for (int i = 0; i < nb; i++)
                {
                    OldPos = new Tuple<int, int>(X, Y);
                    Y++;
                    MoveTail();
                }
            }
            public void Down(int nb)
            {
                for (int i = 0; i < nb; i++)
                {
                    OldPos = new Tuple<int, int>(X, Y);
                    Y--;
                    MoveTail();
                }
            }
            public void Left(int nb)
            {
                for (int i = 0; i < nb; i++)
                {
                    OldPos = new Tuple<int, int>(X, Y);
                    X--;
                    MoveTail();
                }
            }
            public void Right(int nb)
            {
                for (int i = 0; i < nb; i++)
                {
                    OldPos = new Tuple<int, int>(X, Y);
                    X++;
                    MoveTail();
                }
            }
            private void MoveTail()
            {
                if (Math.Abs(Xt - X) >= 2 || Math.Abs(Yt - Y) >= 2)
                {
                    Xt = OldPos.Item1;
                    Yt = OldPos.Item2;
                    if(!Positions.Contains($"{Xt},{Yt}"))
                        Positions.Add($"{Xt},{Yt}");
                    // if (Yt == Y)
                    // {
                    //     // même ligne donc on bouge X dans la bonne direction
                    //     Xt -= (Xt - X) / Math.Abs(Xt - X);
                    // }
                    // else if (Xt == X)
                    // {
                    //     // même colonne donc on bouge Y dans la bonne direction
                    //     Yt -= (Yt - Y) / Math.Abs(Yt - Y);
                    // }
                    // else
                    // {
                    //     // on doit bouger en diagonale
                    //     if (Math.Abs(Xt - X) > Math.Abs(Yt - Y))
                    //     {
                    //         // on doit bouger 
                    //     }
                    // }
                }
            }
        }

        public void Solution1()
        {
            string[] lines = File.ReadAllLines("9\\input.txt");
            int result = 0;
            Position p = new Position();

            foreach (string line in lines)
            {
                string[] cmd = line.Split(" ");
                p.Move(cmd[0], int.Parse(cmd[1]));
            }

            result = p.Positions.Count();
            Console.WriteLine(result);
        }

        public void Solution2()
        {
        }
    }
}
