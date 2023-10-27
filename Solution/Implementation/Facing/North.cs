namespace Test.Facing;

public class North : Implementation.Facing.Facing
{
    public Implementation.Facing.Facing WhatIsRight()
    {
        return new East();
    }

    public Implementation.Facing.Facing WhatIsLeft()
    {
        return new West();
    }

    public FacingValue Value()
    {
        return FacingValue.N;
    }
}