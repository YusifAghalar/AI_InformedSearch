using System;
using System.IO;

namespace AI_Project1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = System.IO.File.ReadAllLines(Directory.GetCurrentDirectory()+"/input.txt");
            var problem  = Problem.Init(input);
            var steps= problem.Search();
            if (steps > 0) Console.WriteLine(steps);
            else Console.WriteLine("Path not found");
        }
    }
}
