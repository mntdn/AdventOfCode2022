using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AdventCode2022
{
    public class Day12
    {
        public class Square 
        {
            public int Elevation { get; set; }
            public string Letter { get; set; } = "";
            public bool Visited { get; set; } = false;
            public int TentativeDistance { get; set; } = int.MaxValue;
            public Square(char c)
            {
                if (c == 'S')
                    Elevation = 0;
                else if (c == 'E')
                    Elevation = EndElevation;
                else
                    Elevation = c - 96;
                Letter = c.ToString();
            }
        }
        public List<List<Square>> grid = new List<List<Square>>();
        public int pathLength = -1;
        public static int EndElevation = 'z' - 96 + 1;
        public void Try(int l1, int c1, int l2, int c2)
        {
            if (!(l2 < 0 || l2 > grid.Count - 1 || c2 < 0 || c2 > grid[l1].Count - 1) && 
                (grid[l1][c1].Elevation == grid[l2][c2].Elevation + 1 || grid[l1][c1].Elevation == grid[l2][c2].Elevation))
            {
                if (grid[l1][c1].TentativeDistance + 1 < grid[l2][c2].TentativeDistance)
                    grid[l2][c2].TentativeDistance = grid[l1][c1].TentativeDistance + 1;
            }
        }

        public void Dijkstra(int l, int c)
        {
            // haut
            Try(l, c, l - 1, c);
            // droite
            Try(l, c, l, c + 1);
            // bas
            Try(l, c, l + 1, c);
            // gauche
            Try(l, c, l, c - 1);
            grid[l][c].Visited = true;
        }

        public void Solution1()
        {
            string[] lines = File.ReadAllLines("12\\input.txt");
            int result = 0;

            foreach (string line in lines)
                grid.Add(line.Select(l => new Square(l)).ToList());

            bool found = false;
            int l = 0;
            int c = 0;
            for (; l < grid.Count; l++)
            {
                for(; c < grid[l].Count; c++)
                {
                    if (grid[l][c].Elevation == 0)
                    {
                        grid[l][c].TentativeDistance = 0;
                        found = true;
                        break;
                    }
                }
                if (found)
                    break;
                c = 0;
            }

            Dijkstra(l, c);

            result = pathLength;

            Console.WriteLine(result);
        }

        public void Solution2()
        {
        }
    }
}
