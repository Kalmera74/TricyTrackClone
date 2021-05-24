using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    public float Acceleration = 2f;
    public float Deceleration = 2f;

    private bool _hasStopped = true;

    private Rigidbody _body;

    public void ToggleStop()
    {
        _hasStopped = !_hasStopped;
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
    protected void SetBody(Rigidbody body)
    {
        _body = body;
    }
}
