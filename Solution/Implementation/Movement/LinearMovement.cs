namespace Test;

public class LinearMovement
{
    private readonly Position _edgePosition;
    private readonly Map _map;
    private readonly Position targetPosition;

    public LinearMovement(Position edgePosition, Map map, Position targetPosition)
    {
        _edgePosition = edgePosition;
        _map = map;
        this.targetPosition = targetPosition;
    }

    protected bool CanApply()
    {
        if (!_map.HasAnyObstacle())
        {
            return true;
        }

        return !_map.HasObstacles(targetPosition);
    }

    public Either<Alert, Position> Apply()
    {
        if (!CanApply())
        {
            return Either<Alert, Position>.FromLeft(Alert.ObstacleDetectedAlert(targetPosition));
        }

        if (_map.IsPositionOutOfTheMap(targetPosition))
        {
            return Either<Alert, Position>.FromRight(_edgePosition);
        }

        return Either<Alert, Position>.FromRight(targetPosition);
    }
}