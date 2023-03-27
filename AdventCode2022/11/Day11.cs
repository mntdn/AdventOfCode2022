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
    public class Day11
    {
        public class Monkey 
        { 
            List<int> Items {  get; set; }
            Func<int, int> Func { get; set; }
            int DivideTest { get; set; }
        }
        public void Solution1()
        {
            string[] lines = File.ReadAllLines("11\\input.txt");
            int result = 0;
            Console.WriteLine(result);
        }

        public void Solution2()
        {
        }
    }
}
