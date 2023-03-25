using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventCode2022
{
    internal class Day6
    {

        public void Solution()
        {
            string[] lines = File.ReadAllLines("6\\input.txt");

            int result = 0;
            foreach (string l in lines)
            {
                Queue<char> q = new Queue<char>();
                for(int i = 0; i < l.Length; i++)
                {
                    q.Enqueue(l[i]);
                    if (q.Count > 4)
                        q.Dequeue();
                    if(q.Count == 4)
                    {
                        HashSet<char> h = q.ToHashSet();
                        if(h.Distinct().Count() == 4)
                        {
                            result = i + 1;
                            break;
                        }
                    }
                }
                Console.WriteLine(result);
            }
        }

        public void Solution2()
        {
            string[] lines = File.ReadAllLines("6\\input.txt");

            int result = 0;
            foreach (string l in lines)
            {
                Queue<char> q = new Queue<char>();
                for (int i = 0; i < l.Length; i++)
                {
                    q.Enqueue(l[i]);
                    if (q.Count > 14)
                        q.Dequeue();
                    if (q.Count == 14)
                    {
                        HashSet<char> h = q.ToHashSet();
                        if (h.Distinct().Count() == 14)
                        {
                            result = i + 1;
                            break;
                        }
                    }
                }
                Console.WriteLine(result);
            }
        }
    }
}
