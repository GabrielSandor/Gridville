namespace Gridville
{
    internal sealed class CityGrid
    {
        public int Width { get; }

        public int Height { get; }

        public IEnumerable<GridPoint> TrafficJams { get; }

        public CityGrid(int width, int height, IEnumerable<GridPoint> trafficJams)
        {
            Width = width > 0 ? width : throw new ArgumentOutOfRangeException(nameof(width));
            Height = height > 0 ? height : throw new ArgumentOutOfRangeException(nameof(height));

            if (trafficJams.Any(p => !IsWithinBounds(p)))
            {
                throw new ArgumentOutOfRangeException(nameof(trafficJams));
            }

            TrafficJams = trafficJams;
        }

        public CityGrid(int width, int height)
            : this(width, height, Enumerable.Empty<GridPoint>())
        {
        }

        public bool IsWithinBounds(GridPoint gridPoint) =>
            gridPoint.Y <= Height && gridPoint.X <= Width;
    }
}
