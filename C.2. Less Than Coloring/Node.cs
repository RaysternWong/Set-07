using System.Collections.Generic;

namespace LessThanColoring
{
    public class Node
    {
        public string Name { get; set; }
        public int Power { get; set; }
        public Color Color { get; set; } = Color.Blank;
        public List<Node> Neighbours { get; set; } = new List<Node>();

        public void MakeNeighbour(Node newNode)
        {
            if (Neighbours.Contains(newNode))
            {
                return;
            }

            Neighbours.Add(newNode);
            newNode.Neighbours.Add(this);
        }
    }
}
