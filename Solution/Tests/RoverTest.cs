using Test.Facing;

namespace Test;

public class RoverTest
{
    // Acceso a los atributos de las clases
    // Refactor tests

    #region Landing

    [Fact]
    public void Should_Not_LandOutOfThePlanet()
    {
        var planet = Planet.Create(new Surface(100, 100), new List<Obstacle>());
        var rover = new Rover();

        Assert.Throws<RoverCantLandOutOfThePlanetSurface>(() =>
            rover.Land(new Position(110, 110), new North(), planet)
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

        Assert.Throws<RoverCantLandOnInTopOfAnObstacle>(() =>
            rover.Land(obstaclePosition, new North(), planet)
        );
    }

    #endregion

    #region Movement

    [Fact]
    public void Should_MoveAroundThePlanetSurface_MakingLinearMovements()
    {
        var planet = Planet.Create(new Surface(100, 100), new List<Obstacle>());
        var rover = new Rover();
        var initialPosition = new Position(50, 50);
        var initialFacing = new North();
        rover.Land(initialPosition, initialFacing, planet);

        rover.Execute(new List<char>("F"));

        var expectedLatitude = initialPosition._latitude + 1;
        Assert.Equal(rover._position._latitude, expectedLatitude);
        Assert.Equal(rover._position._longitude, initialPosition._longitude);

        rover.Execute(new List<char>("B"));
        Assert.Equal(rover._position._latitude, initialPosition._latitude);
        Assert.Equal(rover._position._longitude, initialPosition._longitude);
    }

    [Fact]
    public void Should_MoveAroundThePlanetSurface_MakingRotationalMovements()
    {
        var rover = LandRoverInTheMiddleOfTheSurface(out _);

        rover.Execute(new List<char>("L"));
        Assert.Equal(FacingValue.W, rover._facing.Value());
        rover.Execute(new List<char>("R"));
        Assert.Equal(FacingValue.N, rover._facing.Value());
        rover.Execute(new List<char>("R"));
        Assert.Equal(FacingValue.E, rover._facing.Value());
        rover.Execute(new List<char>("R"));
        Assert.Equal(FacingValue.S, rover._facing.Value());
        rover.Execute(new List<char>("R"));
        Assert.Equal(FacingValue.W, rover._facing.Value());
        rover.Execute(new List<char>("L"));
        Assert.Equal(FacingValue.S, rover._facing.Value());
        rover.Execute(new List<char>("L"));
        Assert.Equal(FacingValue.E, rover._facing.Value());
        rover.Execute(new List<char>("L"));
        Assert.Equal(FacingValue.N, rover._facing.Value());
    }

    [Fact]
    public void Should_MoveAroundThePlanetSurface_MakingBothRotationalAndLinearMovements()
    {
        var rover = LandRoverInTheMiddleOfTheSurface(out var initialPosition);

        rover.Execute(new List<char>("L"));
        rover.Execute(new List<char>("F"));

        var expectedLongitude = initialPosition._longitude - 1;
        Assert.Equal(rover._position._latitude, initialPosition._latitude);
        Assert.Equal(rover._position._longitude, expectedLongitude);

        rover.Execute(new List<char>("R"));
        rover.Execute(new List<char>("B"));

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

        rover.Execute(new List<char>("F"));

        var expectedLatitude = 0;
        Assert.Equal(rover._position._latitude, expectedLatitude);
        Assert.Equal(rover._position._longitude, initialPosition._longitude);

        rover.Execute(new List<char>("B"));

        expectedLatitude = 100;
        Assert.Equal(rover._position._latitude, expectedLatitude);
        Assert.Equal(rover._position._longitude, initialPosition._longitude);
    }

    [Fact]
    public void Should_MoveAroundThePlanetSurface_CircumnavigatingTheWorldHorizontally()
    {
        var rover = LandRoverOnTheLimitByHeightOfTheSurface(out var initialPosition);

        rover.Execute(new List<char>("R"));
        rover.Execute(new List<char>("F"));

        var expectedLongitude = 0;
        Assert.Equal(rover._position._latitude, initialPosition._latitude);
        Assert.Equal(rover._position._longitude, expectedLongitude);

        rover.Execute(new List<char>("B"));

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
        
        //ToDo: Reportar la posición del obstáculo
    }

    [Fact]
    public void
        Should_MoveAroundThePlanetSurface_DetectingObstacles_And_StoppingTheExecutionSequence_And_StartingNextSequenceCorrectly()
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
        var expectedPosition = new Position(51, 50);
        Assert.True(rover._position.Equals(expectedPosition));
        
        rover.Execute(new List<char> { 'R','F', 'F' });

        Assert.False(rover._position.Equals(expectedPosition));
    }

    #endregion

    #region LandingShortcuts

    private Rover LandRoverInTheMiddleOfTheSurface(out Position initialPosition, List<Obstacle>? obstacles = null)
    {
        obstacles ??= new List<Obstacle>();
        var planet = Planet.Create(new Surface(100, 100), obstacles);
        var rover = new Rover();
        initialPosition = new Position(50, 50);
        rover.Land(initialPosition, new North(), planet);
        return rover;
    }

    private Rover LandRoverOnTheLimitByLengthOfTheSurface(out Position initialPosition)
    {
        var planet = Planet.Create(new Surface(100, 100), new List<Obstacle>());
        var rover = new Rover();
        initialPosition = new Position(100, 50);
        rover.Land(initialPosition, new North(), planet);
        return rover;
    }

    private Rover LandRoverOnTheLimitByHeightOfTheSurface(out Position initialPosition,
        List<Obstacle>? obstacles = null)
    {
        obstacles ??= new List<Obstacle>();
        var planet = Planet.Create(new Surface(100, 100), obstacles);
        var rover = new Rover();
        initialPosition = new Position(50, 100);
        rover.Land(initialPosition, new North(), planet);
        return rover;
    }

    private Rover LandRoverOnTheLimitOfTheSurface(out Position initialPosition)
    {
        var planet = Planet.Create(new Surface(100, 100), new List<Obstacle>());
        var rover = new Rover();
        initialPosition = new Position(100, 100);
        rover.Land(initialPosition, new North(), planet);
        return rover;
    }

    #endregion
}