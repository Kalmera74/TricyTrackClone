using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObstacleC : Obstacles
{
    private Transform _target;
    public override void Activate(BaseController player)
    {
        _target.parent.gameObject.SetActive(false);
    }

    public override void SetDoor(GameObject door)
    {
        base.SetDoor(door);
        //  _target = Door.transform.GetChild(0);
        _target = Door.transform.parent.GetChild(0);
        _target.Translate(Random.insideUnitCircle * 3);
    }
}
