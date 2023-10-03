namespace Test;

public class Rover
{
    private Position _position;
    private Facing _facing;
    private Map _map;

    public void LoadMap(Map map)
    {
        _map = map;
    }

    public void Land(Position landPosition, Facing facing)
    {
        if (_map == null)
        {
            throw new RoverCantLandOnAUnrecognizedPlanet();
        }

        if (!_map._surface.CanBeContained(landPosition._latitude,landPosition._longitude))
        {
            throw new RoverCantLandOutOfThePlanetSurface();
        }

        if (_map._obstacles.Any(obstacle => obstacle._position == landPosition))
        {
            throw new RoverCantLandOnInTopOfAnObstacle();
        }
        
        _position = landPosition;
        _facing = facing;
    }
}

public class RoverCantLandOnAUnrecognizedPlanet : Exception
{
    public RoverCantLandOnAUnrecognizedPlanet(): base("Can't arrange the landing operation in an unrecognized planet")
    {
        
    }
}

public class RoverCantLandOutOfThePlanetSurface : Exception
{
    public RoverCantLandOutOfThePlanetSurface(): base("Can't arrange the landing operation out of the planet surface")
    {
        
    }
}

public class RoverCantLandOnInTopOfAnObstacle : Exception
{
    public RoverCantLandOnInTopOfAnObstacle(): base("Can't arrange the landing operation in top of an obstacle")
    {
        
    }
}