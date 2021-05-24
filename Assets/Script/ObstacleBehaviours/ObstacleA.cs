using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleA : Obstacles
{
    private bool _isActivated = false;


    //! Implement IObstacle method, Move the door object up or down depending on its current state when it gets hit
    public override void Activate(BaseController Player)
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

        SetPlayerCondition(Player);
    }

}
