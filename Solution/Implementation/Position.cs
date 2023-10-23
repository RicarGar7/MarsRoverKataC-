namespace Test;

public class Position
{
    internal readonly int _latitude;
    internal readonly int _longitude;

    public Position(int latitude, int longitude)
    {
        _latitude = latitude;
        _longitude = longitude;
    }

    public Position ShallowCopy()
    {
        return (Position)MemberwiseClone();
    }
}