
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCode2022
{
    internal class Day1
    {
        public void Solution()
        {
            string[] lines = File.ReadAllLines("1\\input.txt");

            List<int> elves = new List<int>();
            int curElf = 0;
            foreach (string l in lines)
            {
                if (int.TryParse(l, out int _res))
                    curElf += _res;
                else
                {
                    elves.Add(curElf);
                    curElf = 0;
                }
            }

            // first
            Console.WriteLine(elves.Max());

            // second
            Console.WriteLine(elves.OrderByDescending(e => e).Take(3).Sum());
        }
    }
}
