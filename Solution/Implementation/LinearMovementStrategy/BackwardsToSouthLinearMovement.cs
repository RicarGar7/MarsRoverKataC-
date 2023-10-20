namespace Test;

public class BackwardsToSouthLinearMovement : MovementStrategy
{
    public Position _position;
    private Map _map;
    public int _speed;

    public BackwardsToSouthLinearMovement(Position position, int speed, Map map)
    {
        _position = position;
        _speed = speed;
        _map = map;
    }

    public bool CanApply()
    {
        return true;
    }

    public Position Move()
    {
        var targetLatitude = _position._latitude + _speed;
        if (_map._surface.IsOut(targetLatitude))
        {
            targetLatitude = 0;
        }
        return new Position(targetLatitude, _position._longitude);
    }
}