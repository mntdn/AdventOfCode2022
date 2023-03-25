using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCode2022
{
    internal class Day2
    {
        public enum RPS
        {
            Rock = 1,
            Paper = 2,
            Scissors = 3
        }

        public RPS StringToRPS(string a, Boolean isSecond)
        {
            string final = a;
            if(isSecond)
                final = a == "Y" ? "B" : (a == "X" ? "A" : "C");
            return final == "A" ? RPS.Rock :(final == "B" ? RPS.Paper : RPS.Scissors);
        }

        public bool Win(RPS a, RPS b)
        {
            return (a == RPS.Rock && b == RPS.Scissors) || (a == RPS.Scissors && b == RPS.Paper) || (a == RPS.Paper && b == RPS.Rock);
        }

        public void Solution()
        {
            string[] lines = File.ReadAllLines("2\\input.txt");

            int finalScore = 0;
            foreach (string l in lines)
            {
                var jeu = l.Split(" ");
                RPS a = StringToRPS(jeu[0], false);
                RPS b = StringToRPS(jeu[1], true);
                if (a == b)
                    finalScore += (int)b + 3;
                else if (Win(b, a))
                    finalScore += (int)b + 6;
                else
                    finalScore += (int)b;
            }

            // first
            Console.WriteLine(finalScore);
        }

        public void Solution2()
        {
            string[] lines = File.ReadAllLines("2\\input.txt");

            int finalScore = 0;
            foreach (string l in lines)
            {
                var jeu = l.Split(" ");
                RPS a = StringToRPS(jeu[0], false);
                RPS b = RPS.Rock;
                int toAdd = 0;
                if (jeu[1] == "X")
                    // losing
                    b = a == RPS.Rock ? RPS.Scissors : (a == RPS.Scissors ? RPS.Paper : RPS.Rock);
                else if (jeu[1] == "Y")
                {
                    // equal
                    toAdd = 3;
                    b = a;
                }
                else
                {
                    // win
                    toAdd = 6;
                    b = a == RPS.Rock ? RPS.Paper : (a == RPS.Scissors ? RPS.Rock : RPS.Scissors);
                }
                finalScore += (int)b + toAdd;
            }

            // first
            Console.WriteLine(finalScore);
        }
    }
}
