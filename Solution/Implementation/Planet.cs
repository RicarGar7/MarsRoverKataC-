namespace Test;

public class Planet
{
    public readonly Surface _surface;
    public readonly List<Obstacle> _obstacles;

    private Planet(Surface surface, List<Obstacle> obstacles)
    {
        _surface = surface;
        _obstacles = obstacles;
    }

    public static Planet Create(Surface surface, List<Obstacle> artifacts)
    {
        if (!artifacts.All(artifact =>
                surface.CanBeContained(artifact._position._latitude, artifact._position._longitude)))
        {
            throw new ObstacleDeclaredOutOfThePlanetException();
        }

        return new Planet(surface, artifacts);
    }
}

public class ObstacleDeclaredOutOfThePlanetException : Exception
{
    public ObstacleDeclaredOutOfThePlanetException() : base("One or more obstacle are declared out of the planet surface")
    {
    }
}