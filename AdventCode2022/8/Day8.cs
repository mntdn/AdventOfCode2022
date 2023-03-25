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
using static AdventCode2022.Day7;

namespace AdventCode2022
{
    public class Day8
    {
        public class Tree
        {
            public int Height { get; set; }
            public bool IsVisible { get; set; } = false;
            public int Score { get; set; } = -1;
            public Tree(int height)
            {
                Height = height;
            }
        }

        public bool IsTreeVisible(int pos, Tree[] treeLine)
        {
            if(pos == 0) return true;
            bool visible = true;
            for (int i = 0; i < pos; i++)
            {
                visible = treeLine[i].Height < treeLine[pos].Height;
                if (!visible) break;
            }
            return visible;
        }

        public int NbCanSee(List<Tree> treeLine)
        {
            if (treeLine.Count == 0) return 0;
            int result = 0;
            //for(int i = treeLine.Count - 1; i > 0; i--)
            //{
            //    bool canSee = true;
            //    for (int j = i; j > 0; j--)
            //    {
            //        canSee = treeLine[i].Height > treeLine[j - 1].Height;
            //        if (!canSee) break;
            //    }
            //    result += canSee ? 1 : 0;
            //}

            for(int i = 1; i < treeLine.Count; i++)
            {
                if (treeLine[i].Height >= treeLine[0].Height)
                {
                    result++;
                    break;
                }
                else
                    result++;
            }

            return result;
        }

        public void Solution1()
        {
            string[] lines = File.ReadAllLines("8\\input.txt");
            int result = 0;
            Tree[][] trees = new Tree[lines[0].Length][];
            int i = 0;
            foreach (string line in lines)
                trees[i++] = line.ToList().Select(l => new Tree(int.Parse(l.ToString()))).ToArray();

            // gauche
            for (i = 0; i < trees.Length; i++)
            {
                for (int j = 0; j < trees[i].Length; j++)
                {
                    if (IsTreeVisible(j, trees[i]))
                        trees[i][j].IsVisible = true;
                }
            }

            // droite (on inverse)
            for (i = 0; i < trees.Length; i++)
            {
                List<Tree> revList = trees[i].ToList();
                revList.Reverse();
                for (int j = 0; j < trees[i].Length; j++)
                {
                    if (IsTreeVisible(j, revList.ToArray()))
                        trees[i][trees[i].Length - 1 - j].IsVisible = true;
                }
            }

            // haut (on reconstruit)
            for (i = 0; i < trees[0].Length; i++)
            {
                List<Tree> revList = new List<Tree>();
                for (int j = 0; j < trees.Length; j++)
                    revList.Add(trees[j][i]);

                for (int j = 0; j < revList.Count; j++)
                {
                    if (IsTreeVisible(j, revList.ToArray()))
                        trees[j][i].IsVisible = true;
                }
            }

            // bas (on reconstruit et on renverse)
            for (i = 0; i < trees[0].Length; i++)
            {
                List<Tree> revList = new List<Tree>();
                for (int j = 0; j < trees.Length; j++)
                    revList.Add(trees[j][i]);

                revList.Reverse();

                for (int j = 0; j < revList.Count; j++)
                {
                    if (IsTreeVisible(j, revList.ToArray()))
                        trees[trees.Length - 1 - j][i].IsVisible = true;
                }
            }

            trees.ToList().ForEach(t =>
            {
                t.ToList().ForEach(a => {
                    if(a.IsVisible)
                        Console.ForegroundColor = ConsoleColor.White;
                    else 
                        Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(a.Height);
                });
                Console.WriteLine();
            });

            trees.ToList().ForEach(t =>
            {
                result += t.ToList().Count(s => s.IsVisible);
            });

            Console.WriteLine(result);
        }

        public void Solution2()
        {
            string[] lines = File.ReadAllLines("8\\input.txt");
            int result = 0;
            Tree[][] trees = new Tree[lines[0].Length][];
            int i = 0;
            foreach (string line in lines)
                trees[i++] = line.ToList().Select(l => new Tree(int.Parse(l.ToString()))).ToArray();

            for (i = 0; i < trees.Length; i++)
            {
                for (int j = 0; j < trees[0].Length; j++)
                {
                    //i = 1; int j = 2;
                    // droite
                    List<Tree> d = new List<Tree>();
                    for(int k = j; k < trees[0].Length; k++)
                        d.Add(trees[i][k]);
                    // gauche
                    List<Tree> g = new List<Tree>();
                    for(int k = j; k >= 0; k--)
                        g.Add(trees[i][k]);
                    // haut
                    List<Tree> h = new List<Tree>();
                    for(int k = i; k >= 0; k--)
                        h.Add(trees[k][j]);
                    // bas
                    List<Tree> b = new List<Tree>();
                    for(int k = i; k < trees.Length; k++)
                        b.Add(trees[k][j]);

                    trees[i][j].Score = NbCanSee(d) * NbCanSee(g) * NbCanSee(h) * NbCanSee(b);
                }
            }

            trees.ToList().ForEach(t =>
            {
                t.ToList().ForEach(a => {
                    Console.Write(a.Score);
                });
                Console.WriteLine();
            });

            for (i = 0; i < trees.Length; i++)
            {
                for (int j = 0; j < trees[0].Length; j++)
                {
                    result = trees[i][j].Score > result ? trees[i][j].Score : result;
                }
            }

            Console.WriteLine(result);
        }
    }
}
