namespace Test;

public class BackwardsToEastLinearMovement : MovementStrategy
{
    public Position _position;
    private Map _map;
    public int _speed;

    public BackwardsToEastLinearMovement(Position position, int speed, Map map)
    {
        _position = position;
        _speed = speed;
        _map = map;
    }

    public Position Move()
    {
        var targetLongitude = _position._longitude - _speed;
        if (_map._surface.IsEdge(targetLongitude))
        {
            targetLongitude = 0;
        }
        return Position.Declare(_position._latitude, targetLongitude);
    }
}