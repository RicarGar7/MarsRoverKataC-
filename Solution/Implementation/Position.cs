namespace Test;

public class Position
{
    public readonly int _latitude;
    public readonly int _longitude;

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

public class CouldNotDeclarePositionException : Exception
{
    public CouldNotDeclarePositionException() : base("CouldNotDeclarePosition")
    {
    }
}