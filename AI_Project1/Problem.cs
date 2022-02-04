using Priority_Queue;
using System;
using System.Collections.Generic;
using System.Linq;


namespace AI_Project1
{
    public  class Problem
    {
        private Problem()
        {
            ActiveStates = new FastPriorityQueue<State>(15000);
            VisitedStates = new Dictionary<string, float>();
        }
        
        public int Goal { get; set; }
        public float MaxCapacity { get; set; }
        public HashSet<(float value,float cost)> EasyToGetPossibleVolumes { get; set; }
        public FastPriorityQueue<State> ActiveStates { get; set; }
        public Dictionary<string,float> VisitedStates { get; set; }

        public List<int> Capacities { get; set; }
        public static Problem Init(string[] lines)
        {

            var capacities = lines[0].Split(",", StringSplitOptions.RemoveEmptyEntries);
            var pithces = capacities.Select(x => new WaterPitch(float.Parse(x))).ToList();
            var goal = int.Parse(lines[1]);
            
            var maxCap = pithces.OrderByDescending(x => x.Capacity).FirstOrDefault().Capacity;

            var set = new HashSet<(float,float)>();
            
            
            pithces.Add(WaterPitch.InfiniteWaterPitch());
            var pq = new FastPriorityQueue<State>(15000);
            pq.Enqueue(new State(pithces, null, goal,maxCap,set) { }, 0);
            return new Problem() {  
                Goal = goal,
                ActiveStates =  pq,
                MaxCapacity = maxCap,
                EasyToGetPossibleVolumes = set,
                Capacities=capacities.Select(x=>int.Parse(x)).ToList()};

        }

    

        internal State Search()
        {
            if (!Helper.IsSolvable(Capacities.ToArray(), Goal))
                return null;

            while (ActiveStates.Any())
            {
              
                var searchedStated = ActiveStates.Dequeue();
                Console.WriteLine($"{searchedStated.Key} - {searchedStated.Distance} -  {searchedStated.Cost} -  {searchedStated.CostDistance}");
                if (searchedStated.HasReachedGoal(Goal)) return searchedStated;

              

                VisitedStates.Add(searchedStated.Key,searchedStated.Cost);
                
                if (searchedStated.Cost < 6)
                {
                    searchedStated.Pitches.ForEach(x=>EasyToGetPossibleVolumes.Add((x.Current,searchedStated.Cost)));
                }
                
                var possibleStates = GetPossible(searchedStated,EasyToGetPossibleVolumes);

                foreach (var state in possibleStates)
                {
                   
                    if (VisitedStates.ContainsKey(state.Key))
                        continue;

                    if (ActiveStates.Any(x => x.Key==state.Key))
                    {
                        var existingState = ActiveStates.First(x => x.Key==state.Key);
                        if (existingState.CostDistance > searchedStated.CostDistance)
                        {
                            ActiveStates.Remove(existingState);
                            ActiveStates.Enqueue(state,state.CostDistance);
                        }
                    }
                    else
                    {
                        ActiveStates.Enqueue(state, state.CostDistance);
                    }

                }
                
            }
           

            return null;
        }
        

        
        private List<State> GetPossible(State state, HashSet<(float value, float cost)> easyToGetPossibleVolumes)
        {

            var possible = new List<State>();
            for (int i = 0; i < state.Pitches.Count; i++)
            {
                
                AddEmptiedPitch(i,possible,state);
                AddFilledPitch(i, possible, state);
                for (int j = 0; j< state.Pitches.Count; j++)
                {
                  
                    var temp  = new List<WaterPitch>(state.Pitches.Select(x => new WaterPitch(x)));
                    temp[i].FillFrom(temp[j]);
                   
                    var newState = new State(temp,state,Goal,MaxCapacity,easyToGetPossibleVolumes);
                    if(!possible.Any(x=>x.Key==newState.Key))
                        possible.Add(newState);
                    
                }
            }
            
            return possible.ToList();

        }
        private void AddEmptiedPitch(int i, List<State> possible,State currentState)
        {

          
            var emptied = new List<WaterPitch>(currentState.Pitches.Select(x => new WaterPitch(x)));
            if (emptied[i].IsInfinite) return;
            emptied[i].Empty();
            var emptiedstate = new State(emptied,currentState,Goal,MaxCapacity,EasyToGetPossibleVolumes);
            
            possible.Add(emptiedstate);
            
        }
        private void AddFilledPitch(int i, List<State> possible, State currentState)
        {
           
            var filled = new List<WaterPitch>(currentState.Pitches.Select(x => new WaterPitch(x)));
            if (filled[i].IsInfinite) return;
            filled[i].Fill();
            var filledState = new State(filled, currentState,Goal,MaxCapacity,EasyToGetPossibleVolumes);
            possible.Add(filledState);
           
        }
    }
}
