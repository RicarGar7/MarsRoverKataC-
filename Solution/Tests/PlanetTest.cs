namespace Test;

public class PlanetTest
{
    [Fact]
    public void Should_Create()
    {
        var planet = Planet.Create(
            new Surface(10, 10),
            new List<Obstacle>()
            {
                new(new Position(5, 5))
            }
        );

        Assert.NotNull(planet);
    }

    [Fact]
    public void Should_Not_CreateWhenAnArtifactIsOut()
    {
        Assert.Throws<ObstacleDeclaredOutOfThePlanetException>(() =>
            Planet.Create(
                new Surface(10, 10),
                new List<Obstacle>()
                {
                    new(new Position(11, 11))
                }
            ));
    }
    
    [Fact]
    public void ShouldDetectNoObstacles()
    {
        Assert.False(Planet.Create(new Surface(50, 50), new List<Obstacle>()).HasAnyObstacle());
    }

    [Fact]
    public void ShouldDetectObstacles()
    {
        var obstacles = new List<Obstacle>
        {
            new(new Position(1, 2)),
            new(new Position(3, 4)),
        };
        var planet = Planet.Create(new Surface(50, 50), obstacles);

        Assert.True(planet.HasAnyObstacle());
    }

    [Fact]
    public void ShouldFindObstacleAtSpecificPosition()
    {
        var obstacles = new List<Obstacle>
        {
            new(new Position(1, 2)),
            new(new Position(3, 4)),
        };
        var planet = Planet.Create(new Surface(50, 50), obstacles);

        Assert.True(planet.HasObstacles(new Position(1, 2)));
        Assert.False(planet.HasObstacles(new Position(5, 6)));
    }

    [Fact]
    public void ShouldDetectTargetPositionIsOutOfTheMap()
    {
        var surface = new Surface(50, 50);
        var planet =  Planet.Create(surface, new List<Obstacle>());

        Assert.True(planet.IsPositionOutOfTheMap(new Position(25, 51))); 
        Assert.True(planet.IsPositionOutOfTheMap(new Position(51, 25)));
        Assert.True(planet.IsPositionOutOfTheMap(new Position(51, 51)));
        Assert.True(planet.IsPositionOutOfTheMap(new Position(-1, 25)));
        Assert.True(planet.IsPositionOutOfTheMap(new Position(25, -1)));
        Assert.False(planet.IsPositionOutOfTheMap(new Position(25, 25)));
    }
}