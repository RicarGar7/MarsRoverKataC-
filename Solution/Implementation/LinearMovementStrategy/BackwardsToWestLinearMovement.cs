namespace Test;

public class BackwardsToWestLinearMovement : MovementStrategy
{
    public Position _position;
    private Map _map;
    public int _speed;

    public BackwardsToWestLinearMovement(Position position, int speed, Map map)
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
        var targetLongitude = _position._longitude + _speed;
        if (_map._surface.IsOut(targetLongitude))
        {
            targetLongitude = 0;
        }
        return Position.Declare(_position._latitude, targetLongitude);
    }
}