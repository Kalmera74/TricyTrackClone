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

    public GameObject[] Path;
    void Start()
    {
        //! Get the mouse position and calculate the velocity accorting to it
        //! Calculate the end position from starting position and velocity
        //! Interpolate between the two position by the resolution amount to get intermediate positions
        //! instantiate simple spheres the amount of the resolution and assign their position to the intermediatory positions
        StartingPossition = gameObject.transform.position;
        Path = new GameObject[Length];
        Physics.gravity = new Vector3(0, -Gravity, 0);

        for (int i = 0; i < Path.Length; i++)
        {

            GameObject obj = Instantiate(Ball, StartingPossition, Quaternion.identity);
            obj.SetActive(false);
            Path[i] = obj;
        }




    }

    // Update is called once per frame
    void Update()
    {

        LineRender.positionCount = Length;

        StartingPossition = gameObject.transform.position;


        //InitialVelocity = Vector3.zero * 0.1.1f;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            InitialVelocity += Vector3.left * 1.1f;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            InitialVelocity += Vector3.right * 1.1f;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            InitialVelocity += Vector3.forward * 1.6f;
            InitialVelocity += Vector3.up * 1f;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            InitialVelocity += Vector3.back * 1.6f;
            InitialVelocity += Vector3.down * 1f;
        }



        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject obj = Instantiate(Ball, StartingPossition, Quaternion.identity);
            obj.GetComponent<Rigidbody>().isKinematic = false;
            obj.GetComponent<Rigidbody>().AddForce(InitialVelocity, ForceMode.Impulse);
        }

        Trajectory(ref Path);
        // Plot(StartingPossition, InitialVelocity, Length, ref Path);


    }

    private void Trajectory(ref GameObject[] path)
    {
        Vector3 prevPos = StartingPossition;
        // InitialVelocity *= 4f;
        float ty = InitialVelocity.y;

        for (int i = 0; i < Length; i++)
        {
            Vector3 nextPos = prevPos + Vector3.up * (ty / Resolution);
            nextPos.x += (InitialVelocity.x / Resolution);
            nextPos.z += (InitialVelocity.z / Resolution);
            prevPos = nextPos;
            // path[i].transform.position = nextPos;
            ty -= Gravity;
            LineRender.SetPosition(i, prevPos);
            //    Debug.Log(prevPos);
        }


        //  DrawPath(ref path);
    }

    private void DrawPath(ref GameObject[] path)
    {
        for (int i = 0; i < Mathf.Abs(StartingPossition.z - InitialVelocity.z) / 10; i++)
        {
            path[i].SetActive(true);
        }

    }

    private void Plot(Vector3 pos, Vector3 velocity, int length, ref GameObject[] path)
    {


        float timestep = Time.fixedDeltaTime / Physics.defaultSolverVelocityIterations;
        Vector3 gravityAccel = Physics.gravity * 1 * timestep * timestep;
        float drag = 1f - timestep * 0;
        Vector3 moveStep = velocity * timestep;

        for (int i = 0; i < length; ++i)
        {
            moveStep += gravityAccel;
            moveStep *= drag;
            pos += moveStep;
            path[i].transform.position = pos;

        }

        DrawPath(ref path);
    }

}
