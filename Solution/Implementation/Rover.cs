using System.Runtime.InteropServices.ComTypes;

namespace Test;

public class Rover
{
    const int speed = 1;
    
    public Position _position;
    public Facing _facing;
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

    public void Execute(Instructions instruction)
    {
        _position = instruction switch
        {
            Instructions.MoveForward when _facing == Facing.N => Position.Declare(_position._latitude + speed , _position._longitude),
            Instructions.MoveForward when _facing == Facing.S => Position.Declare(_position._latitude - speed , _position._longitude),
            Instructions.MoveForward when _facing == Facing.E => Position.Declare(_position._latitude, _position._longitude + speed),
            Instructions.MoveForward when _facing == Facing.W => Position.Declare(_position._latitude, _position._longitude - speed),
            Instructions.MoveBackwards when _facing == Facing.N => Position.Declare(_position._latitude - speed, _position._longitude),
            Instructions.MoveBackwards when _facing == Facing.S => Position.Declare(_position._latitude + speed, _position._longitude),
            Instructions.MoveBackwards when _facing == Facing.E => Position.Declare(_position._latitude, _position._longitude -speed),
            Instructions.MoveBackwards when _facing == Facing.W => Position.Declare(_position._latitude, _position._longitude +speed),
            _ => _position
        };

        _facing = instruction switch
        {
            Instructions.RotateLeft when _facing == Facing.N => _facing = Facing.W,
            Instructions.RotateLeft when _facing == Facing.W => _facing = Facing.S,
            Instructions.RotateLeft when _facing == Facing.S => _facing = Facing.E,
            Instructions.RotateLeft when _facing == Facing.E => _facing = Facing.N,
            Instructions.RotateRight when _facing == Facing.N => _facing = Facing.E,
            Instructions.RotateRight when _facing == Facing.E => _facing = Facing.S,
            Instructions.RotateRight when _facing == Facing.S => _facing = Facing.W,
            Instructions.RotateRight when _facing == Facing.W => _facing = Facing.N,
            _ => _facing
        };
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