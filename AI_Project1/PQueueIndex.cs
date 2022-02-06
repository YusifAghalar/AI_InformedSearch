using System;

namespace AI_Project1
{
    public struct PQueueIndex : IComparable<PQueueIndex>
    {
        public float CostDistance { get; set; }
        public float Distance { get; set; }
        public int CompareTo(PQueueIndex other)
        {
            if (other.CostDistance == CostDistance)
            {
                if (Distance < other.Distance) return -1;

                else if (Distance == other.Distance) return 0;
                else return 1;
            }
                

            if (other.CostDistance > CostDistance)
                return -1;
            else return 1;
        }
    }
}
