using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    // Ball object that the player avatar throw
    [SerializeField]
    private GameObject Ball;
    // Start position of the trajectory which is the player position
    [SerializeField]
    private Vector3 StartingPossition;
    // The initial velocity of the ball that is used for calculating the trajectory
    [SerializeField]
    private Vector3 InitialVelocity;

    // Determines the smoothness of the trajectory line. THe higher the count the smoother the line
    [SerializeField]
    private int Resolution = 10;

    // length of the trajectory line
    [SerializeField]
    private int Length = 10;

    // The gravity constant used for calculating the trajectory and used by all the physic objects
    [SerializeField]
    private float Gravity = 5f;

    // Line renderer object that draws the trajectory
    [SerializeField]
    private LineRenderer LineRender;

    void Awake()
    {
        // Set the global gravity to the Gravity variable
        Physics.gravity = new Vector3(0, -Gravity, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // Set the StartingPossition to the current position of the player
        StartingPossition = gameObject.transform.position;

        //! For testing
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            InitialVelocity += Vector3.left * 1.1f;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            InitialVelocity += Vector3.right * 1.1f;
        }

        if (Input.GetKey(KeyCode.UpArrow) && (InitialVelocity.y <= 80f || InitialVelocity.z <= 120f))
        {
            InitialVelocity += Vector3.forward * 1.6f;
            InitialVelocity += Vector3.up * 1f;
            //TODO: Increase the length according to z and y
            //! Length max 20
            if (InitialVelocity.y % 9 == 0)
                Length += 2;

        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            InitialVelocity += Vector3.back * 1.6f;
            InitialVelocity += Vector3.down * 1f;

            if (InitialVelocity.y % 9 == 0)
                Length -= 2;

        }

        if (Input.GetKey(KeyCode.C))
        {
            Length += 1;
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject obj = Instantiate(Ball, StartingPossition, Quaternion.identity);
            obj.GetComponent<Rigidbody>().AddForce(InitialVelocity, ForceMode.Impulse);
        }
        //! For testing

        Trajectory();


    }

    private void Trajectory()
    {
        // Set the vertices count of the linerenderer to the length of the path
        LineRender.positionCount = Length;
        Vector3 prevPos = StartingPossition;
        float ty = InitialVelocity.y;

        for (int i = 0; i < Length; i++)
        {
            // Calculate the trajectory of the Ball, by diving the result by Resolution we get a smooth curves
            Vector3 nextPos = prevPos + Vector3.up * (ty / Resolution);
            nextPos.x += (InitialVelocity.x / Resolution);
            nextPos.z += (InitialVelocity.z / Resolution);
            // Set the position of the vertices of the linerenderer
            LineRender.SetPosition(i, prevPos);
            prevPos = nextPos;
            // Apply gravity
            ty -= Gravity;

        }
    }

}
