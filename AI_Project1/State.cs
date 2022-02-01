using System;
using System.Collections.Generic;
using System.Linq;

namespace AI_Project1
{
    public class State
    {
        public List<WaterPitch> Pitches { get; set; }
        public int Cost { get; set; }

        //ToDo
        public int Distance=>  42;
        

        public int CostDistance => Distance + Cost;
        public bool HasReachedGoal(int goal)
        {
            return Pitches.FirstOrDefault(x => x.IsInfinite).Current == goal;
        }

        public string Key  => string.Join(" ", Pitches.Select(x => x.Current.ToString()));
    }


}
