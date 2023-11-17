namespace Test.Implementation.Movement;

public abstract class Movement
{
    private readonly Facing.Facing facing;

    protected Movement(Facing.Facing facing)
    {
        this.facing = facing;
    }

    public Either<Alert, Position> Move()
    {
        return facing.Value() switch
        {
            FacingValue.N => ToNorth(),
            FacingValue.S => ToSouth(),
            FacingValue.E => ToEast(),
            FacingValue.W => ToWest(),
        };
    }

     protected abstract Either<Alert, Position> ToNorth();
     protected abstract Either<Alert, Position> ToSouth();
     protected abstract Either<Alert, Position> ToEast();
     protected abstract Either<Alert, Position> ToWest();
}