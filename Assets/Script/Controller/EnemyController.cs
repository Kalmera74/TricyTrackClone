using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BaseController
{
    private Shooter _shooter;


    void Start()
    {
        _shooter = GetComponent<Shooter>();
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
