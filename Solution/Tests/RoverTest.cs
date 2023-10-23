namespace Test;

public class RoverTest
{
    //ToDo
    // Acceso a los atributos de las clases
    // Refactor tests
    // Quitar objeto satelite
    // Meter una lista de instrucciones

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

    #region Movement

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
    public void Should_be_able_to_execute_character_collection_as_instructions_skipping_unknown_characters()
    {
        var rover = LandRoverInTheMiddleOfTheSurface(out var initialPosition);
        rover.Execute(new List<char> { 'F', 'L', 'M', 'F', 'L', 'F', 'L', 'F' });

        Assert.Equal(rover._position._latitude, initialPosition._latitude);
        Assert.Equal(rover._position._longitude, initialPosition._longitude);
    }

    #endregion

    #region CircumnavigatingMovement

    [Fact]
    public void Should_MoveAroundThePlanetSurface_CircumnavigatingTheWorldVertically()
    {
        var rover = LandRoverOnTheLimitByLengthOfTheSurface(out var initialPosition);

        rover.Execute(Instructions.MoveForward);

        var expectedLatitude = 0;
        Assert.Equal(rover._position._latitude, expectedLatitude);
        Assert.Equal(rover._position._longitude, initialPosition._longitude);

        rover.Execute(Instructions.MoveBackwards);

        expectedLatitude = 100;
        Assert.Equal(rover._position._latitude, expectedLatitude);
        Assert.Equal(rover._position._longitude, initialPosition._longitude);
    }

    [Fact]
    public void Should_MoveAroundThePlanetSurface_CircumnavigatingTheWorldHorizontally()
    {
        var rover = LandRoverOnTheLimitByHeightOfTheSurface(out var initialPosition);

        rover.Execute(Instructions.RotateRight);
        rover.Execute(Instructions.MoveForward);

        var expectedLongitude = 0;
        Assert.Equal(rover._position._latitude, initialPosition._latitude);
        Assert.Equal(rover._position._longitude, expectedLongitude);

        rover.Execute(Instructions.MoveBackwards);

        expectedLongitude = 100;
        Assert.Equal(rover._position._latitude, initialPosition._latitude);
        Assert.Equal(rover._position._longitude, expectedLongitude);
    }

    #endregion

    #region ObstacleDetection

    [Fact]
    public void
        Should_MoveAroundThePlanetSurface_DetectingObstacles_And_StoppingTheExecutionSequence_And_ReportingTheObstaclePosition()
    {
        var rover = LandRoverInTheMiddleOfTheSurface(out var initialPosition, new List<Obstacle>
        {
            new(new Position(51, 49))
        });

        rover.Execute(new List<char> { 'F', 'L', 'F', 'F', 'L', 'F' });

        Assert.True(new List<Alert>
        {
            Alert.ObstacleDetectedAlert(new Position(51, 49))
        }.SequenceEqual(rover._alerts));
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
        initialPosition = new Position(100, 50);
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
        initialPosition = new Position(50, 100);
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