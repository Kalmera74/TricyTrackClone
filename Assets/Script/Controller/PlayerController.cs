using UnityEngine;

public class PlayerController : BaseController
{

    void Start()
    {
        SetBody(GetComponent<Rigidbody>());

        Accelerate();
    }



    // Update is called once per frame

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Door"))
        {
            //transform.Translate(Vector3.back * 2f);
            Decelerate();


        }

    }


}
