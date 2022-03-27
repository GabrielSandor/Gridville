namespace Gridville
{
    internal sealed class TraversalNode
    {
        public TraversalNode? Parent { get; }

        public GraphNode GraphNode { get; }

        public TraversalNode(GraphNode graphNode, TraversalNode? parent)
            : this(graphNode)
        {
            Parent = parent;
        }

        public TraversalNode(GraphNode node)
        {
            GraphNode = node;
        }

        public IEnumerable<GraphNode> TraverseBackToTopParent()
        {
            var traversalNode = this;

            do
            {
                yield return traversalNode.GraphNode;
                traversalNode = traversalNode.Parent;
            } while (traversalNode != null);
        }
    }
}
