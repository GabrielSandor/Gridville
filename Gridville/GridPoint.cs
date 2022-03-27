namespace Gridville
{
    internal readonly struct GridPoint
    {
        public int X { get; }

        public int Y { get; }

        public GridPoint(int x, int y) : this()
        {
            X = x >= 0 ? x : throw new ArgumentOutOfRangeException(nameof(x));
            Y = y >= 0 ? y : throw new ArgumentOutOfRangeException(nameof(y));
        }

        public int DistanceTo(GridPoint other) => Math.Abs(X - other.X) + Math.Abs(Y - other.Y);


        public override string ToString() => $"({X}, {Y})";

        public override bool Equals(object? obj)
        {
            return obj is GridPoint point &&
                   X == point.X &&
                   Y == point.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public static bool operator ==(GridPoint left, GridPoint right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(GridPoint left, GridPoint right)
        {
            return !(left == right);
        }
    }
}
