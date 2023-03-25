using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using static AdventCode2022.Day7;

namespace AdventCode2022
{
    public static class LinqExt
    {
        public static IEnumerable<Day7.Dir> Descendants(this Day7.Dir root)
        {
            var nodes = new Stack<Day7.Dir>(new[] { root });
            while (nodes.Any())
            {
                Day7.Dir node = nodes.Pop();
                yield return node;
                foreach (var n in node.SubDirs) nodes.Push(n);
            }
        }
    }

    public class Day7
    {
        public class AFile
        {
            public string Name { get; set; }
            public long Size { get; set; }
            public AFile(string n, long s)
            {
                Name = n;
                Size = s;
            }
        }
        public class Dir
        {
            public string Name { get; set; }
            public List<Dir> SubDirs { get; set; }
            public List<AFile> Files { get; set; }
            public Dir(string name)
            {
                Name = name;
                SubDirs = new List<Dir>();
                Files = new List<AFile>();
            }
            public long TotalSize { 
                get { 
                    return Files.Sum(f => f.Size) + SubDirs.Sum(s => s.TotalSize);
                } 
            }
        }


        public void Solution1()
        {
            string[] lines = File.ReadAllLines("7\\input.txt");

            long result = 0;

            Dir finalDir = new Dir("/");
            bool first = true;
            Dir curDir = finalDir;
            List<string> cdList = new List<string>();
            int i = 0;

            foreach (string l in lines)
            {
                i++;
                if (first) { first = false; continue; }
                string[] cmd = l.Split(" ");
                if (cmd[0] == "$")
                {
                    if (cmd[1] == "cd")
                    {
                        if (cmd[2] == "..")
                        {
                            cdList = cdList.Take(cdList.Count - 1).ToList();
                            curDir = finalDir;
                            foreach (string dirName in cdList)
                                curDir = curDir.SubDirs.First(d => d.Name == dirName);
                        }
                        else
                        {
                            cdList.Add(cmd[2]);
                            curDir = curDir.SubDirs.First(d => d.Name == cmd[2]);
                        }
                    }
                    else if (cmd[1] == "ls")
                    {
                        continue;
                    }
                }
                else if (cmd[0] == "dir")
                {
                    curDir.SubDirs.Add(new Dir(cmd[1]));
                }
                else // forcément un fichier
                {
                    curDir.Files.Add(new AFile(cmd[1], long.Parse(cmd[0])));
                }
            }
            result = finalDir.Descendants().Where(d => d.TotalSize <= 100000).Sum(s => s.TotalSize);
            Console.WriteLine(result);
        }

        public void Solution2()
        {
            string[] lines = File.ReadAllLines("7\\input.txt");

            long result = 0;

            Dir finalDir = new Dir("/");
            bool first = true;
            Dir curDir = finalDir;
            List<string> cdList = new List<string>();
            int i = 0;

            foreach (string l in lines)
            {
                i++;
                if (first) { first = false; continue; }
                string[] cmd = l.Split(" ");
                if (cmd[0] == "$")
                {
                    if (cmd[1] == "cd")
                    {
                        if (cmd[2] == "..")
                        {
                            cdList = cdList.Take(cdList.Count - 1).ToList();
                            curDir = finalDir;
                            foreach (string dirName in cdList)
                                curDir = curDir.SubDirs.First(d => d.Name == dirName);
                        }
                        else
                        {
                            cdList.Add(cmd[2]);
                            curDir = curDir.SubDirs.First(d => d.Name == cmd[2]);
                        }
                    }
                    else if (cmd[1] == "ls")
                    {
                        continue;
                    }
                }
                else if (cmd[0] == "dir")
                {
                    curDir.SubDirs.Add(new Dir(cmd[1]));
                }
                else // forcément un fichier
                {
                    curDir.Files.Add(new AFile(cmd[1], long.Parse(cmd[0])));
                }
            }

            long toDelete = 30000000 - (70000000 - finalDir.TotalSize);

            result = finalDir.Descendants().Where(d => d.TotalSize > toDelete).OrderBy(d => d.TotalSize).First().TotalSize;

            Console.WriteLine(result);
        }
    }
}
