namespace Test;

public class PlanetTest
{
    [Fact]
    public void Should_Create()
    {
        var planet = Planet.Create(
            new Surface(10, 10),
            new List<Obstacle>()
            {
                new(new Position(5, 5))
            }
        );

        Assert.NotNull(planet);
    }

    [Fact]
    public void Should_Not_CreateWhenAnArtifactIsOut()
    {
        Assert.Throws<ObstacleDeclaredOutOfThePlanetException>(() =>
            Planet.Create(
                new Surface(10, 10),
                new List<Obstacle>()
                {
                    new(new Position(11, 11))
                }
            ));
    }
}