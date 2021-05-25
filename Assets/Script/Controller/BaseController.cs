using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    [SerializeField]
    protected float Acceleration = 3f;

    [SerializeField]
    protected float Deceleration = 3f;

    private bool _hasStopped = true;

    private Rigidbody _body;

    protected Shooter _shooter;
    public void ToggleStop()
    {
        _hasStopped = !_hasStopped;
    }

    protected void SetShooter()
    {
        _shooter = GetComponent<Shooter>();
    }

    public void SetCondition()
    {
        if (_hasStopped)
        {
            Accelerate();

        }
    }

    protected void Accelerate()
    {

        SetDrag(0f);
        _body.AddForce(Vector3.forward * Acceleration, ForceMode.Impulse);
        ToggleStop();

    }

    protected void Decelerate()
    {
        SetDrag(2f);
        _body.AddForce(Vector3.back * Acceleration * Deceleration, ForceMode.Impulse);
        ToggleStop();
    }
    protected void SetDrag(float drag)
    {
        _body.drag = drag;
    }
    protected void SetBody()
    {
        _body = GetComponent<Rigidbody>();
    }
}
