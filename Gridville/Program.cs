using Gridville;

var routeSearchStrategy = new BfsWithClosestDropPointStrategy();
var navigator = new Navigator(routeSearchStrategy);

var trafficJams = new List<GridPoint>
            {
                new(0, 3), new(1, 3), new(2, 3), new(3, 3), new(4, 3), new(5, 3),
                new(5, 4), new(5, 2), new(5, 1)
            };

var cityGrid = new CityGrid(6, 6, trafficJams);
var startPoint = new GridPoint(0, 0);
var destinationPoint = new GridPoint(5, 5);

if (navigator.TryFindShortestRoute(cityGrid, startPoint, destinationPoint, out var route))
{
    Console.WriteLine($"Found the following route of length {route!.Length}: ");
    foreach (var gridPoint in route.Points)
    {
        Console.Write($"{gridPoint} ");
    }
}
else
{
    Console.WriteLine("Unable to find a route to the destination.");
}

Console.ReadLine();
