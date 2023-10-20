namespace Test;

public class BackwardsToNorthLinearMovement : Movement
{
    private Position _position;
    private Map _map;

    public BackwardsToNorthLinearMovement(Position position, int speed, Map map)
        : base(map, new Position(position._latitude - speed, position._longitude))
    {
        _position = position;
        _map = map;
    }

    public override Position Apply()
    {
        if (!CanApply())
        {
            return _position.ShallowCopy();
        }

        if (_map.IsPositionOutOfTheMap(targetPosition))
        {
            return new Position(_map._surface._latitude, _position._longitude);
        }

        return targetPosition;
    }
}