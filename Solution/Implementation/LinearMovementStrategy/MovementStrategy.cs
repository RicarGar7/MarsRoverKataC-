namespace Test;

interface MovementStrategy
{
    public bool CanApply();
    public Position Move();
}