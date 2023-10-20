namespace Test;

public class ForwardToNorthLinearMovement : MovementStrategy
{
    public Position _position;
    private Map _map;
    public int _speed;
    private Position targetPosition;

    public ForwardToNorthLinearMovement(Position position, int speed, Map map)
    {
        _position = position;
        _speed = speed;
        _map = map;
        
        targetPosition = Position.Declare(_position._latitude + _speed, _position._longitude);
    }

    public bool CanApply()
    {
        if (!_map.HasAnyObstacle())
        {
            return true;
        }

        return !_map.HasObstacles(targetPosition);
    }

    public Position Move()
    {
        if (!CanApply())
        {
            return _position.ShallowCopy();
        }

        if (_map.IsTargetPositionInTheEdge(targetPosition))
        {
            return Position.Declare(0, _position._longitude);
        }

        return targetPosition;
    }
}