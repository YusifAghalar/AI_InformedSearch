using Priority_Queue;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AI_Project1
{
    public class State:FastPriorityQueueNode
    {

        public State(List<WaterPitch> pitches, State parent, int goal,float maxCap, HashSet<(float value, float cost)> easyToGetPossibleVolumes)
        {
            Pitches = pitches;
            Parent = parent;
            Infinite = Pitches.FirstOrDefault(x => x.IsInfinite);

            if (Parent == null) Cost = 0;
            else Cost = parent.Cost + 1;

            SetDistance(goal,maxCap,easyToGetPossibleVolumes);

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
        public void SetDistance(float goal,float maxCap, HashSet<(float value, float cost)> easyToGetPossibleVolumes)
        {

           
            Distance = (Math.Abs(goal  - Infinite.Current));
            
            if (Distance < maxCap)
            {
                var capacities = Pitches.Where(x=>!x.IsInfinite).Select(x => x.Capacity);
                var currents = Pitches.Where(x=>!x.IsInfinite).Select(x => x.Current);
                EstimateV1(maxCap,currents,capacities,easyToGetPossibleVolumes);
            }
            else
            {
                EstimateV2(maxCap,easyToGetPossibleVolumes);
            }

          
        }

        private void EstimateV2(float maxCap, HashSet<(float value, float cost)> easyToGetPossibleVolumes)
        {
            float remaider = Distance % maxCap;
            Distance = Distance / maxCap;

            if (Distance % 1 != 0)
            {
                Distance = Distance *2 + 1;
               
                Distance= Distance + easyToGetPossibleVolumes.Where(x => Math.Abs(x.value - remaider) == 0).OrderBy(x => x.cost).FirstOrDefault().cost;

            }
            else
            {
                Distance = Distance * 2;
            }
            CostDistance = Distance + Cost;
        }

        private void EstimateV1(float maxCap, IEnumerable<float> currents,IEnumerable<float> capacities, HashSet<(float value, float cost)> easyToGetPossibleVolumes)
        {
            if (Distance <= maxCap)
            {
                foreach (var cur in currents)
                {
                    if (Distance - cur == 0)
                    {
                        Distance = 0;
                        CostDistance = Distance + Cost;
                        return;
                        
                    }
                    
                }

                foreach (var cap in capacities)
                {
                    if (Distance - cap == 0)
                    {
                        Distance = 1;
                        CostDistance = Distance + Cost;
                        return;
                        
                    }
                }

                foreach (var volume in easyToGetPossibleVolumes.OrderBy(x=>x.cost))
                {

                    if (Distance - volume.value == 0)
                    {
                        Distance = volume.cost;
                        CostDistance = Distance + Cost;
                        return;

                    }
                }






                // Todo: make better estimation here. Distance is not 2.
                Distance = 2;
                CostDistance = Distance + Cost;
               
            }

        
        }

        public string Key => string.Join(" ", Pitches.Select(x => x.Current.ToString()));

        public override int GetHashCode()
        {
            return Key.GetHashCode();
        } 
      
    }

}
