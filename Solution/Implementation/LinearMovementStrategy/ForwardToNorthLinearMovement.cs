namespace Test;

public class ForwardToNorthLinearMovement : Movement
{
    private Position _position;
    private Map _map;

    public ForwardToNorthLinearMovement(Position position, int speed, Map map) 
        : base(map, new Position(position._latitude + speed, position._longitude))
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
            return new Position(0, _position._longitude);
        }

        return targetPosition;
    }
}