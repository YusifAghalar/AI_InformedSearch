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
            ActiveStates =  new PriorityQueue<State, PQueueIndex>(15000);
            VisitedStates = new Dictionary<string, bool>();
        }
        
        public int Goal { get; set; }
        public float MaxCapacity { get; set; }
     
        public PriorityQueue<State, PQueueIndex> ActiveStates { get; set; }
        public Dictionary<string,bool> ActiveDict { get; set; }

        public Dictionary<string, bool> VisitedStates { get; set; }

        public List<int> Capacities { get; set; }
        public static Problem Init(string[] lines)
        {

            var capacities = lines[0].Split(",", StringSplitOptions.RemoveEmptyEntries);
            var pithces = capacities.Select(x => new WaterPitch(float.Parse(x))).ToList();
            var goal = int.Parse(lines[1]);
            
            var maxCap = pithces.OrderByDescending(x => x.Capacity).FirstOrDefault().Capacity;

            var set = new HashSet<float>();
            
            
            pithces.Add(WaterPitch.InfiniteWaterPitch());
            var pq = new PriorityQueue<State, PQueueIndex>(15000);
            pq.Enqueue(new State(pithces, null, goal,maxCap) { }, new PQueueIndex());
            return new Problem() {  
                Goal = goal,
                ActiveStates =  pq,
                MaxCapacity = maxCap,
              
                Capacities=capacities.Select(x=>int.Parse(x)).ToList()};

        }

    

        public State Search()
        {
            if (!Helper.IsSolvable(Capacities.ToArray(), Goal))
                return null;
           

            while (ActiveStates.Count>0)
            {
              
                var searchedStated = ActiveStates.Dequeue();
                Console.WriteLine($"{searchedStated.Key} - {searchedStated.Distance} -  {searchedStated.Cost} -  {searchedStated.CostDistance}");
                if (searchedStated.HasReachedGoal(Goal)) return searchedStated;

                if(!VisitedStates.ContainsKey(searchedStated.Key))
                    VisitedStates.Add(searchedStated.Key,true);
                
              
                var possibleStates = GetPossible(searchedStated);

                foreach (var state in possibleStates)
                {
                
                   
                    if (VisitedStates.ContainsKey(state.Key))
                        continue;

                    if (ActiveStates.UnorderedItems.Any(x => x.Element.Key==state.Key))
                    {
                        var existingState = ActiveStates.UnorderedItems.First(x => x.Element.Key==state.Key);
                        if (existingState.Element.Cost > searchedStated.CostDistance)
                        {
                            
                            ActiveStates.Enqueue(state,new PQueueIndex { Distance=state.Distance,CostDistance=state.CostDistance, RawDistance=Math.Abs(state.Infinite.Current-Goal)});
                        }
                    }
                    else
                    {
                        ActiveStates.Enqueue(state, new PQueueIndex { Distance = state.Distance, CostDistance = state.CostDistance, RawDistance = Math.Abs(state.Infinite.Current - Goal) });
                    }

                }
                
            }
           

            return null;
        }
        

        
        private List<State> GetPossible(State state)
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
                   
                    var newState = new State(temp,state,Goal,MaxCapacity);
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
            var emptiedstate = new State(emptied,currentState,Goal,MaxCapacity);
            
            possible.Add(emptiedstate);
            
        }
        private void AddFilledPitch(int i, List<State> possible, State currentState)
        {
           
            var filled = new List<WaterPitch>(currentState.Pitches.Select(x => new WaterPitch(x)));
            if (filled[i].IsInfinite) return;
            filled[i].Fill();
            var filledState = new State(filled, currentState,Goal,MaxCapacity);
            possible.Add(filledState);
           
        }
    }
}
