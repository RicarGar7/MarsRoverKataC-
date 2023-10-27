namespace Test.Facing;

public class South : Implementation.Facing.Facing
{
    public Implementation.Facing.Facing WhatIsRight()
    {
        return new West();
    }

    public Implementation.Facing.Facing WhatIsLeft()
    {
        return new East();
    }

    public FacingValue Value()
    {
        return FacingValue.S;
    }
}