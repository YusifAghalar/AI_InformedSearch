using System.Collections.Generic;

namespace AI_Project1
{
    public class Node
    {
        public Node(WaterPitch pitch, int current)
        {
            Pitch = pitch;
            Current = current;
        }

        public WaterPitch Pitch { get; set; }

        public Node Parent { get; set; }
        public int Current { get; set; }
        public int Cost { get; set; }
        public int Distance { get; set; }

        public bool TryMove(int quantity)
        {
            if (Current + quantity > Pitch.Capacity)
                return false;

            Current = Current + quantity;
            return true;
        }

    

        public List<Node> GetPossibleNodes(List<int> waterAmounts)
        {

            var possiblePithces = new List<Node>();
            foreach (var item in waterAmounts)
            {
                var node = new Node(Pitch,Current);
                if (TryMove(item))
                    possiblePithces.Add(node);
            }
            return possiblePithces;
        }

        public void Empty()
        {
            Current = 0;
        }
    }
}