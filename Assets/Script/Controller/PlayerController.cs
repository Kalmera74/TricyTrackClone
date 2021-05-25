using UnityEngine;

public class PlayerController : BaseController
{

    public Transform Camera;
    void Start()
    {
        SetBody();
        SetShooter();

        Accelerate();
    }

    private void Loose()
    {
        Decelerate();

    }

    private void SetCameraForFinal()
    {



    }

    private void Finish()
    {

    }

    private void ShootEndlessly()
    {
        _shooter.DidEnterFinishLine = true;
        SetCameraForFinal();
    }

    // Update is called once per frame

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Door"))
        {
            Decelerate();
        }
        else if (other.CompareTag("FinDoor"))
        {
            Loose();
        }
        else if (other.CompareTag("FinalLine"))
        {

            ShootEndlessly();
        }

    }


}
