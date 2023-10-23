using System.Runtime.InteropServices.ComTypes;

namespace Test;

public class Rover
{
    const int speed = 1;
    
    internal Position _position;
    internal Facing _facing;
    private Map _map;

    public void Land(Position landPosition, Facing facing, Map map)
    {
        if (!map._surface.CanBeContained(landPosition._latitude,landPosition._longitude))
        {
            throw new RoverCantLandOutOfThePlanetSurface();
        }

        if (map.HasObstacles(landPosition))
        {
            throw new RoverCantLandOnInTopOfAnObstacle();
        }
        
        _position = landPosition;
        _facing = facing;
        _map = map;
    }

    public void Execute(Instructions instruction)
    {
        _position = instruction switch
        {
            Instructions.MoveForward when _facing == Facing.N => new ForwardToNorthLinearMovement(_position, speed, _map).Apply(),
            Instructions.MoveForward when _facing == Facing.S => new ForwardToSouthLinearMovement(_position, speed, _map).Apply(),
            Instructions.MoveForward when _facing == Facing.E => new ForwardToEastLinearMovement(_position, speed, _map).Apply(),
            Instructions.MoveForward when _facing == Facing.W => new ForwardToWestLinearMovement(_position, speed, _map).Apply(),
            Instructions.MoveBackwards when _facing == Facing.N => new BackwardsToNorthLinearMovement(_position, speed, _map).Apply(),
            Instructions.MoveBackwards when _facing == Facing.S => new BackwardsToSouthLinearMovement(_position, speed, _map).Apply(),
            Instructions.MoveBackwards when _facing == Facing.E => new BackwardsToEastLinearMovement(_position, speed, _map).Apply(),
            Instructions.MoveBackwards when _facing == Facing.W => new BackwardsToWestLinearMovement(_position, speed, _map).Apply(),
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

    public void Execute(List<char> rawInstructions)
    {
        foreach (var instruction in Normalize(rawInstructions))
        {
            Execute(instruction);
        }
    }

    private IEnumerable<Instructions> Normalize(List<char> rawInstructions)
    {
        return rawInstructions
            .Where(value=> value == 'F' || value == 'B' || value == 'L' || value == 'R')
            .Select(value => value switch
            {
                'F' => Instructions.MoveForward,
                'B' => Instructions.MoveBackwards,
                'L' => Instructions.RotateLeft,
                'R' => Instructions.RotateRight
            });
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