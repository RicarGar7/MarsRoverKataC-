namespace Test;

using Xunit;
using Test; // Assuming that this is the namespace for the Map class.
using System.Collections.Generic;
using System.Linq;

public class MapTests
{
    [Fact]
    public void ShouldInitializeMapWithSurfaceAndObstacles()
    {
        var irrelevantSurface = new Surface(50, 50);
        var surface = irrelevantSurface;
        var obstacles = new List<Obstacle>
        {
            new(Position.Declare(25, 25))
        };

        var map = new Map(surface, obstacles);

        Assert.NotNull(map._surface);
        Assert.NotNull(map._obstacles);
    }

    [Fact]
    public void ShouldDetectNoObstacles()
    {
        var map = new Map(new Surface(50, 50), new List<Obstacle>());
        // Act & Assert
        Assert.False(map.HasAnyObstacle());
    }

    [Fact]
    public void ShouldDetectObstacles()
    {
        var obstacles = new List<Obstacle>
        {
            new(Position.Declare(1, 2)),
            new(Position.Declare(3, 4)),
        };
        var map = new Map(new Surface(50, 50), obstacles);

        Assert.True(map.HasAnyObstacle());
    }

    [Fact]
    public void ShouldFindObstacleAtSpecificPosition()
    {
        var obstacles = new List<Obstacle>
        {
            new(Position.Declare(1, 2)),
            new(Position.Declare(3, 4)),
        };
        var map = new Map(new Surface(50, 50), obstacles);

        Assert.True(map.HasObstacles(Test.Position.Declare(1, 2)));
        Assert.False(map.HasObstacles(Test.Position.Declare(5, 6)));
    }

    [Fact]
    public void ShouldDetectTargetPositionIsInTheEdge()
    {
        var surface = new Surface(50, 50);
        var map = new Map(surface, new List<Obstacle>());

        Assert.True(map.IsTargetPositionInTheEdge(Position.Declare(25, 51))); 
        Assert.True(map.IsTargetPositionInTheEdge(Position.Declare(51, 25)));
        Assert.True(map.IsTargetPositionInTheEdge(Position.Declare(51, 51)));
        Assert.True(map.IsTargetPositionInTheEdge(Position.Declare(-1, 25)));
        Assert.True(map.IsTargetPositionInTheEdge(Position.Declare(25, -1)));
        Assert.False(map.IsTargetPositionInTheEdge(Position.Declare(25, 25)));
    }
}