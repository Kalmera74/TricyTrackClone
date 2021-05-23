using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacles : MonoBehaviour, IObstacle
{
    // * With this abstract class implementing IObstacle we collect the obstacles with similar behaviour and functions (eg. activate a door, and effect player) together
    // * and ensure SOLID and DRY
    public GameObject Door;
    public abstract void Activate(PlayerController player);

    public void SetDoor(GameObject door)
    {
        if (Door != null) throw new MissingReferenceException();

        Door = door;
    }

    public void SetPlayerCondition(PlayerController player)
    {
        if (player == null) throw new MissingReferenceException();

        player.SetCondition();
    }


}