namespace Test;

public class Satellite
{
    public Map Recognize(Planet planet)
    {
        return new Map(planet._surface, planet._obstacles);
    }
}