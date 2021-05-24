using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleB : Obstacles
{

    private Renderer _targetA, _targetB;
    private Material _activatedMaterial;

    private bool _isActivatedA = true;
    public float TurnAmount = 2f;

    public ObstacleB()
    {
        // TODO: Find a better way to get the objects, a more generic way
        _targetA = GameObject.Find("TargetA").GetComponent<Renderer>();
        _targetB = GameObject.Find("TargetB").GetComponent<Renderer>();
        _activatedMaterial = Resources.Load("Materials/Activated") as Material;

    }



    private void SetActiveMaterial()
    {
        if (_isActivatedA)
        {
            _targetA.material = _targetB.material;
            _targetB.material = _activatedMaterial;
            _isActivatedA = false;
            return;
        }
        _targetB.material = _targetA.material;
        _targetA.material = _activatedMaterial;
        _isActivatedA = true;

    }
    public override void Activate(BaseController player)
    {
        SetActiveMaterial();
        Door.transform.Translate(Vector3.right * TurnAmount);
        TurnAmount = TurnAmount * -1;
        SetPlayerCondition(player);
    }
}
