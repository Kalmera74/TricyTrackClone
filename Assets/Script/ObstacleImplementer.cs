using UnityEngine;

// * This class uses Collision to activate the IObstacle objects
public class ObstacleImplementer : MonoBehaviour
{

    public BaseController Player;
    private IObstacle ObstacleBehaviour;

    // With this type we get the related class from the factory
    public ObstacleFactory.ObstacleType ObstacleType;

    void Start()
    {
        //! Because the Unity Engine does not allow Interfaces to be assigned from Inspector we are using a factory to get the Strategies for our obstacle behaviour
        //! Therefore, it's not the best solution but it still gives more flexibility and reduce coupling 

        ObstacleBehaviour = ObstacleFactory.GetObstacle(ObstacleType);
        ObstacleBehaviour.SetDoor(GetChildrenWithTag("Door"));
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

    }

    public GameObject GetChildrenWithTag(string tag)
    {
        foreach (Transform child in transform)
        {

            if (child.tag == tag)
            {

                return child.gameObject;
            }
        }
        return null;

    }

    void OnCollisionEnter(Collision collision)
    {
        Collider myCollider = collision.contacts[0].thisCollider;
        Destroy(collision.collider.gameObject);

        if (myCollider.CompareTag("Bullseye"))
        {
            ObstacleBehaviour.Activate(Player);
        }

    }

}
