namespace Test;

public class Surface
{
    private readonly int _x;
    private readonly int _y;

    public Surface(int x, int y)
    {
        _x = x;
        _y = y;
    }

    public bool CanBeContained(int x, int y)
    {
        return x <= _x && y <= _y;
    }

    public bool IsOut(int target)
    {
        return (target > _x && target >= _y) || target < 0 ;
    }
}