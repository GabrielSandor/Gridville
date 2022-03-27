namespace Gridville
{
    internal interface IRouteSearchStrategy
    {
        bool TryFindShortestRoute(CityGrid cityGrid, GridPoint startPoint, GridPoint destinationPoint, out Route? route);
    }
}