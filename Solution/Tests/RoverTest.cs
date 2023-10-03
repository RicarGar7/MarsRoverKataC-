namespace Test;

public class RoverTest
{
    #region Landing

    [Fact]
    public void Should_Not_LandOnAnUnrecognizedPlanet()
    {
        var rover = new Rover();

        Assert.Throws<RoverCantLandOnAUnrecognizedPlanet>(() =>
            rover.Land(Position.Declare(0, 0), Facing.N)
        );
    }

    [Fact]
    public void Should_Not_LandOutOfThePlanet()
    {
        var planet = Planet.Create(new Surface(100, 100), new List<Obstacle>());
        var rover = new Rover();
        var satellite = new Satellite();
        var map = satellite.Recognize(planet);
        rover.LoadMap(map);

        Assert.Throws<RoverCantLandOutOfThePlanetSurface>(() =>
            rover.Land(Position.Declare(110, 110), Facing.N)
        );
    }

    [Fact]
    public void Should_Not_LandInTopOfAnObstacle()
    {
        var obstaclePosition = Position.Declare(90, 90);
        var planet = Planet.Create(
            new Surface(100, 100),
            new List<Obstacle>
            {
                new(obstaclePosition)
            }
        );
        var rover = new Rover();
        var satellite = new Satellite();
        var map = satellite.Recognize(planet);
        rover.LoadMap(map);

        Assert.Throws<RoverCantLandOnInTopOfAnObstacle>(() =>
            rover.Land(obstaclePosition, Facing.N)
        );
    }

    #endregion
}