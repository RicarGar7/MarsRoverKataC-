namespace Test.Implementation.Movement;

public abstract class Movement
{
    private readonly Facing.Facing facing;

    protected Movement(Facing.Facing facing)
    {
        this.facing = facing;
    }

    public Result<Alert, Position> Move()
    {
        return facing.Value() switch
        {
            FacingValue.N => ToNorth(),
            FacingValue.S => ToSouth(),
            FacingValue.E => ToEast(),
            FacingValue.W => ToWest(),
        };
    }

     protected abstract Result<Alert, Position> ToNorth();
     protected abstract Result<Alert, Position> ToSouth();
     protected abstract Result<Alert, Position> ToEast();
     protected abstract Result<Alert, Position> ToWest();
}