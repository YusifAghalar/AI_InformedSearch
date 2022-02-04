using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Project1
{
    public static class Helper
    {
        public static int GCD(int[] numbers)
        {
            int a = numbers.Aggregate(GCD);
            return numbers.Aggregate(GCD);
        }

        public static int GCD(int a, int b)
        {
            return b == 0 ? a : GCD(b, a % b);
        }

        public static bool IsSolvable(int [] capacities,int goal)
        {
            return goal % Helper.GCD(capacities) == 0;
           
        }
    }
}
