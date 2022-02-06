using Priority_Queue;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AI_Project1
{
    public class State:FastPriorityQueueNode
    {
        
        public State(List<WaterPitch> pitches, State parent, int goal,float maxCap)
        {
            Pitches = pitches;
            Parent = parent;
            Infinite = Pitches.FirstOrDefault(x => x.IsInfinite);

            if (Parent == null) Cost = 0;
            else Cost = parent.Cost + 1;

            SetDistance(goal,maxCap);

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
        public void SetDistance(float goal,float maxCap)
        {
            Distance = (Math.Abs(goal  - Infinite.Current));
            var capacities = Pitches.Where(x => !x.IsInfinite).Select(x => x.Capacity).ToList();
            var currents = Pitches.Where(x => !x.IsInfinite).Select(x => x.Current).ToList();
            if (Distance < maxCap)
            {
                EstimateV1(maxCap,currents,capacities);
            }
            else
            {
                EstimateV2(maxCap, capacities);
            }

          
        }

        private float EstimateRemainder(float remainder, List<float> pithces)
        {
            float remainderEstimate = 0;
            var candidate = pithces.Where(x => remainder > x).OrderByDescending(x => x).FirstOrDefault();
            while (candidate != 0)
            {
                remainderEstimate += (float)(Math.Floor(remainder / candidate) * 2);
                remainder =  remainder % candidate;
                candidate = pithces.Where(x => remainder > x).OrderByDescending(x => x).FirstOrDefault();
            }
            if (remainder != 0) remainderEstimate +=2 ;
            

            return remainderEstimate;

        }

        private void EstimateV2(float maxCap, IEnumerable<float> capacities)
        {
            var dist1 = Math.Floor(Distance / maxCap);

            
            if (Distance % maxCap != 0)
            {
                dist1 = dist1 * 2 ;

                var upperRemainder = Distance % maxCap;
                var lowerRemainder = maxCap - Distance % maxCap;

                var upperRemainderDistance = EstimateRemainder(upperRemainder, capacities.ToList());
                var lowerRemainderDistance = EstimateRemainder(lowerRemainder, capacities.ToList());

                var dist2 = Math.Min(upperRemainderDistance, lowerRemainderDistance);

                Distance =(float) (dist1+dist2);
                CostDistance = Distance + Cost;

            }
            else
            {
                Distance = (float)dist1 * 2;
            }
            CostDistance = Distance + Cost;


        }

        private void EstimateV1(float maxCap, IEnumerable<float> currents, IEnumerable<float> capacities)
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


                var upperRemainder = Distance % maxCap;
                var lowerRemainder = maxCap - Distance % maxCap;

                var a = EstimateRemainder(upperRemainder, capacities.ToList());
                var b = EstimateRemainder(lowerRemainder, capacities.ToList());

                var minRemainderDistance = Math.Min(a, b);

                Distance = minRemainderDistance;
                CostDistance = Distance+ Cost;
               
            }

        
        }

        public string Key => string.Join(" ", Pitches.Select(x => x.Current.ToString()));

        public override int GetHashCode()
        {
            return Key.GetHashCode();
        } 
      
    }
}
