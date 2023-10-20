namespace Test;

public class Surface
{
    private readonly int _latitude;
    private readonly int _longitude;

    public Surface(int latitude, int longitude)
    {
        _latitude = latitude;
        _longitude = longitude;
    }

    public bool CanBeContained(int latitude, int longitude)
    {
        return latitude <= _latitude && longitude <= _longitude;
    }

    public bool IsOut(int target)
    {
        return (target > _latitude && target >= _longitude) || target < 0 ;
    }
}