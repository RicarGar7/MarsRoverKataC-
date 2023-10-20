namespace Test;

public abstract class Movement
{
    private Map _map;
    protected Position targetPosition;

    protected Movement(Map map, Position targetPosition)
    {
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

    //Todo buscar patrones de comportamiento para implementar esto de forma generica
    public abstract Position Apply();
}