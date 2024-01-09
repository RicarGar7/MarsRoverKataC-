namespace Test;

public class Planet
{
    internal readonly Surface _surface;
    internal readonly List<Obstacle> _obstacles;

    private Planet(Surface surface, List<Obstacle> obstacles)
    {
        _surface = surface;
        _obstacles = obstacles;
    }

    public static Planet Create(Surface surface, List<Obstacle> obstacles)
    {
        if (!obstacles.All(artifact =>
                surface.CanBeContained(artifact._position._latitude, artifact._position._longitude)))
        {
            throw new ObstacleDeclaredOutOfThePlanetException();
        }

        return new Planet(surface, obstacles);
    }

    public bool HasAnyObstacle()
    {
        return _obstacles.Count > 0;
    }

    public bool HasObstacles(Position position)
    {
        return Enumerable.Any<Obstacle>(_obstacles, obstacle => obstacle._position._latitude == position._latitude 
                                                                && obstacle._position._longitude == position._longitude
        );
    }

    public bool IsPositionOutOfThePlanet(Position position)
    {
        return _surface.IsOut(position._latitude) || _surface.IsOut(position._longitude);
    }
}

public class ObstacleDeclaredOutOfThePlanetException : Exception
{
    public ObstacleDeclaredOutOfThePlanetException() : base("One or more obstacle are declared out of the planet surface")
    {
    }
}