namespace Test;

public class BackwardsToNorthLinearMovement : MovementStrategy
{
    public Position _position;
    private Map _map;
    public int _speed;

    public BackwardsToNorthLinearMovement(Position position, int speed, Map map)
    {
        _position = position;
        _speed = speed;
        _map = map;
    }

    public Position Move()
    {
        var targetLatitude = _position._latitude - _speed;
        if (_map._surface.IsEdge(targetLatitude))
        {
            targetLatitude = 0;
        }
        return Position.Declare(targetLatitude, _position._longitude);
    }
}