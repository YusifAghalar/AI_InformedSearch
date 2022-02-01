using System.Collections.Generic;
using System.Linq;

namespace AI_Project1
{
    public class WaterPitch
    {
       
      
        public WaterPitch(int capacity)
        {
            Capacity = capacity;
          
        }
        public int Capacity { get; set; }
        public int Current { get; set; }
        public bool IsInfinite { get; set; }
        public WaterPitch (WaterPitch waterPitch)
        {
            Capacity = waterPitch.Capacity;
            Current = waterPitch.Current;
        }

        public void Empty()=> Current = 0;
        
        public void Fill() =>  Current = Capacity;

        public bool IsValid() => Current < Capacity;

        public int AvailableSpace => Capacity - Current;

        public (WaterPitch filled, WaterPitch emptied) FillFrom(WaterPitch a)
        {
            var first = new WaterPitch(this);
            var second = new WaterPitch(a);
            if (first.Current + second.Current <= first.Capacity)
            {
                first.Current += second.Current;
                second.Empty();
            }
            else {

                second.Current = second.Current - AvailableSpace;
                first.Current = first.Current + AvailableSpace;

            }
           

            return (first,second);

        }

     

    }

   
}