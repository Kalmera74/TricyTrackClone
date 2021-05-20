
public static class ObstacleFactory
{
    //! This factory generates IObstacle types, since UnityEngine doesn't allow Interfaces to be assigned from Inspector we are using a factory
    //! This kinda violate Open/Close principle of SOLID but it's the best solution right now. It collects all the IObstacle type into a single entity
    public enum ObstacleType { A, B, C };
    public static IObstacle GetObstacle(ObstacleType type)
    {
        switch (type)
        {
            case ObstacleType.A: return new ObstacleA(); break;
            case ObstacleType.B: return new ObstacleB(); break;
            case ObstacleType.C: return new ObstacleC(); break;
            default: return null;
        }
    }

}
