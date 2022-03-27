using NUnit.Framework;
using System.Collections.Generic;

namespace Gridville.Tests
{
    [TestFixture]
    internal class BfsWithClosestDropPointStrategyTests
    {
        [Test]
        public void ClosestDropPointOnly()
        {
            var trafficJams = new List<GridPoint>
            {
                new(0, 3), new(1, 3), new(2, 3), new(3, 3), new(4, 3), new(5, 3),
                new(5, 4), new(5, 5), new(5, 6)
            };

            var cityGrid = new CityGrid(6, 6, trafficJams);
            var startPoint = new GridPoint(0, 0);
            var destinationPoint = new GridPoint(4, 4);

            var strategy = new BfsWithClosestDropPointStrategy();

            var foundRoute = strategy.TryFindShortestRoute(cityGrid, startPoint, destinationPoint, out var route);

            Assert.IsTrue(foundRoute);
            Assert.AreEqual(6, route!.Length);
        }

        [Test]
        public void IsolatedStartPoint()
        {
            var trafficJams = new List<GridPoint>
            {
                new(0, 1), new(1, 1), new(1, 0)
            };

            var cityGrid = new CityGrid(6, 6, trafficJams);
            var startPoint = new GridPoint(0, 0);
            var destinationPoint = new GridPoint(4, 4);

            var strategy = new BfsWithClosestDropPointStrategy();

            var foundRoute = strategy.TryFindShortestRoute(cityGrid, startPoint, destinationPoint, out var route);

            Assert.IsTrue(foundRoute);
            Assert.AreEqual(0, route!.Length);
        }

        [Test]
        public void StartPointWithOnlyOneFreeNeighbor()
        {
            var trafficJams = new List<GridPoint>
            {
                new(0, 1), new(1, 1), new(2, 1), new(2, 0)
            };

            var cityGrid = new CityGrid(6, 6, trafficJams);
            var startPoint = new GridPoint(0, 0);
            var destinationPoint = new GridPoint(4, 4);

            var strategy = new BfsWithClosestDropPointStrategy();

            var foundRoute = strategy.TryFindShortestRoute(cityGrid, startPoint, destinationPoint, out var route);

            Assert.IsTrue(foundRoute);
            Assert.AreEqual(1, route!.Length);
        }

        [Test]
        public void BlockedStartPoint()
        {
            var trafficJams = new List<GridPoint>
            {
                new(0, 0)
            };

            var cityGrid = new CityGrid(6, 6, trafficJams);
            var startPoint = new GridPoint(0, 0);
            var destinationPoint = new GridPoint(4, 4);

            var strategy = new BfsWithClosestDropPointStrategy();

            var foundRoute = strategy.TryFindShortestRoute(cityGrid, startPoint, destinationPoint, out _);

            Assert.IsFalse(foundRoute);
        }
    }
}
