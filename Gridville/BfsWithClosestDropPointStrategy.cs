namespace Gridville
{
    internal sealed class BfsWithClosestDropPointStrategy : IRouteSearchStrategy
    {
        public bool TryFindShortestRoute(
            CityGrid cityGrid,
            GridPoint startPoint,
            GridPoint destinationPoint,
            out Route? route)
        {
            var foundDirectRoute = false;
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

            // In addition to the other strategy, here we also keep track of the minimum distance point to the destination
            // and the path to it. If no direct route to the destination point is found, we will at least have this
            // closest drop point available.
            var minDistanceToDestination = int.MaxValue;
            var traversalNodeForClosestDropPoint = traversalNode;

            while (traversalNodesQueue.Any())
            {
                traversalNode = traversalNodesQueue.Dequeue();

                if (traversalNode.GraphNode.GridPoint == destinationNode?.GridPoint)
                {
                    foundDirectRoute = true;
                    break;
                }

                var distance = traversalNode.GraphNode.GridPoint.DistanceTo(destinationPoint);
                if (distance < minDistanceToDestination)
                {
                    minDistanceToDestination = distance;
                    traversalNodeForClosestDropPoint = traversalNode;
                }

                foreach (var neighbor in traversalNode.GraphNode.Neighbors.Where(n => !visitedNodes.Contains(n)))
                {
                    visitedNodes.Add(neighbor);
                    traversalNodesQueue.Enqueue(new TraversalNode(neighbor, traversalNode));
                }
            }

            var resultTraversalNode = foundDirectRoute ? traversalNode : traversalNodeForClosestDropPoint;

            var gridPoints = new List<GridPoint>();

            foreach (var graphNode in resultTraversalNode.TraverseBackToTopParent())
            {
                gridPoints.Insert(0, graphNode.GridPoint);
            }

            route = new Route(gridPoints);

            return true;
        }
    }
}
