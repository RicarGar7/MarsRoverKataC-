namespace Test;

public class RoverTest
{
    //ToDo
    // Acceso a los atributos de las clases
    // Simplificar mapa-satétilte
    // Refactor tests

    #region Landing

    [Fact]
    public void Should_Not_LandOutOfThePlanet()
    {
        var planet = Planet.Create(new Surface(100, 100), new List<Obstacle>());
        var rover = new Rover();
        var satellite = new Satellite();
        var map = satellite.Recognize(planet);

        Assert.Throws<RoverCantLandOutOfThePlanetSurface>(() =>
            rover.Land(new Position(110, 110), Facing.N, map)
        );
    }

    [Fact]
    public void Should_Not_LandInTheLimitOfThePlanet()
    {
        var rover = LandRoverOnTheLimitOfTheSurface(out var initialPosition);

        Assert.Equal(rover._position._latitude, initialPosition._latitude);
        Assert.Equal(rover._position._longitude, initialPosition._longitude);
    }

    [Fact]
    public void Should_Not_LandInTopOfAnObstacle()
    {
        var obstaclePosition = new Position(90, 90);
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

        Assert.Throws<RoverCantLandOnInTopOfAnObstacle>(() =>
            rover.Land(obstaclePosition, Facing.N, map)
        );
    }

    #endregion

    #region movement

    [Fact]
    public void Should_MoveAroundThePlanetSurface_MakingLinearMovements()
    {
        var planet = Planet.Create(new Surface(100, 100), new List<Obstacle>());
        var rover = new Rover();
        var satellite = new Satellite();
        var map = satellite.Recognize(planet);
        var initialPosition = new Position(50, 50);
        var initialFacing = Facing.N;
        rover.Land(initialPosition, initialFacing, map);

        rover.Execute(Instructions.MoveForward);

        var expectedLatitude = initialPosition._latitude + 1;
        Assert.Equal(rover._position._latitude, expectedLatitude);
        Assert.Equal(rover._position._longitude, initialPosition._longitude);

        rover.Execute(Instructions.MoveBackwards);
        Assert.Equal(rover._position._latitude, initialPosition._latitude);
        Assert.Equal(rover._position._longitude, initialPosition._longitude);
    }

    [Fact]
    public void Should_MoveAroundThePlanetSurface_MakingRotationalMovements()
    {
        var rover = LandRoverInTheMiddleOfTheSurface(out _);

        rover.Execute(Instructions.RotateLeft);
        Assert.Equal(Facing.W, rover._facing);
        rover.Execute(Instructions.RotateRight);
        Assert.Equal(Facing.N, rover._facing);
        rover.Execute(Instructions.RotateRight);
        Assert.Equal(Facing.E, rover._facing);
        rover.Execute(Instructions.RotateRight);
        Assert.Equal(Facing.S, rover._facing);
        rover.Execute(Instructions.RotateRight);
        Assert.Equal(Facing.W, rover._facing);
        rover.Execute(Instructions.RotateLeft);
        Assert.Equal(Facing.S, rover._facing);
        rover.Execute(Instructions.RotateLeft);
        Assert.Equal(Facing.E, rover._facing);
        rover.Execute(Instructions.RotateLeft);
        Assert.Equal(Facing.N, rover._facing);
    }

    [Fact]
    public void Should_MoveAroundThePlanetSurface_MakingBothRotationalAndLinearMovements()
    {
        var rover = LandRoverInTheMiddleOfTheSurface(out var initialPosition);

        rover.Execute(Instructions.RotateLeft);
        rover.Execute(Instructions.MoveForward);

        var expectedLongitude = initialPosition._longitude - 1;
        Assert.Equal(rover._position._latitude, initialPosition._latitude);
        Assert.Equal(rover._position._longitude, expectedLongitude);

        rover.Execute(Instructions.RotateRight);
        rover.Execute(Instructions.MoveBackwards);

        var expectedLatitude = initialPosition._latitude - 1;
        Assert.Equal(rover._position._latitude, expectedLatitude);
        Assert.Equal(rover._position._longitude, expectedLongitude);
    }

    [Fact]
    public void Should_MoveAroundThePlanetSurface_CircumnavigatingTheWorldVertically()
    {
        var rover = LandRoverOnTheLimitByHeightOfTheSurface(out var initialPosition);

        rover.Execute(Instructions.MoveForward);

        var expectedLatitude = 0;
        Assert.Equal(rover._position._latitude, expectedLatitude);
        Assert.Equal(rover._position._longitude, initialPosition._longitude);
    }

    #endregion

    #region obstacleDetection

    [Fact]
    public void Should_MoveAroundThePlanetSurface_DetectingObstacles()
    {
        var rover = LandRoverInTheMiddleOfTheSurface(out var initialPosition, new List<Obstacle>
        {
            new(new Position(51, 50))
        });

        rover.Execute(Instructions.MoveForward);

        Assert.Equal(rover._position._latitude, initialPosition._latitude);
        Assert.Equal(rover._position._longitude, initialPosition._longitude);
    }

    #endregion

    #region LandingShortcuts

    private Rover LandRoverInTheMiddleOfTheSurface(out Position initialPosition, List<Obstacle>? obstacles = null)
    {
        obstacles ??= new List<Obstacle>();
        var planet = Planet.Create(new Surface(100, 100), obstacles);
        var rover = new Rover();
        var satellite = new Satellite();
        var map = satellite.Recognize(planet);
        initialPosition = new Position(50, 50);
        var initialFacing = Facing.N;
        rover.Land(initialPosition, initialFacing, map);
        return rover;
    }

    private Rover LandRoverOnTheLimitByLengthOfTheSurface(out Position initialPosition)
    {
        var planet = Planet.Create(new Surface(100, 100), new List<Obstacle>());
        var rover = new Rover();
        var satellite = new Satellite();
        var map = satellite.Recognize(planet);
        initialPosition = new Position(50, 0);
        var initialFacing = Facing.N;
        rover.Land(initialPosition, initialFacing, map);
        return rover;
    }

    private Rover LandRoverOnTheLimitByHeightOfTheSurface(out Position initialPosition,
        List<Obstacle>? obstacles = null)
    {
        obstacles ??= new List<Obstacle>();
        var planet = Planet.Create(new Surface(100, 100), obstacles);
        var rover = new Rover();
        var satellite = new Satellite();
        var map = satellite.Recognize(planet);
        initialPosition = new Position(100, 50);
        var initialFacing = Facing.N;
        rover.Land(initialPosition, initialFacing, map);
        return rover;
    }

    private Rover LandRoverOnTheLimitOfTheSurface(out Position initialPosition)
    {
        var planet = Planet.Create(new Surface(100, 100), new List<Obstacle>());
        var rover = new Rover();
        var satellite = new Satellite();
        var map = satellite.Recognize(planet);
        initialPosition = new Position(100, 100);
        var initialFacing = Facing.N;
        rover.Land(initialPosition, initialFacing, map);
        return rover;
    }

    #endregion
}

public enum Instructions
{
    MoveForward,
    MoveBackwards,
    RotateLeft,
    RotateRight
}