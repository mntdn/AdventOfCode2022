using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.Http.Headers;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AdventCode2022
{
    public class Day11
    {
        static BigInteger finalModulo;
        public class Monkey 
        { 
            public Queue<BigInteger> Items {  get; set; }
            public BigInteger Operand { get; set; }
            public string Operator { get; set; }
            public BigInteger DivideTest { get; set; }
            public int TrueResult { get; set; }
            public int FalseResult { get; set; }
            public int NbInspections { get; set; } = 0;

            /// <summary>
            /// 1er int = id monkey, 2e = worry
            /// </summary>
            /// <returns></returns>
            public Tuple<int, BigInteger> Inspect(bool solution1 = true)
            {
                if (Items == null || Items.Count == 0)
                    return new Tuple<int, BigInteger>(-1, 0);
                BigInteger item = Items.Dequeue();
                BigInteger finalWorry;
                if(Operator == "+")
                {
                    finalWorry = BigInteger.Add(item, Operand);
                }
                else
                {
                    if (Operand == 0)
                        finalWorry = BigInteger.Pow(item, 2);
                    else
                        finalWorry = BigInteger.Multiply(item, Operand);
                    finalWorry %= finalModulo;
                }
                if (solution1)
                    finalWorry /= 3;
                NbInspections++;
                BigInteger.DivRem(finalWorry, DivideTest, out BigInteger rem);
                return new Tuple<int, BigInteger>(rem == 0 ? TrueResult : FalseResult, finalWorry);
            }
            public Monkey()
            {
                Items = new Queue<BigInteger>();
                Operator = "";
            }
        }
        public void Solution1()
        {
            string[] lines = File.ReadAllLines("11\\input.txt");
            BigInteger result = 0;

            List<Monkey> Monkeys = new List<Monkey>();
            Monkey CurrentMonkey = new Monkey();
            Regex op = new Regex(".*new = old (.+) (.*)");
            Regex test = new Regex(".*divisible by (.*)");
            Regex throwM = new Regex(".*throw to monkey (.*)");
            foreach (string line in lines)
            {
                string[] cmd = line.Split(":");
                if (cmd[0].Contains("Monkey"))
                {
                    if(CurrentMonkey.Operator != "")
                    {
                        Monkeys.Add(CurrentMonkey);
                        CurrentMonkey = new Monkey();
                    }
                }
                else if (cmd[0].Contains("items"))
                {
                    CurrentMonkey.Items = new Queue<BigInteger>(cmd[1].Split(",").ToList().Select(l => BigInteger.Parse(l.Trim())).ToList());
                }
                else if (cmd[0].Contains("Operation"))
                {
                    Match m = op.Match(cmd[1]);
                    if (m.Success)
                    {
                        CurrentMonkey.Operator = m.Groups[1].Value;
                        CurrentMonkey.Operand = m.Groups[2].Value == "old" ? 0 : BigInteger.Parse(m.Groups[2].Value);
                    }
                }
                else if (cmd[0].Contains("Test"))
                {
                    Match m = test.Match(cmd[1]);
                    if (m.Success)
                    {
                        CurrentMonkey.DivideTest = BigInteger.Parse(m.Groups[1].Value);
                    }
                }
                else if (cmd[0].Contains("true"))
                {
                    Match m = throwM.Match(cmd[1]);
                    if (m.Success)
                    {
                        CurrentMonkey.TrueResult = int.Parse(m.Groups[1].Value);
                    }
                }
                else if (cmd[0].Contains("false"))
                {
                    Match m = throwM.Match(cmd[1]);
                    if (m.Success)
                    {
                        CurrentMonkey.FalseResult = int.Parse(m.Groups[1].Value);
                    }
                }
            }
            Monkeys.Add(CurrentMonkey);

            for(int i = 0; i < 20; i++)
            {
                for (int j = 0; j < Monkeys.Count; j++)
                {
                    if (Monkeys[j].Items.Count > 0)
                    {
                        bool finish = false;
                        do
                        {
                            var res = Monkeys[j].Inspect();
                            finish = res.Item1 == -1;
                            if (!finish)
                            {
                                //Console.WriteLine($"{j} envoie {res.Item2} à {res.Item1}");
                                Monkeys[res.Item1].Items.Enqueue(res.Item2);
                            }
                        } while (!finish);
                    }
                }
            }

            result = Monkeys.Select(m => m.NbInspections).OrderByDescending(m => m).Take(2).Aggregate((a, b) => a * b);

            Console.WriteLine(result);
        }

        public void Solution2()
        {
            string[] lines = File.ReadAllLines("11\\input.txt");
            BigInteger result = 0;

            List<Monkey> Monkeys = new List<Monkey>();
            Monkey CurrentMonkey = new Monkey();
            Regex op = new Regex(".*new = old (.+) (.*)");
            Regex test = new Regex(".*divisible by (.*)");
            Regex throwM = new Regex(".*throw to monkey (.*)");
            foreach (string line in lines)
            {
                string[] cmd = line.Split(":");
                if (cmd[0].Contains("Monkey"))
                {
                    if (CurrentMonkey.Operator != "")
                    {
                        Monkeys.Add(CurrentMonkey);
                        CurrentMonkey = new Monkey();
                    }
                }
                else if (cmd[0].Contains("items"))
                {
                    CurrentMonkey.Items = new Queue<BigInteger>(cmd[1].Split(",").ToList().Select(l => BigInteger.Parse(l.Trim())).ToList());
                }
                else if (cmd[0].Contains("Operation"))
                {
                    Match m = op.Match(cmd[1]);
                    if (m.Success)
                    {
                        CurrentMonkey.Operator = m.Groups[1].Value;
                        CurrentMonkey.Operand = m.Groups[2].Value == "old" ? 0 : BigInteger.Parse(m.Groups[2].Value);
                    }
                }
                else if (cmd[0].Contains("Test"))
                {
                    Match m = test.Match(cmd[1]);
                    if (m.Success)
                    {
                        CurrentMonkey.DivideTest = BigInteger.Parse(m.Groups[1].Value);
                    }
                }
                else if (cmd[0].Contains("true"))
                {
                    Match m = throwM.Match(cmd[1]);
                    if (m.Success)
                    {
                        CurrentMonkey.TrueResult = int.Parse(m.Groups[1].Value);
                    }
                }
                else if (cmd[0].Contains("false"))
                {
                    Match m = throwM.Match(cmd[1]);
                    if (m.Success)
                    {
                        CurrentMonkey.FalseResult = int.Parse(m.Groups[1].Value);
                    }
                }
            }
            Monkeys.Add(CurrentMonkey);
            finalModulo = Monkeys.Select(s => s.DivideTest).Aggregate((a, b) => a * b);

            var watch = System.Diagnostics.Stopwatch.StartNew();
            // the code that you want to measure comes here
            for (int i = 0; i < 10000; i++)
            {
                for (int j = 0; j < Monkeys.Count; j++)
                {
                    if (Monkeys[j].Items.Count > 0)
                    {
                        bool finish = false;
                        do
                        {
                            var res = Monkeys[j].Inspect(false);
                            finish = res.Item1 == -1;
                            if (!finish)
                            {
                                //Console.WriteLine($"{j} envoie {res.Item2} à {res.Item1}");
                                Monkeys[res.Item1].Items.Enqueue(res.Item2);
                            }
                        } while (!finish);
                    }
                }
            }
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            result = Monkeys.Select(m => (BigInteger)m.NbInspections).OrderByDescending(m => m).Take(2).Aggregate((a, b) => a * b);

            Console.WriteLine(result);
        }
    }
}
