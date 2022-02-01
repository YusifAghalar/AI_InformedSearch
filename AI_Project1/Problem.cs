using Priority_Queue;
using System;
using System.Collections.Generic;
using System.Linq;


namespace AI_Project1
{
    public class Problem
    {
        private Problem()
        {

        }


        public int Goal { get; set; }
        public List<State> ActiveStates { get; set; }
        public List<State> VisitedStates { get; set; }
        public static Problem Init(string[] lines)
        {

            var pithces = lines[0].Split(",", StringSplitOptions.RemoveEmptyEntries).Select(x => new WaterPitch(int.Parse(x))).ToList();
            pithces.Add(WaterPitch.InfiniteWaterPitch());
            var goal = int.Parse(lines[1]);
            return new Problem() {  Goal = goal, ActiveStates = new List<State> { new State(pithces,null) { } } };

        }

        internal int Search()
        {
          

            while (ActiveStates.Any())
            {
                //Change with Pirioriry queue
                var searchedStated = ActiveStates.OrderBy(x => x.Cost).First();

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
                        if (existingState.CostDistance > searchedStated.CostDistance)
                        {
                            ActiveStates.Remove(existingState);
                            ActiveStates.Add(searchedStated);
                        }
                    }
                    else
                    {
                       
                        ActiveStates.Add(state);
                    }

                }
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
                AddFilled(i,possible,state);
                for (int j = i; j< state.Pitches.Count; j++)
                {
                    var temp  = new List<WaterPitch>(state.Pitches);
                    (temp[i], temp[j]) = state.Pitches[i].FillFrom(state.Pitches[j]);
                    var newState = new State(temp,state);
                    possible.Add(newState);
                    
                }
            }

            return possible.Where(x => !x.Pitches.Any(p => !p.IsValid())).ToList();

        }
        private void AddEmptiedPitch(int i, List<State> possible,State currentState)
        {
            var emptied = new List<WaterPitch>(currentState.Pitches);
            emptied[i].Empty();
            var emptiedstate = new State(emptied,currentState);
            possible.Add(emptiedstate);
            
        }
        private void AddFilled(int i, List<State> possible, State currentState)
        {
            var filled = new List<WaterPitch>(currentState.Pitches);
            filled[i].Fill();
            var filledState = new State(filled, currentState);
            possible.Add(filledState);
            
        }
    }
}
