namespace Test;

public class Result<Error, Success>
{
    public Error Left { get; private set; }
    public Success Right { get; private set; }
    public bool IsLeft { get; private set; }

    private Result() { }

    public static Result<Error, Success> FromLeft(Error left)
    {
        return new Result<Error, Success> { Left = left, IsLeft = true };
    }

    public static Result<Error, Success> FromRight(Success right)
    {
        return new Result<Error, Success> { Right = right, IsLeft = false };
    }
}