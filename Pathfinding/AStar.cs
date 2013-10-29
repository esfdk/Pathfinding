namespace Pathfinding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security;

    public class AStar
    {
        private static List<Node> open;

        private static List<Node> closed;

        public static List<Node> Algorithm(Node start, Node target)
        {
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
                var successors = q.GetSuccessors();
                
                // (8) for each succesor
                foreach (var successor in successors)
                {
                    // (9) if successor is the goal, stop the search
                    if (successor.Equals(target))
                    {
                        successor.parent = q;
                        return reconstructPath(successor);
                    }

                    // (10) successor.g = q.g + distance between successor and q
                    var g = q.g + successor.cost;

                    // (11) successor.h = distance from goal to successor
                    var h = DistanceBetweenNodes(successor, target); 

                    // (12) successor.f = successor.g + successor.h
                    var f = g + h;

                    // (13 & 14) if new f is lower than to successor.f
                    if (f <= successor.f)
                    {
                        successor.f = f;
                        successor.g = g;
                        successor.parent = q;
                    }

                    // (13 & 14) if successor is not in open or closed list
                    if (!(open.Contains(successor) || closed.Contains(successor)))
                    {
                        // (15) otherwise, add the node to the open list
                        successor.parent = q;
                        AddToOpenList(successor);
                    }
                }

                // (17) push q on the closed list
                AddToCLosedList(q);
            }
        }

        private static List<Node> reconstructPath(Node end)
        {
            var list = new List<Node>();
            list = reconstructPath(list, end);
            list.Reverse();
            return list;
        }

        private static List<Node> reconstructPath(List<Node> nodes, Node current)
        {
            if (current == null)
            {
                return nodes;
            }

            nodes.Add(current);
            return reconstructPath(nodes, current.parent);
        }

        private static float DistanceBetweenNodes(Node a, Node b)
        {
            return (float) Math.Sqrt(Math.Pow((b.x - a.x), 2) + Math.Pow((b.y - a.y), 2));
        }

        private static void AddToCLosedList(Node n)
        {
            for (var i = 0; i < closed.Count; i++)
            {
                if (n.f < closed[i].f)
                {
                    closed.Insert(i, n);
                    return;
                }
            }

            closed.Add(n);
        }

        private static void AddToOpenList(Node n)
        {
            for (var i = 0; i < open.Count; i++)
            {
                if (n.f < open[i].f)
                {
                    open.Insert(i, n);
                    return;
                }
            }

            open.Add(n);
        }
    }
}
