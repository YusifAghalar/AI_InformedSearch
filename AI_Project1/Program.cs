using System;
using System.IO;

namespace AI_Project1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = System.IO.File.ReadAllLines(Directory.GetCurrentDirectory() + "/input.txt");
            var problem = Problem.Init(input);
            var finalState = problem.Search();

            PrintPath(finalState);
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
