namespace Test;

public class ForwardToEastLinearMovement : Movement
{
    public Position _position;
    private Map _map;

    public ForwardToEastLinearMovement(Position position, int speed, Map map) 
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
            return new Position(_position._latitude, 0);
        }

        return targetPosition;
    }
}