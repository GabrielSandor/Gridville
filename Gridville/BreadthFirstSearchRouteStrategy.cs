namespace Gridville
{
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

            var (startNode, destinationNode) = GraphUtils.ToGraphNodes(cityGrid, startPoint, destinationPoint);
            if (startNode == null)
            {
                return false;
            }

            var visitedNodes = new HashSet<GraphNode>();
            var traversalNodesQueue = new Queue<TraversalNode>();

            var traversalNode = new TraversalNode(startNode);
            traversalNodesQueue.Enqueue(traversalNode);
            visitedNodes.Add(startNode);

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
