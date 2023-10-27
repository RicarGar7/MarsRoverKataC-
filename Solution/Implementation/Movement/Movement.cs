namespace Test.Implementation.Movement;

public interface Movement
{
    public Either<Alert, Position> Move();
}