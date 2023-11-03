namespace Test;

public class Position: IEquatable<Position>
{
    internal readonly int _latitude;
    internal readonly int _longitude;

    public Position(int latitude, int longitude)
    {
        _latitude = latitude;
        _longitude = longitude;
    }

    public bool Equals(Position? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return _latitude == other._latitude && _longitude == other._longitude;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Position)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_latitude, _longitude);
    }
}