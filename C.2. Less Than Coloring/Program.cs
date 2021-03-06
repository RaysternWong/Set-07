using System;
using System.Collections.Generic;
using System.Linq;

namespace LessThanColoring
{
    /**
     * Less Than Coloring
     *
     * Given a graph where each Node has property called Power and Color.
     * Each node can only colors its neighbours if it has equal or greater Power than them.
     * Each node will color the neighbours after they are completely colored.
     * The coloring process for each node will occur concurrently and takes 1 day.
     *
     * ================================ ATTENTION PLEASE ================================
     *
     * Your ONLY task is to implement the following two methods:
     * 1. FindMinimumDays
     * 2. FindMostColor
     *
     * You are ALLOWED to
     * 1. Add new method(s) in this file.
     * 2. Add additional test case(s) in Main.
     *
     * You are NOT ALLOWED to
     * 1. Add any new method in other files.
     * 2. Change signature of any existing methods.
     *    Method signature includes
     *    - method's name
     *    - return type
     *    - number of parameters
     *    - parameters' type
     *    - access modifier
     *
     * --- Please make sure your code is error-free when built.
     *
     * Note: Do not count the time needed for coloring the starting node
     */
    public class Program
    {
        public static void Main(string[] args)
        {
            var graph = _GenerateGraphA();
            var nodeA = _GetNode("Node A", graph);
            nodeA.Color = Color.Red;
            Console.WriteLine(FindMinimumDays(nodeA));

            var nodeK = _GetNode("Node K", graph);
            nodeA.Color = Color.Red;
            Console.WriteLine(FindMinimumDays(nodeK));

            graph = _GenerateGraphB();
            nodeA = _GetNode("Node A", graph);
            nodeA.Color = Color.Red;
            var nodeZ = _GetNode("Node Z", graph);
            nodeZ.Color = Color.Blue;
            Console.WriteLine(FindMostColor(nodeA, nodeZ, 2));

            graph = _GenerateGraphB();
            nodeA = _GetNode("Node A", graph);
            nodeA.Color = Color.Blue;
            nodeZ = _GetNode("Node Z", graph);
            nodeZ.Color = Color.Red;
            Console.WriteLine(FindMostColor(nodeA, nodeZ, 7));

            graph = _GenerateGraphB();
            var nodeR = _GetNode("Node R", graph);
            nodeR.Color = Color.Red;
            var nodeU = _GetNode("Node U", graph);
            nodeU.Color = Color.Blue;
            Console.WriteLine(FindMostColor(nodeR, nodeU, 2));

            graph = _GenerateGraphB();
            nodeR = _GetNode("Node R", graph);
            nodeR.Color = Color.Blue;
            nodeZ = _GetNode("Node Z", graph);
            nodeZ.Color = Color.Red;
            Console.WriteLine(FindMostColor(nodeR, nodeZ, 2));


            graph = _GenerateGraphB();
            var nodeL = _GetNode("Node L", graph);
            nodeL.Color = Color.Blue;
            nodeZ = _GetNode("Node Z", graph);
            nodeZ.Color = Color.Red;
            Console.WriteLine(FindMostColor(nodeL, nodeZ, 1));
        }

        /**
         * Find Minimum Days
         *
         * Return minimum days required to color entire graph.
         * Return 0 if graph cannot colored entirely.
         *
         * Example (See GraphA.png):
         * start node: Node A
         * output: 3 days
         * explanation:
         *  day 1, A colors B, E, H
         *  day 2, B colors C, F
         *         E colors I
         *         H colors K
         *  day 3, C colors D
         *         F colors G, I
         *         I colors J
         */
        public static int FindMinimumDays(Node startNode)
        {
            int days = 0;

            List<Node> passedNodes = new List<Node>() { startNode };
            List<Node> tomorrowNodes = GetTomorrowNodes(startNode, passedNodes);
            
            while (tomorrowNodes?.Count > 0)
            {
                days++;
                tomorrowNodes = GetTomorrowNodes(tomorrowNodes, passedNodes);
            }

            return days;
        }

        private static List<Node> GetTomorrowNodes(List<Node> todayNodes, List<Node> passedNodes)
        {
            List<Node> tomorrowNodes = new List<Node>();

            foreach (Node node in todayNodes)
            {
                tomorrowNodes.AddRange(GetTomorrowNodes(node, passedNodes));
            }
 
            return tomorrowNodes;
        }

        private static List<Node> GetTomorrowNodes(Node node, List<Node> passedNodes)
        {
            var nodes = node.Neighbours.Except(passedNodes).Where(n => n.Power <= node.Power).ToList();
            passedNodes.AddRange(nodes);

            return nodes;
        }

        /**
         * If we color 2 nodes at the same time then the color will spread for a certain period of time, find which color colored the most nodes.
         * Rule:
         * - Blank + Red = Red
         * - Red + Red = Red
         * - Blank + Blue = Blue
         * - Blue + Blue = Blue
         * - Red + Blue = Mixed (applies vice versa)
         * - Mixed + Red = Mixed (applies vice versa)
         * - Mixed + Blue = Mixed (applies vice versa)
         *
         * Example (See GraphB.png):
         * node 1: node A (Red)
         * node 2: node Z (Blue)
         * coloring duration: 2
         * output: Red
         * explanation:
         *  day 1, A colors B, C, D
         *         Z colors Q, Y
         *  day 2, B colors F, G
         *         C colors G, I
         *         Q colors P
         *         Y colors X
         *  result: Red = 7 (A, B, C, D, F, G, I)
         *          Blue = 5 (P, Q, X, Y, Z)
         */
        public static Color FindMostColor(Node node1, Node node2, int coloringDuration)
        {
            List<Node> passedNodesFrom1 = new List<Node>() { node1 };
            List<Node> passedNodesFrom2 = new List<Node>() { node2 };

            List<Node> tomorrowNodesFrom1 = new List<Node>() { node1 };
            List<Node> tomorrowNodesFrom2 = new List<Node>() { node2 };

            for (int i=0; i < coloringDuration; i++)
            {
                tomorrowNodesFrom1 = GetAndColoringTomorrowNodes(tomorrowNodesFrom1, passedNodesFrom1);
                tomorrowNodesFrom2 = GetAndColoringTomorrowNodes(tomorrowNodesFrom2, passedNodesFrom2);
            }

            passedNodesFrom1.AddRange(passedNodesFrom2);

            Color mostColor = passedNodesFrom1
                                .GroupBy(n => n.Color)
                                .OrderByDescending(grp => grp.Count())
                                .First().Key;

            return mostColor;
        }

        private static List<Node> GetAndColoringTomorrowNodes(List<Node> todayNodes, List<Node> passedNodes)
        {
            List<Node> tomorrowNodes = new List<Node>();

            foreach (Node node in todayNodes)
            {
                List<Node> nodes = GetAndColoringTomorrowNodes(node, passedNodes);
                tomorrowNodes.AddRange(nodes);       
            }

            return tomorrowNodes;
        }

        private static List<Node> GetAndColoringTomorrowNodes(Node node, List<Node> passedNodes)
        {
            var tomorrowNode = node.Neighbours
                .Except(passedNodes)
                .Where(n => n.Power <= node.Power).ToList();

            tomorrowNode.ForEach(n => n.Color = GetNewColor(n.Color, node.Color));


            passedNodes.AddRange(tomorrowNode);

            return tomorrowNode;
        }

        private static Color GetNewColor(Color currentColor, Color comingColor)
        {
            if (currentColor == comingColor)
                return currentColor;

            if (currentColor == Color.Blank)
                return comingColor;

            if (comingColor == Color.Blank)
                return currentColor;

            return Color.Mixed;
        }

        private static List<Node> _GenerateGraphA()
        {
            var graph = new List<Node>();

            var nodeA = new Node { Name = "Node A", Power = 6 };
            var nodeB = new Node { Name = "Node B", Power = 6 };
            var nodeC = new Node { Name = "Node C", Power = 6 };
            var nodeD = new Node { Name = "Node D", Power = 6 };
            var nodeE = new Node { Name = "Node E", Power = 5 };
            var nodeF = new Node { Name = "Node F", Power = 5 };
            var nodeG = new Node { Name = "Node G", Power = 5 };
            var nodeH = new Node { Name = "Node H", Power = 4 };
            var nodeI = new Node { Name = "Node I", Power = 4 };
            var nodeJ = new Node { Name = "Node J", Power = 3 };
            var nodeK = new Node { Name = "Node K", Power = 2 };

            graph.Add(nodeA);
            graph.Add(nodeB);
            graph.Add(nodeC);
            graph.Add(nodeD);
            graph.Add(nodeE);
            graph.Add(nodeF);
            graph.Add(nodeG);
            graph.Add(nodeH);
            graph.Add(nodeI);
            graph.Add(nodeJ);
            graph.Add(nodeK);

            nodeA.MakeNeighbour(nodeB);
            nodeA.MakeNeighbour(nodeE);
            nodeA.MakeNeighbour(nodeH);
            nodeB.MakeNeighbour(nodeC);
            nodeB.MakeNeighbour(nodeF);
            nodeC.MakeNeighbour(nodeD);
            nodeD.MakeNeighbour(nodeG);
            nodeE.MakeNeighbour(nodeI);
            nodeE.MakeNeighbour(nodeH);
            nodeF.MakeNeighbour(nodeI);
            nodeF.MakeNeighbour(nodeG);
            nodeG.MakeNeighbour(nodeJ);
            nodeH.MakeNeighbour(nodeK);
            nodeI.MakeNeighbour(nodeJ);
            nodeJ.MakeNeighbour(nodeK);

            return graph;
        }

        private static List<Node> _GenerateGraphB()
        {
            var graph = new List<Node>();

            var nodeA = new Node { Name = "Node A", Power = 6 };
            var nodeB = new Node { Name = "Node B", Power = 6 };
            var nodeC = new Node { Name = "Node C", Power = 6 };
            var nodeD = new Node { Name = "Node D", Power = 6 };
            var nodeE = new Node { Name = "Node E", Power = 5 };
            var nodeF = new Node { Name = "Node F", Power = 5 };
            var nodeG = new Node { Name = "Node G", Power = 5 };
            var nodeH = new Node { Name = "Node H", Power = 5 };
            var nodeI = new Node { Name = "Node I", Power = 5 };
            var nodeJ = new Node { Name = "Node J", Power = 5 };
            var nodeK = new Node { Name = "Node K", Power = 5 };
            var nodeL = new Node { Name = "Node L", Power = 5 };
            var nodeM = new Node { Name = "Node M", Power = 5 };
            var nodeN = new Node { Name = "Node N", Power = 5 };
            var nodeO = new Node { Name = "Node O", Power = 5 };
            var nodeP = new Node { Name = "Node P", Power = 5 };
            var nodeQ = new Node { Name = "Node Q", Power = 6 };
            var nodeR = new Node { Name = "Node R", Power = 6 };
            var nodeS = new Node { Name = "Node S", Power = 6 };
            var nodeT = new Node { Name = "Node T", Power = 5 };
            var nodeU = new Node { Name = "Node U", Power = 5 };
            var nodeV = new Node { Name = "Node V", Power = 5 };
            var nodeW = new Node { Name = "Node W", Power = 5 };
            var nodeX = new Node { Name = "Node X", Power = 5 };
            var nodeY = new Node { Name = "Node Y", Power = 6 };
            var nodeZ = new Node { Name = "Node Z", Power = 6 };

            graph.Add(nodeA);
            graph.Add(nodeB);
            graph.Add(nodeC);
            graph.Add(nodeD);
            graph.Add(nodeE);
            graph.Add(nodeF);
            graph.Add(nodeG);
            graph.Add(nodeH);
            graph.Add(nodeI);
            graph.Add(nodeJ);
            graph.Add(nodeK);
            graph.Add(nodeL);
            graph.Add(nodeM);
            graph.Add(nodeN);
            graph.Add(nodeO);
            graph.Add(nodeP);
            graph.Add(nodeQ);
            graph.Add(nodeR);
            graph.Add(nodeS);
            graph.Add(nodeT);
            graph.Add(nodeU);
            graph.Add(nodeV);
            graph.Add(nodeW);
            graph.Add(nodeX);
            graph.Add(nodeY);
            graph.Add(nodeZ);

            nodeA.MakeNeighbour(nodeB);
            nodeA.MakeNeighbour(nodeC);
            nodeA.MakeNeighbour(nodeD);
            nodeB.MakeNeighbour(nodeF);
            nodeB.MakeNeighbour(nodeG);
            nodeC.MakeNeighbour(nodeG);
            nodeC.MakeNeighbour(nodeI);
            nodeF.MakeNeighbour(nodeK);
            nodeG.MakeNeighbour(nodeL);
            nodeH.MakeNeighbour(nodeI);
            nodeH.MakeNeighbour(nodeM);
            nodeH.MakeNeighbour(nodeN);
            nodeI.MakeNeighbour(nodeJ);
            nodeI.MakeNeighbour(nodeO);
            nodeJ.MakeNeighbour(nodeP);
            nodeK.MakeNeighbour(nodeL);
            nodeK.MakeNeighbour(nodeS);
            nodeL.MakeNeighbour(nodeM);
            nodeL.MakeNeighbour(nodeS);
            nodeM.MakeNeighbour(nodeT);
            nodeM.MakeNeighbour(nodeV);
            nodeN.MakeNeighbour(nodeO);
            nodeO.MakeNeighbour(nodeP);
            nodeO.MakeNeighbour(nodeV);
            nodeO.MakeNeighbour(nodeW);
            nodeP.MakeNeighbour(nodeQ);
            nodeQ.MakeNeighbour(nodeZ);
            nodeR.MakeNeighbour(nodeS);
            nodeS.MakeNeighbour(nodeT);
            nodeT.MakeNeighbour(nodeM);
            nodeV.MakeNeighbour(nodeW);
            nodeW.MakeNeighbour(nodeX);
            nodeX.MakeNeighbour(nodeY);
            nodeY.MakeNeighbour(nodeZ);

            return graph;
        }

        private static Node _GetNode(string name, List<Node> nodes)
        {
            return nodes.Find(x => x.Name == name);
        }
    }
}
