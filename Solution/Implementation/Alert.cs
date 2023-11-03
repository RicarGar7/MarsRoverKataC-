namespace Test;

public class Alert: IEquatable<Alert>
{
    private readonly Position _position;
    private readonly AlertType _type;

    private Alert(Position position, AlertType type)
    {
        _position = position;
        _type = type;
    }

    public static Alert ObstacleDetectedAlert(Position position)
    {
        return new Alert(position, AlertType.ObstacleDetected);
    }

    public bool Equals(Alert? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return _position.Equals(other._position) && _type == other._type;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Alert)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_position, (int)_type);
    }
}