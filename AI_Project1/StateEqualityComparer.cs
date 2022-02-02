using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;


namespace AI_Project1
{
  
        public class StateEqualityComparer : IEqualityComparer<State>
        {
            public bool Equals(State x, State y)
            {
                return x.Key == y.Key;
            }

            public int GetHashCode([DisallowNull] State obj)
            {
                return obj.Key.GetHashCode();
            }
        }
   

  
}
