namespace Test;

public class LinearMovement
{
    private readonly Position _edgePosition;
    private readonly Position _targetPosition;
    private readonly Planet _map;

    public LinearMovement(Position edgePosition, Planet map, Position targetPosition)
    {
        _edgePosition = edgePosition;
        _map = map;
        this._targetPosition = targetPosition;
    }

    protected bool CanApply()
    {
        if (!_map.HasAnyObstacle())
        {
            return true;
        }

        return !_map.HasObstacles(_targetPosition);
    }

    public Either<Alert, Position> Apply()
    {
        if (!CanApply())
        {
            return Either<Alert, Position>.FromLeft(Alert.ObstacleDetectedAlert(_targetPosition));
        }

        if (_map.IsPositionOutOfTheMap(_targetPosition))
        {
            return Either<Alert, Position>.FromRight(_edgePosition);
        }

        return Either<Alert, Position>.FromRight(_targetPosition);
    }
}