namespace Test;

public class ForwardToEastLinearMovement : Movement
{
    private Position _position;
    private Map _map;

    public ForwardToEastLinearMovement(Position position, int speed, Map map)
        : base(map, new Position(position._latitude, position._longitude + speed))
    {
        _position = position;
        _map = map;
    }

    public override Either<Alert, Position> Apply()
    {
        if (!CanApply())
        {
            return Either<Alert, Position>.FromLeft(Alert.ObstacleDetectedAlert(targetPosition));
        }

        if (_map.IsPositionOutOfTheMap(targetPosition))
        {
            return Either<Alert, Position>.FromRight (new Position(_position._latitude, 0));
        }

        return Either<Alert, Position>.FromRight(targetPosition);
    }
}