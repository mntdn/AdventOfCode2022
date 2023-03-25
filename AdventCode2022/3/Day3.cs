using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCode2022
{
    internal class Day3
    {
        public int CalcValue(char c)
        {
            if (c >= 'a')
                return (int)c - 97 + 1;
            else
                return (int)c - 65 + 27;
        }

        public void Solution()
        {
            string[] lines = File.ReadAllLines("3\\input.txt");

            int finalScore = 0;
            foreach (string l in lines)
            {
                Console.WriteLine(l.Substring(0, l.Length / 2) + Environment.NewLine + l.Substring((l.Length / 2), l.Length / 2));
                List<char> sac1 = l.Substring(0, l.Length / 2).Distinct().ToList();
                List<char> sac2 = l.Substring(l.Length / 2, l.Length / 2).Distinct().ToList();
                finalScore += sac1.Intersect(sac2).Select(s => CalcValue(s)).Sum();
                Console.WriteLine(sac1.Intersect(sac2).ToArray());
            }

            // first
            Console.WriteLine(finalScore);
        }

        public void Solution2()
        {
            string[] lines = File.ReadAllLines("3\\input.txt");

            int finalScore = 0;
            List<List<char>> sacs = new List<List<char>>();
            for (var i = 0; i < lines.Length; i++)
            {
                sacs.Add(lines[i].ToList());
                if(sacs.Count() == 3)
                {
                    var int1 = sacs[0].Intersect(sacs[1]);
                    finalScore += int1.Intersect(sacs[2]).Select(s => CalcValue(s)).First();
                    sacs = new List<List<char>>();
                }
            }

            // first
            Console.WriteLine(finalScore);
        }
    }
}
