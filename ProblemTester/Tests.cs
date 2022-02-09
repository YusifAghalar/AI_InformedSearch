
using AI_Project1;
using System.Diagnostics;
using Xunit;

namespace ProblemTester
{
   public  class ProblemTester
    {

        [Theory]
        [InlineData("3, 10, 15", "18")]
        [InlineData("2,3,5,19,121,852", "11443")]
        [InlineData("3,5", "4")]
        public void IsSolvable(string a, string b)
        {
            var problem = Problem.Init(new string[] { a, b });
            var solution = problem.Search();
            Assert.NotNull(solution);
        }


        [Theory]
        [InlineData("3, 6", "10")]
        [InlineData("2,4,10", "101")]
        [InlineData("2", "143")]
        public void NotSolvable(string a, string b)
        {
            var problem = Problem.Init(new string[] { a, b });
            var solution = problem.Search();
            Assert.Null(solution);
        }


        [Theory]
        [InlineData("3, 5, 10", "18",6)]
        [InlineData("2,3,5,19,121,852", "11443",36)]
        [InlineData("3,5,8,10", "207", 43)]
        [InlineData("3,5,9", "34", 11)]
        [InlineData("2,5,6,72", "143", 7)]
        [InlineData("3,5", "4",7)]
        [InlineData("3,5", "17",9)]
        [InlineData("3,5", "14",8)]
        [InlineData("1,94,100", "12",7)]
        [InlineData("1,97,100", "3",3)]
        public void HasFoundCorrentSteps(string a, string b,float cost)
        {
            var problem = Problem.Init(new string[] { a, b });
            var solution = problem.Search();
            Assert.Equal(solution.Cost,cost);
        }

        [Theory]
        [InlineData("3, 5, 10", "18", 10000)]
        [InlineData("2,3,5,19,121,852", "11443", 10000)]
        [InlineData("2,3,5,19,121,852", "21443", 15000)]
        [InlineData("3,5,8,10", "207", 10000)]
        public void ShouldNotTakeLongerThan(string a, string b, long milliseconds)
        {
            var sw = Stopwatch.StartNew();
            sw.Start();
            var problem = Problem.Init(new string[] { a, b });
            problem.Search();
            sw.Stop();

            Assert.True(sw.ElapsedMilliseconds < milliseconds);
        }
        [Theory]
        [InlineData("3, 5, 10", "18", 6)]
        [InlineData("2,3,5,19,121,852", "11443", 36)]
        [InlineData("3,5,8,10", "207", 43)]
        [InlineData("3,5,9", "34", 11)]
        [InlineData("2,5,6,72", "143", 7)]
        [InlineData("3,5", "4", 7)]
        [InlineData("3,5", "17", 9)]
        [InlineData("3,5", "14", 8)]
        [InlineData("1,94,100", "12", 7)]
        [InlineData("1,97,100", "3", 3)]
        public void Is_Admissable(string a, string b, float max  )
        {
            var problem = Problem.Init(new string[] { a, b.ToString() });
            var solution = problem.Search();

            float maxEstiomation=0;
            while (solution!=null)
            {
                if (solution.Distance > maxEstiomation)
                {
                    maxEstiomation = solution.Distance;
                };
                solution = solution.Parent;
            }

            Assert.True(maxEstiomation <= max);
        }


    }
}
