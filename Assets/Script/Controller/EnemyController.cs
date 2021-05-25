using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BaseController
{

    void Start()
    {
        SetBody();
        SetShooter();

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
