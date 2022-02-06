
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
        public void HasFoundCorrentSteps(string a, string b,float cost)
        {
            var problem = Problem.Init(new string[] { a, b });
            var solution = problem.Search();
            Assert.Equal(solution.Cost,cost);
        }

        [Theory]
        [InlineData("3, 5, 10", "18", 15000)]
        [InlineData("2,3,5,19,121,852", "11443", 15000)]
        [InlineData("3,5,8,10", "207", 15000)]
        public void ShouldNotTakeLongerThan(string a, string b, long milliseconds)
        {
            var sw = Stopwatch.StartNew();
            sw.Start();
            var problem = Problem.Init(new string[] { a, b });
            problem.Search();
            sw.Stop();

            Assert.True(sw.ElapsedMilliseconds < milliseconds);
        }



    }
}
