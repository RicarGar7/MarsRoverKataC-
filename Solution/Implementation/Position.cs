namespace Test;

public class Position
{
    public readonly int _latitude;
    public readonly int _longitude;

    private Position(int latitude, int longitude)
    {
        _latitude = latitude;
        _longitude = longitude;
    }

    public static Position Declare(int latitude, int longitude)
    {
        if (latitude < 0 || longitude < 0)
        {
            throw new CouldNotDeclarePositionException();
        }

        return new Position(latitude, longitude);
    }
}

public class CouldNotDeclarePositionException : Exception
{
    public CouldNotDeclarePositionException() : base("CouldNotDeclarePosition")
    {
    }
}