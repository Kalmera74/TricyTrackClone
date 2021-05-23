using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2f;
    [SerializeField]
    private bool _didHit = false;

    private Rigidbody body;
    void Start()
    {
        body = GetComponent<Rigidbody>();
        body.AddForce(Vector3.forward * _speed, ForceMode.Impulse);
    }

    public void SetCondition()
    {

        SetDrag(0f);
        body.AddForce(Vector3.forward * _speed, ForceMode.Impulse);

    }

    private void SetDrag(float drag)
    {
        body.drag = drag;
    }
    // Update is called once per frame

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Door"))
        {
            //transform.Translate(Vector3.back * 2f);
            SetDrag(2f);
            body.AddForce(Vector3.back * _speed * 2f, ForceMode.Impulse);

        }

    }
}
