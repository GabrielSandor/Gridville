namespace Gridville
{
    /// <summary>
    /// Applies the breadth-first search (BFS) algorithm to find the shortest path for the city grid, which can be
    /// seen as essentially an undirected and unweighted graph: https://en.wikipedia.org/wiki/Shortest_path_problem#Unweighted_graphs.
    /// 
    /// There may be other better-performing algorithms such as Dijkstra's and A-star, which are also more general, however
    /// for the problem at hand BFS is simple and gives the correct solution reliably.
    /// </summary>
    internal sealed class BreadthFirstSearchRouteStrategy : IRouteSearchStrategy
    {
        public bool TryFindShortestRoute(
            CityGrid cityGrid,
            GridPoint startPoint,
            GridPoint destinationPoint,
            out Route? route)
        {
            var foundRoute = false;
            route = null;

            // First, "transform" the grid into a graph by keeping the list of neighbors for each node in the grid.
            // If there is a path from the start node to the destination node via the intermediary nodes, the algorithm
            // will find it.
            var (startNode, destinationNode) = GraphUtils.ToGraphNodes(cityGrid, startPoint, destinationPoint);
            if (startNode == null || destinationNode == null)
            {
                // no solution, the start point or destination point is itself part of the traffic jam
                return false;
            }

            var visitedNodes = new HashSet<GraphNode>();
            var traversalNodesQueue = new Queue<TraversalNode>();

            // the traversal node helps with keeping the "parent" (previously visited neighbor) node, so that the route can be
            // reconstructed backwards
            var traversalNode = new TraversalNode(startNode);
            traversalNodesQueue.Enqueue(traversalNode);
            visitedNodes.Add(startNode);

            // standard BFS algorithm for graphs (https://en.wikipedia.org/wiki/Breadth-first_search), marking
            // the already visited nodes to avoid infinite cycles
            while (traversalNodesQueue.Any())
            {
                traversalNode = traversalNodesQueue.Dequeue();

                if (traversalNode.GraphNode.GridPoint == destinationNode.GridPoint)
                {
                    foundRoute = true;
                    break;
                }

                foreach (var neighbor in traversalNode.GraphNode.Neighbors.Where(n => !visitedNodes.Contains(n)))
                {
                    visitedNodes.Add(neighbor);
                    traversalNodesQueue.Enqueue(new TraversalNode(neighbor, traversalNode));
                }
            }

            if (foundRoute)
            {
                var gridPoints = new List<GridPoint>();

                foreach (var graphNode in traversalNode.TraverseBackToTopParent())
                {
                    gridPoints.Insert(0, graphNode.GridPoint);
                }

                route = new Route(gridPoints);
            }

            return foundRoute;
        }
    }
}
