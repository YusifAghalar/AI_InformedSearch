using System;
using System.Diagnostics;
using System.IO;

namespace AI_Project1
{
    class Program
    {
        static void Main(string[] args)
        {

            var sw = Stopwatch.StartNew();
            sw.Start();
            string[] input = System.IO.File.ReadAllLines(Directory.GetCurrentDirectory() + "/input.txt");
            var problem = Problem.Init(input);
            var finalState = problem.Search();

            PrintPath(finalState);
            
            sw.Stop();
            Console.Write("Elapset time: ");
            Console.Write(sw.ElapsedMilliseconds);
        }

        private static void PrintPath(State finalState)
        {
            if (finalState != null)
            {
                Console.WriteLine(finalState.Cost);

                while (finalState != null)
                {
                    Console.WriteLine(finalState.Key);
                    finalState = finalState.Parent;
                }
            }
            else Console.WriteLine("Path not found");
          
        }
    }
}
