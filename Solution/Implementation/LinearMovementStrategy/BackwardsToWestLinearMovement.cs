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

    public override Either<Alert, Position> Apply()
    {
        if (!CanApply())
        {
            return Either<Alert, Position>.FromLeft(Alert.ObstacleDetectedAlert(targetPosition));
        }

        if (_map.IsPositionOutOfTheMap(targetPosition))
        {
            return Either<Alert, Position>.FromRight( new Position(_position._latitude, _map._surface._longitude));
        }

        return Either<Alert, Position>.FromRight(targetPosition); Either<Alert, Position>.FromRight(targetPosition);
    }
}