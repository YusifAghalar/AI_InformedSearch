using System;
using System.Collections.Generic;
using System.Linq;

namespace AI_Project1
{
    public class State
    {

        public State(List<WaterPitch> pitches,State parent,int goal) 
        {
            Pitches = pitches;
            Parent = parent;

            Infinite = Pitches.FirstOrDefault(x => x.IsInfinite);
            if (Parent == null)  Cost = 0;
            else Cost = parent.Cost + 1;

            Distance = Cost + (goal - Infinite.Current) ;
            
        }
        public WaterPitch Infinite { get; set; }
        public List<WaterPitch> Pitches { get; set; }
        public State Parent { get; set; }

        public int Cost { get; set; }
        public int Distance { get; set; }
   
        public bool HasReachedGoal(int goal)
        {
            return Pitches.FirstOrDefault(x => x.IsInfinite).Current == goal;
        }

        public string Key  => string.Join(" ", Pitches.Select(x => x.Current.ToString()));

    }


}
