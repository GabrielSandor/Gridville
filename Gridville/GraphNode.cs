namespace Gridville
{
    internal sealed class GraphNode
    {
        private readonly List<GraphNode> _neighbors;

        public GridPoint GridPoint { get; }

        public IEnumerable<GraphNode> Neighbors => _neighbors;

        public GraphNode(GridPoint gridPoint)
        {
            GridPoint = gridPoint;
            _neighbors = new List<GraphNode>();
        }

        public void AddNeighbor(GraphNode node)
        {
            _neighbors.Add(node);
        }
    }
}
