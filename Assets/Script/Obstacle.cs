using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{



    private IObstacle ObstacleBehaviour;
    public ObstacleFactory.ObstacleType ObstacleType;

    void Start()
    {
        //! Because the Unity Engine does not allow Interfaces to be assigned from Inspector we are using a factory to get the Strategies for our obstacle behaviour
        ObstacleBehaviour = ObstacleFactory.GetObstacle(ObstacleType);
    }

    void OnCollisionEnter(Collision collision)
    {
        Collider myCollider = collision.contacts[0].thisCollider;
        Destroy(collision.collider.gameObject);

        if (myCollider.CompareTag("Bullseye"))
        {
            ObstacleBehaviour.Activate();

        }

    }

}
