using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2f;
    [SerializeField]
    private bool _didHit = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!_didHit)
            transform.Translate(Vector3.forward * Time.deltaTime * _speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Door"))
            _didHit = true;
    }
}
