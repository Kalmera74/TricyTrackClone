using UnityEngine;
public interface IObstacle
{
    // * Using Interface allows us to easy add and remove obstacle behaviours, when we need a new behaviour we can just implement IObstacle

    void Activate(BaseController player);
    void SetDoor(GameObject door);


}

