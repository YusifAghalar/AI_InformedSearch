using Priority_Queue;
using System;
using System.Collections.Generic;
using System.Linq;


namespace AI_Project1
{
    public partial class Problem
    {
        private Problem()
        {
            ActiveStates = new List<State>();
            VisitedStates = new List<State>();
        }


        public int Goal { get; set; }
        public List<State> ActiveStates { get; set; }
        public List<State> VisitedStates { get; set; }
        public static Problem Init(string[] lines)
        {

            var pithces = lines[0].Split(",", StringSplitOptions.RemoveEmptyEntries).Select(x => new WaterPitch(int.Parse(x))).ToList();
            pithces.ForEach(x => x.Current = x.Capacity);
            pithces.Add(WaterPitch.InfiniteWaterPitch());
            var goal = int.Parse(lines[1]);
            return new Problem() {  Goal = goal, ActiveStates = new List<State> { new State(pithces,null,goal) { } } };

        }

        internal int Search()
        {
          

            while (ActiveStates.Any())
            {
                //Change with Pirioriry queue
                var searchedStated = ActiveStates.OrderBy(x => x.Distance).First();
                
              

                if (searchedStated.HasReachedGoal(Goal))
                {
                    return searchedStated.Cost;
                }
                VisitedStates.Add(searchedStated);
                ActiveStates.Remove(searchedStated);

                var possibleStates = GetPossible(searchedStated);

                foreach (var state in possibleStates)
                {
                    if (VisitedStates.Any(x => x.Key == state.Key))
                        continue;

                    if (ActiveStates.Any(x => x.Key==state.Key))
                    {
                        var existingState = ActiveStates.First(x => x.Key==state.Key);
                        if (existingState.Distance > searchedStated.Distance)
                        {
                            ActiveStates.Remove(existingState);
                            ActiveStates.Add(state);
                        }
                    }
                    else
                    {
                       
                        ActiveStates.Add(state);
                    }

                }
                ActiveStates = ActiveStates.Distinct(new StateEqualityComparer()).ToList();
            }
           

            return -1;
        }
        //Needs rework
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
                   
                    var newState = new State(temp,state,Goal);
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
            var emptiedstate = new State(emptied,currentState,Goal);
            
            possible.Add(emptiedstate);
            
        }
        private void AddFilledPitch(int i, List<State> possible, State currentState)
        {
           
            var filled = new List<WaterPitch>(currentState.Pitches.Select(x => new WaterPitch(x)));
            if (filled[i].IsInfinite) return;
            filled[i].Fill();
            var filledState = new State(filled, currentState,Goal);
            possible.Add(filledState);
           
        }
    }
}
