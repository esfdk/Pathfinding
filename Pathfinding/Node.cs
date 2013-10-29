namespace Pathfinding
{
    using System.Collections.Generic;

    public class Node
    {
        public Node parent;

        public int x, y;

        public float f, g, h;

        public int cost;

        public Node(int x, int y, int cost = 1)
        {
            this.x = x;
            this.y = y;
            this.cost = cost;
            parent = null;
        }

        public List<Node> GetSuccessors()
        {
            var nodes = new List<Node>();
            if (x > 0)
            {
                if (y > 0)
                {
                    nodes.Add(Program.map[x - 1][y - 1]);
                }
                nodes.Add(Program.map[x - 1][y]);
                if (y < Program.mapSizeY)
                {
                    nodes.Add(Program.map[x - 1][y + 1]);
                }
            }

            if (y > 0)
            {
                nodes.Add(Program.map[x][y - 1]);
            }

            nodes.Add(Program.map[x][y]);
            
            if (y < Program.mapSizeY)
            {
                nodes.Add((Program.map[x][y + 1]));
            }

            if (x < Program.mapSizeX)
            {
                if (y > 0)
                {
                    nodes.Add(Program.map[x + 1][y - 1]);
                }
                nodes.Add(Program.map[x + 1][y]);
                if (y < Program.mapSizeY)
                {
                    nodes.Add(Program.map[x + 1][y + 1]);
                }
            }

            return nodes;
        }
    }
}
