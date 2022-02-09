using Priority_Queue;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AI_Project1
{
    public class State : FastPriorityQueueNode
    {
        public State(List<WaterPitch> pitches, State parent, int goal, float maxCap)
        {
            Pitches = pitches;
            Parent = parent;
            Infinite = Pitches.FirstOrDefault(x => x.IsInfinite);

            if (Parent == null) Cost = 0;
            else Cost = parent.Cost + 1;

          
        }

        public WaterPitch Infinite { get; set; }
        public List<WaterPitch> Pitches { get; set; }
        public State Parent { get; set; }

        public float Cost { get; set; }
        public float Distance { get; set; }
        public float CostDistance { get; set; }


        public bool HasReachedGoal(int goal)
        {
            return Infinite.Current == goal;
        }

        public void SetDistance(float goal, float maxCap)
        {
            Distance = (Math.Abs(goal - Infinite.Current));
            var capacities = Pitches.Where(x => !x.IsInfinite).Select(x => x.Capacity).ToList();
            var currents = Pitches.Where(x => !x.IsInfinite).Select(x => x.Current).ToList();
            if (Helper.GoalIsLesserThanMinimal(capacities.ToArray(), goal))
            {
                Distance = Math.Abs(Infinite.Current / goal - 1) * 2;
                CostDistance = Cost + Distance;
                return;
            }

            Estimate(maxCap, capacities, currents);
            
        }

        private float EstimateRemainder(float remainder, List<float> pithces,List<float> currentValues)
        {
           

            if (remainder == 0) return 0; 
            var candidate = pithces.OrderByDescending(x => x).FirstOrDefault();

            if (pithces.Any(x => x == remainder)||currentValues.Any(x=>x==remainder))
                return 1;
            float remainderEstimate = (float) (Math.Floor(remainder / candidate) * 2);

            if (pithces.Count == 1 && remainder != 0) return 2;

            pithces.Remove(candidate);

            return remainderEstimate+Math.Min(EstimateRemainder(remainder % candidate, new List<float>(pithces),currentValues ), EstimateRemainder(candidate - remainder % candidate, new List<float>(pithces),currentValues)+2);
        }

        private void Estimate(float maxCap, IEnumerable<float> capacities, IEnumerable<float> current)
        {
           
            if (Distance % maxCap != 0)
            {
              
                Distance = EstimateRemainder(Distance, capacities.ToList(),current.ToList());
                CostDistance = Distance + Cost;
            }
            else
            {
                Distance = (float)(Distance/maxCap) * 2;
            }

            CostDistance = Distance + Cost;
        }


        public string Key => string.Join(" ", Pitches.Select(x => x.Current.ToString()));

        public override int GetHashCode()
        {
            return Key.GetHashCode();
        }
    }
}