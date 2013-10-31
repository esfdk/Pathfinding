namespace Pathfinding
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The agent chase main program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Gets the height of the map.
        /// </summary>
        public static int MapHeight { get; private set; }

        /// <summary>
        /// Gets the height of the map.
        /// </summary>
        public static int MapWidth { get; private set; }

        /// <summary>
        /// Gets the map.
        /// </summary>
        public static Node[,] Map { get; private set; }

        /// <summary>
        /// Gets the agent A.
        /// </summary>
        public static Agent AgentA { get; private set; }

        /// <summary>
        /// Gets the agent B.
        /// </summary>
        public static Agent AgentB { get; private set; }

        /// <summary>
        /// The main method.
        /// </summary>
        /// <param name="args"> The args. </param>
        public static void Main(string[] args)
        {
            string[] sa;

            // Read map size from input
            int h, w;
            while (true)
            {
                Console.WriteLine("Please enter map width and height in the format \"x,y\": ");
                var s = Console.ReadLine();
                sa = s.Split(',');

                if (sa.Length == 2)
                {
                    break;
                }
            }

            // Default sizes to 15 if invalid input
            MapWidth = int.TryParse(sa[0], out w) ? w : 15;
            MapHeight = int.TryParse(sa[1], out h) ? h : 15;
            
            Map = new Node[MapWidth, MapHeight];
            var rand = new Random();

            // Initialise the map nodes
            for (var x = 0; x < MapWidth; x++)
            {
                for (var y = 0; y < MapHeight; y++)
                {
                    Map[x, y] = new Node(x, y);
                    if (rand.NextDouble() < 0.1)
                    {
                        Map[x, y].Cost = -1;
                    }
                }
            }

            // Read values for Agent A
            while (true)
            {
                Console.WriteLine("Please enter Agent A's values in the format \"xPosition,yPosition,speed\": ");
                var s = Console.ReadLine();
                sa = s.Split(',');

                if (sa.Length == 3)
                {
                    break;
                }
            }

            // Initialise Agent A
            int xpos, ypos, speed;
            xpos = int.TryParse(sa[0], out xpos) ? xpos : 0;
            ypos = int.TryParse(sa[1], out ypos) ? ypos : 0;
            speed = int.TryParse(sa[2], out speed) ? speed : 1;
            AgentA = new Agent(xpos, ypos, speed);

            // Read values for Agent B
            while (true)
            {
                Console.WriteLine("Please enter Agent B's values in the format \"xPosition,yPosition,speed\": ");
                var s = Console.ReadLine();
                sa = s.Split(',');

                if (sa.Length == 3)
                {
                    break;
                }
            }

            // Initialise Agent B
            xpos = int.TryParse(sa[0], out xpos) ? xpos : 0;
            ypos = int.TryParse(sa[1], out ypos) ? ypos : 0;
            speed = int.TryParse(sa[2], out speed) ? speed : 1;

            AgentB = new Agent(xpos, ypos, speed);

            Map[AgentA.X, AgentA.Y].Cost = 1;
            Map[AgentB.X, AgentB.Y].Cost = 1;

            // Draw initial map
            DrawMap();

            // While B has not been caught
            while (!(AgentA.X == AgentB.X && AgentA.Y == AgentB.Y))
            {
                Console.WriteLine("Press <enter> to continue the simulation.");
                Console.ReadLine();

                int movement;
                Node p;

                if (DistanceBetweenAgents(AgentB, AgentA) < AgentA.Speed * 2)
                {
                    // Move B first
                    for (var s = 0; s < AgentB.Speed; s++)
                    {
                        AgentB.CurrentPath = MoveB();
                        movement = AgentB.Speed <= AgentB.CurrentPath.Count - 1
                                           ? AgentB.Speed
                                           : AgentB.CurrentPath.Count - 1;
                        p = AgentB.CurrentPath[movement];

                        AgentB.X = p.X;
                        AgentB.Y = p.Y;
                    }
                }

                // Move A
                AgentA.CurrentPath = MoveA();
                if (AgentA.CurrentPath == null || AgentA.CurrentPath.Count == 0)
                {
                    Console.WriteLine("Agent A could not move - he is blocked in.");
                    Console.ReadLine();
                    return;
                }

                movement = AgentA.Speed <= AgentA.CurrentPath.Count - 1 ? AgentA.Speed : AgentA.CurrentPath.Count - 1;
                p = AgentA.CurrentPath[movement];

                AgentA.X = p.X;
                AgentA.Y = p.Y;

                // Draw map after movement
                DrawMap();
            }

            Console.WriteLine("Agent B has been caught by Agent A. The arrest happened at X:" + AgentA.X + ", Y:" + AgentA.Y + ".");
            Console.ReadLine();
        }

        /// <summary>
        /// Calculates Agent A's movement according to A* algorithm.
        /// </summary>
        /// <returns>
        /// The <see cref="List"/> The list of nodes making up the new path of Agent A.
        /// </returns>
        public static List<Node> MoveA()
        {
            var a = Map[AgentA.X, AgentA.Y];
            var b = Map[AgentB.X, AgentB.Y];
            return AStar.Algorithm(a, b);
        }

        /// <summary>
        /// Calculates Agent B's movement away from Agent A.
        /// </summary>
        /// <returns>
        /// The <see cref="List"/>The list of nodes making up the new path of Agent B.
        /// </returns>
        public static List<Node> MoveB()
        {
            var nodes = new List<Node>();

            var newX = AgentB.X > AgentA.X ? AgentB.X + 1 : AgentB.X - 1;
            var newY = AgentB.Y > AgentA.Y ? AgentB.Y + 1 : AgentB.Y - 1;

            newX = 0 <= newX && newX < MapWidth ? newX : AgentB.X;
            newY = 0 <= newY && newY < MapHeight ? newY : AgentB.Y;

            nodes.Add(Map[newX, newY]);
            return nodes;
        }

        /// <summary>
        /// Draws the map to the console. Blocked obstacles are marked with an X.
        /// </summary>
        public static void DrawMap()
        {
            Console.Clear();

            for (var y = 0; y < MapHeight; y++)
            {
                Console.Write("+");

                for (var x = 0; x < MapWidth; x++)
                {
                    Console.Write("-+");
                }
                
                Console.WriteLine();

                Console.Write("|");

                for (var x = 0; x < MapWidth; x++)
                {
                    if (AgentA.X == x && AgentA.Y == y)
                    {
                        Console.Write("A");
                    }
                    else if (AgentB.X == x && AgentB.Y == y)
                    {
                        Console.Write("B");
                    }
                    else if (Map[x, y].Cost == -1)
                    {
                        Console.Write("X");
                    }
                    else
                    {
                        Console.Write(" ");
                    }

                    Console.Write("|");
                }

                Console.WriteLine();
            }

            Console.Write("+");
            
            for (var x = 0; x < MapWidth; x++)
            {
                Console.Write("-+");
            }

            Console.WriteLine();
        }

        /// <summary>
        /// The distance between agents in the world.
        /// </summary>
        /// <param name="a">The agent A.</param>
        /// <param name="b">The agent B.</param>
        /// <returns>The distance between agents.</returns>
        public static double DistanceBetweenAgents(Agent a, Agent b)
        {
            return Math.Sqrt(Math.Pow(b.X - a.X, 2) + Math.Pow(b.Y - a.Y, 2));
        }
    }
}
