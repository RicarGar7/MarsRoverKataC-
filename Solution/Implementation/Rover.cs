using Test.Implementation.Movement;

namespace Test;

public class Rover
{
    const int speed = 1;

    internal Position _position;
    internal Implementation.Facing.Facing _facing;
    private Planet _planet;
    internal ObstacleDetectedAlerts obstacleDetectedAlerts = new();
    private readonly Outbox _outbox;

    public Rover(Outbox outbox)
    {
        _outbox = outbox;
    }

    public void Land(Position landPosition, Implementation.Facing.Facing facing, Planet map)
    {
        if (!map._surface.CanBeContained(landPosition._latitude, landPosition._longitude))
        {
            throw new RoverCantLandOutOfThePlanetSurface();
        }

        if (map.HasObstacles(landPosition))
        {
            throw new RoverCantLandOnInTopOfAnObstacle();
        }

        _position = landPosition;
        _facing = facing;
        _planet = map;
    }

    private Implementation.Facing.Facing ApplyRotationalMovements(Instructions instruction)
    {
        if (obstacleDetectedAlerts.Any())
        {
            return _facing;
        }

        return instruction switch
        {
            Instructions.RotateLeft  => _facing = _facing.WhatIsLeft(),
            Instructions.RotateRight  => _facing = _facing.WhatIsRight(),
            _ => _facing
        };
    }

    private Position ApplyLinearMovement(Instructions instruction)
    {
        if (obstacleDetectedAlerts.Any())
        {
            return _position;
        }

        Result<Alert, Position> appliedMovement = (instruction switch
        {
            Instructions.MoveForward => new Forward(_position,_facing, _planet, speed).Move(),
            Instructions.MoveBackwards => new Backwards(_position,_facing, _planet, speed).Move(),
            _ => null
        });

        if (appliedMovement.IsLeft)
        {
            obstacleDetectedAlerts.Add(appliedMovement.Left);
            _outbox.publish(appliedMovement.Left);
            return _position;
        }

        return appliedMovement.Right;
    }

    public void Execute(List<char> rawInstructions)
    {
        obstacleDetectedAlerts.Clear();

        foreach (var instruction in Normalize(rawInstructions))
        {
            _position = ApplyLinearMovement(instruction);
            _facing = ApplyRotationalMovements(instruction);
        }
    }

    private IEnumerable<Instructions> Normalize(List<char> rawInstructions)
    {
        return rawInstructions
            .Where(value => value == 'F' || value == 'B' || value == 'L' || value == 'R')
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
    public RoverCantLandOutOfThePlanetSurface() : base("Can't arrange the landing operation out of the planet surface")
    {
    }
}

public class RoverCantLandOnInTopOfAnObstacle : Exception
{
    public RoverCantLandOnInTopOfAnObstacle() : base("Can't arrange the landing operation in top of an obstacle")
    {
    }
}