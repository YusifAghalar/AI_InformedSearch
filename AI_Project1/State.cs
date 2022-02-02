using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            SetDistance(goal);
            
        }
        public WaterPitch Infinite { get; set; }
        public List<WaterPitch> Pitches { get; set; }
        public State Parent { get; set; }

        public float Cost { get; set; }
        public float Distance { get; set; }
        public float CostDistance { get; set; }

        public bool HasReachedGoal(int goal)
        {
            return Pitches.FirstOrDefault(x => x.IsInfinite).Current == goal;
        }
        public void SetDistance(float goal)
        {
          

            var maxCapacity =Pitches.Where(x=>!x.IsInfinite).OrderByDescending(x => x.Capacity).ThenByDescending(x=>x.Current).FirstOrDefault().Capacity;
            var currentMax = Pitches.Where(x =>!x.IsInfinite).OrderByDescending(x => x.Current).FirstOrDefault().Current;
            Distance = (Math.Abs(goal - currentMax -  Infinite.Current)/maxCapacity)*2;
            CostDistance = Distance + Cost;
        }

        public string Key  => string.Join(" ", Pitches.Select(x => x.Current.ToString()));

        public override int GetHashCode()
        {
            return Key.GetHashCode();
        }

    }


}
