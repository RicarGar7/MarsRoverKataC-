namespace Test;

public class LinearMovement
{
    private readonly Position _edgePosition;
    private readonly Position _targetPosition;
    private readonly Planet _planet;

    public LinearMovement(Position edgePosition, Planet planet, Position targetPosition)
    {
        _edgePosition = edgePosition;
        _planet = planet;
        _targetPosition = targetPosition;
    }

    protected bool CanApply()
    {
        if (!_planet.HasAnyObstacle())
        {
            return true;
        }

        return !_planet.HasObstacles(_targetPosition);
    }

    public Result<Alert, Position> Apply()
    {
        if (!CanApply())
        {
            return Result<Alert, Position>.FromLeft(Alert.ObstacleDetectedAlert(_targetPosition));
        }

        if (_planet.IsPositionOutOfThePlanet(_targetPosition))
        {
            return Result<Alert, Position>.FromRight(_edgePosition);
        }

        return Result<Alert, Position>.FromRight(_targetPosition);
    }
}