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

    public bool HasAnyObstacle()
    {
        return _obstacles.Count > 0;
    }

    public bool HasObstacles(Position position)
    {
        return _obstacles.Any(obstacle => obstacle._position._latitude == position._latitude 
                                          && obstacle._position._longitude == position._longitude
                                          );
    }
    
    public bool IsTargetPositionInTheEdge(Position position)
    {
        return _surface.IsOut(position._latitude) || _surface.IsOut(position._longitude);
    }
    
}