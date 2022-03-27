namespace Gridville
{
    internal static class GraphUtils
    {
        public static (GraphNode StartNode, GraphNode DestinationNode) ToGraphNodes(
            CityGrid cityGrid,
            GridPoint startPoint,
            GridPoint destinationPoint)
        {
            var trafficJamPoints = new HashSet<GridPoint>(cityGrid.TrafficJams);
            GraphNode? startNode = null, destinationNode = null;

            var graphNodes = new List<GraphNode>();

            var neighborsMatrix = new GraphNode[cityGrid.Width + 1, cityGrid.Height + 1];

            for (int i = 0; i <= cityGrid.Width; i++)
            {
                for (int j = 0; j <= cityGrid.Height; j++)
                {
                    var point = new GridPoint(i, j);
                    if (!trafficJamPoints.Contains(point))
                    {
                        var node = new GraphNode(point);
                        graphNodes.Add(node);

                        if (point == startPoint)
                        {
                            startNode = node;
                        }

                        if (point == destinationPoint)
                        {
                            destinationNode = node;
                        }

                        neighborsMatrix[i, j] = node;

                        if (i > 0)
                        {
                            var westNeighbor = neighborsMatrix[i - 1, j];
                            if (westNeighbor != null)
                            {
                                node.AddNeighbor(westNeighbor);
                                westNeighbor.AddNeighbor(node);
                            }
                        }

                        if (j > 0)
                        {
                            var southNeighbor = neighborsMatrix[i, j - 1];
                            if (southNeighbor != null)
                            {
                                node.AddNeighbor(southNeighbor);
                                southNeighbor.AddNeighbor(node);
                            }
                        }
                    }
                }
            }

            return (startNode!, destinationNode!);
        }
    }
}
