namespace Test;

public class BackwardsToWestLinearMovement : Movement
{
    private Position _position;
    private Map _map;

    public BackwardsToWestLinearMovement(Position position, int speed, Map map)
        : base(map, new Position(position._latitude, position._longitude + speed))
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
            return new Position(_position._latitude, _map._surface._longitude);
        }

        return targetPosition;
    }
}