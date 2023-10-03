namespace Test;

public class PositionTest
{
    [Fact]
    public void Should_NotBeDeclarableWithNegativeLatitudeOrLongitude()
    {
        Assert.Throws<CouldNotDeclarePositionException>(()=>Position.Declare(-1,10));
        Assert.Throws<CouldNotDeclarePositionException>(()=>Position.Declare(1,-10));
        Assert.Throws<CouldNotDeclarePositionException>(()=>Position.Declare(-1,-10));
    }
}