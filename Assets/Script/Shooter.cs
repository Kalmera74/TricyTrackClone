using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Ball;
    public Vector3 StartingPossition;
    public Vector3 InitialVelocity;
    public int Resolution = 10;
    public int Length = 10;
    public float Gravity = 5f;
    public LineRenderer LineRender;

    void Start()
    {
        //! Get the mouse position and calculate the velocity accorting to it
        //! Calculate the end position from starting position and velocity
        //! Interpolate between the two position by the resolution amount to get intermediate positions
        //! instantiate simple spheres the amount of the resolution and assign their position to the intermediatory positions

        Physics.gravity = new Vector3(0, -Gravity, 0);





    }

    // Update is called once per frame
    void Update()
    {



        StartingPossition = gameObject.transform.position;

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
            obj.GetComponent<Rigidbody>().isKinematic = false;
            obj.GetComponent<Rigidbody>().AddForce(InitialVelocity, ForceMode.Impulse);
        }

        Trajectory();


    }

    private void Trajectory()
    {
        LineRender.positionCount = Length;
        Vector3 prevPos = StartingPossition;
        // InitialVelocity *= 4f;
        float ty = InitialVelocity.y;

        for (int i = 0; i < Length; i++)
        {
            Vector3 nextPos = prevPos + Vector3.up * (ty / Resolution);
            nextPos.x += (InitialVelocity.x / Resolution);
            nextPos.z += (InitialVelocity.z / Resolution);
            LineRender.SetPosition(i, prevPos);
            prevPos = nextPos;
            ty -= Gravity;

        }
    }




}
