namespace Pathfinding
{
    using System.Collections.Generic;

    /// <summary>
    /// An agent in the system.
    /// </summary>
    public class Agent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Agent"/> class.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="speed">The speed.</param>
        public Agent(int x, int y, int speed = 1)
        {
            this.X = x;
            this.Y = y;
            this.Speed = speed;
            this.CurrentPath = new List<Node>();
        }

        /// <summary>
        /// Gets or sets the x position of the agent.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Gets or sets the y position of the agent.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Gets or sets the speed with which the agent moves.
        /// </summary>
        public int Speed { get; set; }

        /// <summary>
        /// Gets or sets the current path of the agent.
        /// </summary>
        public List<Node> CurrentPath { get; set; }
    }
}
