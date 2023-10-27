namespace Test.Facing;

public class West : Implementation.Facing.Facing
{
    public Implementation.Facing.Facing WhatIsRight()
    {
        return new North();
    }

    public Implementation.Facing.Facing WhatIsLeft()
    {
        return new South();
    }

    public FacingValue Value()
    {
        return FacingValue.W;
    }
}