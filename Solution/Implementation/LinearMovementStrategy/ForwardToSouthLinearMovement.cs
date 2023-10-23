namespace Test;

public class ForwardToSouthLinearMovement : Movement
{
    private Position _position;
    private Map _map;

    public ForwardToSouthLinearMovement(Position position, int speed, Map map)
        : base(map, new Position(position._latitude - speed, position._longitude))
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
            return Either<Alert, Position>.FromRight( new Position(_map._surface._latitude, _position._latitude));
        }

        return Either<Alert, Position>.FromRight(targetPosition);
    }
}