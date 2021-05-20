using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleA : MonoBehaviour, IObstacle
{
    public GameObject Door;
    private bool _isActivated = false;
    public ObstacleA()
    {
        Door = GameObject.FindGameObjectWithTag("Door");
    }

    //! Implement IObstacle method, Move the door object up or down depending on its current state when it gets hit
    public void Activate()
    {
        if (!_isActivated)
        {
            Door.transform.Translate(Vector3.up * 5f);
            _isActivated = true;

        }
        else
        {
            Door.transform.Translate(Vector3.down * 5f);
            _isActivated = false;


        }

    }

}
