namespace Pathfinding
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A node in the 2D coordinate system.
    /// </summary>
    public class Node
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Node"/> class.
        /// </summary>
        /// <param name="x">The x position.</param>
        /// <param name="y">The y position.</param>
        /// <param name="cost"> The cost to enter the node.</param>
        public Node(int x, int y, int cost = 1)
        {
            this.X = x;
            this.Y = y;
            this.Cost = cost;
            this.Parent = null;
        }

        /// <summary>
        /// Gets or sets the parent node.
        /// </summary>
        public Node Parent { get; set; }

        /// <summary>
        /// Gets or sets the x position.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Gets or sets the y position.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Gets or sets the price to get to the end through this node.
        /// </summary>
        public float F { get; set; }

        /// <summary>
        /// Gets or sets the cost to get to this node.
        /// </summary>
        public float G { get; set; }

        /// <summary>
        /// Gets or sets the distance between this node and the goal.
        /// </summary>
        public float H { get; set; }

        /// <summary>
        /// Gets or sets the cost to enter this node.
        /// </summary>
        public int Cost { get; set; }

        /// <summary>
        /// Euclidean distance between two nodes.
        /// </summary>
        /// <param name="first">The first node.</param>
        /// <param name="second">The second node.</param>
        /// <returns>The distance.</returns>
        public static float DistanceBetweenNodes(Node first, Node second)
        {
            return (float)Math.Sqrt(Math.Pow(second.X - first.X, 2) + Math.Pow(second.Y - first.Y, 2));
        }

        /// <summary>
        /// Gets the successors of this node.
        /// </summary>
        /// <returns>The <see cref="List"/>. of successors for this node. </returns>
        public List<Node> GetSuccessors()
        {
            var nodes = new List<Node>();
            if (this.X > 0)
            {
                if (this.Y > 0)
                {
                    nodes.Add(Program.Map[this.X - 1, this.Y - 1]);
                }
                
                nodes.Add(Program.Map[this.X - 1, this.Y]);
                
                if (this.Y < Program.MapHeight - 1)
                {
                    nodes.Add(Program.Map[this.X - 1, this.Y + 1]);
                }
            }

            if (this.Y > 0)
            {
                nodes.Add(Program.Map[this.X, this.Y - 1]);
            }
            
            if (this.Y < Program.MapHeight - 1)
            {
                nodes.Add(Program.Map[this.X, this.Y + 1]);
            }

            if (this.X < Program.MapWidth - 1)
            {
                if (this.Y > 0)
                {
                    nodes.Add(Program.Map[this.X + 1, this.Y - 1]);
                }

                nodes.Add(Program.Map[this.X + 1, this.Y]);

                if (this.Y < Program.MapHeight - 1)
                {
                    nodes.Add(Program.Map[this.X + 1, this.Y + 1]);
                }
            }

            return nodes;
        }
    }
}
