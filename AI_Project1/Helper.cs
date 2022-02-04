using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Project1
{
    public static class Helper
    {
        public static int GCD(int[] numbers) => numbers.Aggregate(GCD);
        

        public static int GCD(int a, int b) => b == 0 ? a : GCD(b, a % b);
        

        public static bool IsSolvable(int [] capacities,int goal) =>  goal % GCD(capacities) == 0;
           
      
    }
}
