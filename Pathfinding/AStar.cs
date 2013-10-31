namespace Pathfinding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The A* algorithm.
    /// Implementation loosely based on A* pseudo code from http://web.mit.edu/eranki/www/tutorials/search/
    /// </summary>
    public class AStar
    {
        /// <summary>
        /// The open list.
        /// </summary>
        private static List<Node> open;

        /// <summary>
        /// The closed list.
        /// </summary>
        private static List<Node> closed;

        /// <summary>
        /// A* algorithm - takes a starting + target node and calculates the fastest route between the two nodes.
        /// </summary>
        /// <param name="start">The starting node.</param>
        /// <param name="target">The target node.</param>
        /// <returns>The <see cref="List"/> containing the path from starting node to target node.</returns>
        public static List<Node> Algorithm(Node start, Node target)
        {
            for (var x = 0; x < Program.MapWidth; x++)
            {
                for (var y = 0; y < Program.MapHeight; y++)
                {
                    Program.Map[x, y].Parent = null;
                }
            }

            // (1) initialize the open list
            open = new List<Node>();

            // (2) initialize the closed list
            closed = new List<Node>();

            // (3) put the starting node on the open list (you can leave its f at zero)
            AddToOpenList(start);

            // (4) while the open list is not empty
            while (open.Count > 0)
            {
                // (5) find the node with the least f on the open list, call it "q"
                var q = open[0];
                
                // (6) pop q off the open list
                open.RemoveAt(0);

                // (7) generate q's 8 successors
                var successors = q.GetSuccessors().Where(succesor => succesor.Cost > -1);
                
                // (8) for each succesor
                foreach (var successor in successors)
                {
                    // (9) if successor is the goal, stop the search
                    if (successor.Equals(target))
                    {
                        successor.Parent = q;
                        return ReconstructPath(successor);
                    }

                    // (10) successor.g = q.g + distance between successor and q
                    var g = q.G + successor.Cost;

                    // (11) successor.h = distance from goal to successor
                    var h = Node.DistanceBetweenNodes(successor, target); 

                    // (12) successor.f = successor.g + successor.h
                    var f = g + h;

                    // (13 & 14) if successor is not in open or closed list
                    if (closed.Contains(successor) && f >= successor.F)
                    {
                        continue;
                    }

                    if (!open.Contains(successor))
                    {
                        successor.F = f;
                        successor.G = g;
                        successor.Parent = q;
                        AddToOpenList(successor);
                        continue;
                    }
                    
                    // (15) otherwise, add the node to the open list
                    if (open.Contains(successor))
                    {
                        if (f < successor.F)
                        {
                            successor.F = f;
                            successor.G = g;
                            successor.Parent = q;
                        }
                    }
                }

                // (17) push q on the closed list
                AddToCLosedList(q);
            }

            return null;
        }

        /// <summary>
        /// Reconstructs a path based on an end node and its parents.
        /// </summary>
        /// <param name="endNode">The end node of the path to reconstruct.</param>
        /// <returns>The <see cref="List"/> containing the path.</returns>
        private static List<Node> ReconstructPath(Node endNode)
        {
            var nodes = new List<Node>();
            
            while (endNode != null)
            {
                nodes.Add(endNode);
                endNode = endNode.Parent;
            }

            nodes.Reverse();
            return nodes;
        }

        /// <summary>
        /// Adds a node to the closed list (sorted lowest to highest based on the f-value of the node).
        /// </summary>
        /// <param name="n">The node to add.</param>
        private static void AddToCLosedList(Node n)
        {
            for (var i = 0; i < closed.Count; i++)
            {
                if (n.F < closed[i].F)
                {
                    closed.Insert(i, n);
                    return;
                }
            }

            closed.Add(n);
        }

        /// <summary>
        /// Adds a node to the open list (sorted lowest to highest based on the f-value of the node).
        /// </summary>
        /// <param name="n">The node to add.</param>
        private static void AddToOpenList(Node n)
        {
            for (var i = 0; i < open.Count; i++)
            {
                if (n.F < open[i].F)
                {
                    open.Insert(i, n);
                    return;
                }
            }

            open.Add(n);
        }
    }
}
