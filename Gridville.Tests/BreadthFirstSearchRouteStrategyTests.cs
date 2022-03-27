using NUnit.Framework;
using System.Collections.Generic;

namespace Gridville.Tests
{
    [TestFixture]
    internal class BreadthFirstSearchRouteStrategyTests
    {
        [Test]
        public void NoTrafficJam()
        {
            var cityGrid = new CityGrid(3, 3);
            var startPoint = new GridPoint(0, 0);
            var destinationPoint = new GridPoint(3, 2);

            var strategy = new BreadthFirstSearchRouteStrategy();

            var foundRoute = strategy.TryFindShortestRoute(cityGrid, startPoint, destinationPoint, out var route);
            Assert.IsTrue(foundRoute);
            Assert.AreEqual(5, route!.Length);
        }

        [Test]
        public void TrafficJam()
        {
            var trafficJams = new List<GridPoint>
            {
                new(0, 3), new(1, 3), new(2, 3), new(3, 3), new(4, 3), new(5, 3),
                new(5, 4), new(5, 2), new(5, 1)
            };

            var cityGrid = new CityGrid(6, 6, trafficJams);
            var startPoint = new GridPoint(0, 0);
            var destinationPoint = new GridPoint(5, 5);

            var strategy = new BreadthFirstSearchRouteStrategy();

            var foundRoute = strategy.TryFindShortestRoute(cityGrid, startPoint, destinationPoint, out var route);

            Assert.IsTrue(foundRoute);
            Assert.AreEqual(12, route!.Length);
        }

        [Test]
        public void NoSolution()
        {
            var trafficJams = new List<GridPoint>
            {
                new(0, 3), new(1, 3), new(2, 3), new(3, 3), new(4, 3), new(5, 3),
                new(5, 4), new(5, 5), new(5, 6)
            };

            var cityGrid = new CityGrid(6, 6, trafficJams);
            var startPoint = new GridPoint(0, 0);
            var destinationPoint = new GridPoint(4, 4);

            var strategy = new BreadthFirstSearchRouteStrategy();

            var foundRoute = strategy.TryFindShortestRoute(cityGrid, startPoint, destinationPoint, out _);

            Assert.IsFalse(foundRoute);
        }
    }
}
