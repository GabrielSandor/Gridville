namespace Gridville
{
    internal sealed class Route
    {
        private readonly List<GridPoint> _points;

        public IEnumerable<GridPoint> Points => _points;

        public int Length => _points.Count - 1;

        public Route(List<GridPoint> points)
        {
            _points = points;
        }
    }
}
