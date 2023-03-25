using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventCode2022
{
    internal class Day5
    {

        public void Solution()
        {
            string[] lines = File.ReadAllLines("5\\input.txt");

            int finalScore = 0;
            Regex r = new Regex(@"\[([A-Z])\]");
            Regex rMove = new Regex(@"move ([0-9]+) from ([0-9]+) to ([0-9]+)");
            bool queueMode = true;
            List<string>[]? crates = null;
            Stack<string>[]? finalCrates = null;
            foreach (string l in lines)
            {
                if (queueMode)
                {
                    int nbCrates = (l.Length + 1) / 4;
                    if (crates == null)
                    {
                        crates = new List<string>[nbCrates];
                        for (var j = 0; j < nbCrates; j++)
                            crates[j] = new List<string>();
                    }
                    for (int i = 0; i < nbCrates * 4; i += 4)
                    {
                        string val = l.Substring(i, i == (nbCrates - 1) * 4 ? 3 : 4);
                        if (string.IsNullOrWhiteSpace(val.Trim()))
                            continue;
                        var m = r.Match(val.Trim());
                        if (m.Success)
                            crates[i / 4].Add(m.Groups[1].Value);
                    }
                }
                else
                {
                    var m = rMove.Match(l);
                    if (m.Success)
                    {
                        int nb = int.Parse(m.Groups[1].Value);
                        int from = int.Parse(m.Groups[2].Value);
                        int to = int.Parse(m.Groups[3].Value);
                        for(int i = 0; i < nb; i++)
                        {
                            var c = finalCrates[from - 1].Pop();
                            finalCrates[to - 1].Push(c);
                        }
                    }
                }

                if (queueMode && string.IsNullOrWhiteSpace(l))
                {
                    queueMode = false;
                    finalCrates = new Stack<string>[crates.Length];
                    for(var j = 0; j < finalCrates.Length; j++)
                    {
                        crates[j].Reverse();
                        finalCrates[j] = new Stack<string>(crates[j]);
                    }
                }
            }

            // first
            Console.WriteLine(String.Join("", finalCrates.Select(f => f.Peek())));
        }

        public void Solution2()
        {
            string[] lines = File.ReadAllLines("5\\input.txt");

            int finalScore = 0;
            Regex r = new Regex(@"\[([A-Z])\]");
            Regex rMove = new Regex(@"move ([0-9]+) from ([0-9]+) to ([0-9]+)");
            bool queueMode = true;
            List<string>[]? crates = null;
            Stack<string>[]? finalCrates = null;
            foreach (string l in lines)
            {
                if (queueMode)
                {
                    int nbCrates = (l.Length + 1) / 4;
                    if (crates == null)
                    {
                        crates = new List<string>[nbCrates];
                        for (var j = 0; j < nbCrates; j++)
                            crates[j] = new List<string>();
                    }
                    for (int i = 0; i < nbCrates * 4; i += 4)
                    {
                        string val = l.Substring(i, i == (nbCrates - 1) * 4 ? 3 : 4);
                        if (string.IsNullOrWhiteSpace(val.Trim()))
                            continue;
                        var m = r.Match(val.Trim());
                        if (m.Success)
                            crates[i / 4].Add(m.Groups[1].Value);
                    }
                }
                else
                {
                    var m = rMove.Match(l);
                    if (m.Success)
                    {
                        int nb = int.Parse(m.Groups[1].Value);
                        int from = int.Parse(m.Groups[2].Value);
                        int to = int.Parse(m.Groups[3].Value);
                        List<string> tempRes = new List<string>();
                        for (int i = 0; i < nb; i++)
                            tempRes.Add(finalCrates[from - 1].Pop());
                        Stack<string> s = new Stack<string>(tempRes);
                        while(s.Count > 0)
                            finalCrates[to - 1].Push(s.Pop());
                    }
                }

                if (queueMode && string.IsNullOrWhiteSpace(l))
                {
                    queueMode = false;
                    finalCrates = new Stack<string>[crates.Length];
                    for (var j = 0; j < finalCrates.Length; j++)
                    {
                        crates[j].Reverse();
                        finalCrates[j] = new Stack<string>(crates[j]);
                    }
                }
            }

            // first
            Console.WriteLine(String.Join("", finalCrates.Select(f => f.Peek())));
        }
    }
}
