namespace Test;

public class Map
{
    public Surface _surface;
    public readonly List<Obstacle> _obstacles;

    public Map(Surface surface, List<Obstacle> obstacles)
    {
        _surface = surface;
        _obstacles = obstacles;
    }
}