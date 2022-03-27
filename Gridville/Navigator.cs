namespace Gridville
{
    internal sealed class Navigator
    {
        private readonly IRouteSearchStrategy _routeSearchStrategy;

        public Navigator(IRouteSearchStrategy routeSearchStrategy)
        {
            _routeSearchStrategy = routeSearchStrategy;
        }

        public bool TryFindShortestRoute(
            CityGrid cityGrid,
            GridPoint startPoint,
            GridPoint destinationPoint,
            out Route? route)
        {
            if (startPoint == destinationPoint)
            {
                throw new ArgumentException("Start point cannot coincide with the destination point.");
            }

            return _routeSearchStrategy.TryFindShortestRoute(cityGrid, startPoint, destinationPoint, out route);
        }
    }
}
