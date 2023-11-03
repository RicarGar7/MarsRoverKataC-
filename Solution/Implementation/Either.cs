namespace Test;

public class Either<TLeft, TRight>
{
    public TLeft Left { get; private set; }
    public TRight Right { get; private set; }
    public bool IsLeft { get; private set; }

    private Either() { }

    public static Either<TLeft, TRight> FromLeft(TLeft left)
    {
        return new Either<TLeft, TRight> { Left = left, IsLeft = true };
    }

    public static Either<TLeft, TRight> FromRight(TRight right)
    {
        return new Either<TLeft, TRight> { Right = right, IsLeft = false };
    }
}