using UnityEngine;

// * This class uses Collision to activate the IObstacle objects
public class ObstacleImplementer : MonoBehaviour
{

    private BaseController _player;
    private BaseController _enemy;
    private IObstacle ObstacleBehaviour;

    public string TargetTag = "Bullseye";
    public string DoorTag = "Door";

    // With this type we get the related class from the factory
    public ObstacleFactory.ObstacleType ObstacleType;

    void Start()
    {
        //! Because the Unity Engine does not allow Interfaces to be assigned from Inspector we are using a factory to get the Strategies for our obstacle behaviour
        //! Therefore, it's not the best solution but it still gives more flexibility and reduce coupling 

        ObstacleBehaviour = ObstacleFactory.GetObstacle(ObstacleType);
        ObstacleBehaviour.SetDoor(GetChildrenWithTag(DoorTag));
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyController>();

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
        // Get the child that has been hit bit either the Ball, the Player, or the Enemy
        Collider myCollider = collision.contacts[0].thisCollider;
        // Check is the thing that hit the child is Ball, if so destroy it
        if (collision.collider.CompareTag("Ball")) Destroy(collision.collider.gameObject);



        if (myCollider.CompareTag(TargetTag))
        {
            ObstacleBehaviour.Activate(collision.collider.GetComponent<Bullet>().Owner);
        }

    }

}
