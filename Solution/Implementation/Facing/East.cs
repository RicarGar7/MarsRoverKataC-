namespace Test.Facing;

public class East : Implementation.Facing.Facing
{
    public Implementation.Facing.Facing WhatIsRight()
    {
        return new South();
    }

    public Implementation.Facing.Facing WhatIsLeft()
    {
        return new North();
    }

    public FacingValue Value()
    {
        return FacingValue.E;
    }
}