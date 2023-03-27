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
    public class Day10
    {
        public void Solution1()
        {
            string[] lines = File.ReadAllLines("10\\input.txt");
            int result = 0;
            int curCycle = 0;
            int curValue = 1;
            List<int> execQueue = new List<int>();
            Queue<int> cyclesToLook = new Queue<int>();
            for (int i = 20; i <= 220; i += 40)
                cyclesToLook.Enqueue(i);

            foreach (string l in lines)
            {
                string[] cmd = l.Split(" ");

                if (cmd[0] == "addx")
                {
                    execQueue.Add(0);
                    execQueue.Add(int.Parse(cmd[1]));
                }
                else
                    execQueue.Add(0);
            }

            while (cyclesToLook.Count > 0)
            {
                curValue += execQueue[curCycle++];

                int cycleToLook = cyclesToLook.Peek();
                if (cycleToLook == curCycle + 1)
                {
                    Console.WriteLine($"{cycleToLook}, {curValue}");
                    result += cycleToLook * curValue;
                    cyclesToLook.Dequeue();
                }
            }

            Console.WriteLine(result);
        }

        public void Solution2()
        {
            string[] lines = File.ReadAllLines("10\\input.txt");
            int curCycle = 0;
            int curValue = 1;
            List<int> execQueue = new List<int>();
            string finalRender = "";

            foreach (string l in lines)
            {
                string[] cmd = l.Split(" ");

                if (cmd[0] == "addx")
                {
                    execQueue.Add(0);
                    execQueue.Add(int.Parse(cmd[1]));
                }
                else
                    execQueue.Add(0);
            }

            while (curCycle < execQueue.Count)
            {
                curValue += execQueue[curCycle];
                finalRender += (curValue - 1 <= ((curCycle % 40) + 1) && ((curCycle % 40) + 1) <= curValue + 1) ? "#" : ".";
                curCycle++;
                if (((curCycle % 40) + 1) == 1)
                    finalRender += Environment.NewLine;
            }

            Console.WriteLine(finalRender);
        }
    }
}
