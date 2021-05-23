
public static class ObstacleFactory
{
    //! This factory generates IObstacle types, since UnityEngine doesn't allow Interfaces to be assigned from Inspector we are using a factory
    //! This kinda violate Open/Close principle of SOLID but it's the best solution right now. It collects all the IObstacle type into a single entity
    //! We could use reflection to improve it but then the enum would be a problem to be used in the inspector
    public enum ObstacleType { A, B };
    public static IObstacle GetObstacle(ObstacleType type)
    {
        switch (type)
        {
            case ObstacleType.A: return new ObstacleA(); break;
            case ObstacleType.B: return new ObstacleB(); break;
            default: return null;
        }
    }

}
