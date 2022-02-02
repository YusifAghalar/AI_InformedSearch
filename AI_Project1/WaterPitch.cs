
namespace AI_Project1
{
    public class WaterPitch
    {
       
      
        public WaterPitch(float capacity)
        {
            Capacity = capacity;
          
        }
        public float Capacity { get; set; }
        public float Current { get; set; }
        public bool IsInfinite { get; set; }
        public WaterPitch (WaterPitch waterPitch)
        {
            Capacity = waterPitch.Capacity;
            Current = waterPitch.Current;
            IsInfinite = waterPitch.IsInfinite;
        }

        public void Empty()=> Current = 0;
        
        public void Fill() =>  Current = Capacity;

        public bool IsValid() => Current <= Capacity;

        public float AvailableSpace => Capacity - Current;

        public void FillFrom(WaterPitch second)
        {
          
            if (Current + second.Current <= Capacity)
            {
                Current += second.Current;
                second.Empty();
            }
            else {

                second.Current = second.Current - AvailableSpace;
                Current = Current + AvailableSpace;

            }


        }


        public static WaterPitch InfiniteWaterPitch()
        {
            var wp = new WaterPitch(int.MaxValue);
            wp.IsInfinite = true;
            return wp;
        }
    }

   
}