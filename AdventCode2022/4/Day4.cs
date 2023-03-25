using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventCode2022
{
    internal class Day4
    {

        public void Solution()
        {
            string[] lines = File.ReadAllLines("4\\input.txt");

            int finalScore = 0;
            Regex r = new Regex(@"([0-9]+)\-([0-9]+),([0-9]+)\-([0-9]+)");
            foreach (string l in lines)
            {
                Match m = r.Match(l);
                if (m.Success){
                    int[] a= new int[4];
                    for(int i = 0; i < 4; i++)
                        a[i] = int.Parse(m.Groups[i+1].Value);
                    if ((a[0] >= a[2] && a[1] <= a[3]) || (a[2] >= a[0] && a[3] <= a[1]))
                        finalScore++;
                }
            }

            // first
            Console.WriteLine(finalScore);
        }

        public void Solution2()
        {
            string[] lines = File.ReadAllLines("4\\input.txt");

            int finalScore = 0;
            Regex r = new Regex(@"([0-9]+)\-([0-9]+),([0-9]+)\-([0-9]+)");
            foreach (string l in lines)
            {
                Match m = r.Match(l);
                if (m.Success){
                    int[] a= new int[4];
                    for(int i = 0; i < 4; i++)
                        a[i] = int.Parse(m.Groups[i+1].Value);
                    if (!(a[1] < a[2] || a[3] < a[0]))
                        finalScore++;
                }
            }

            // first
            Console.WriteLine(finalScore);
        }
    }
}
