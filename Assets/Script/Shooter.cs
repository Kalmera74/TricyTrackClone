using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    // Ball object that the player avatar throw
    [SerializeField]
    private GameObject _ball;
    // Start position of the trajectory which is the player position
    [SerializeField]
    private Vector3 _startingPosition;
    // The initial velocity of the ball that is used for calculating the trajectory
    [SerializeField]
    private Vector3 _initialVelocity;

    // Determines the smoothness of the trajectory line. THe higher the count the smoother the line
    [SerializeField]
    private int _resolution = 10;

    // length of the trajectory line
    [SerializeField]
    private int _length = 10;

    // The gravity constant used for calculating the trajectory and used by all the physic objects
    [SerializeField]
    private float _gravity = 5f;

    // Line renderer object that draws the trajectory
    [SerializeField]
    private LineRenderer _lineRenderer;

    [SerializeField]
    private float _minSwipeLength = 200f;
    private Vector2 _firstPressPos;
    private Vector2 _secondPressPos;
    private Vector2 _currentSwipe;
    public bool DidEnterFinishLine { set; get; } = false;
    private bool _canShoot { get; set; } = true;

    [SerializeField]
    private float _timer = 1f;
    [SerializeField]
    private float _cooldown = .7f;

    void Awake()
    {
        // Set the global gravity to the Gravity variable
        Physics.gravity = new Vector3(0, -_gravity, 0);
    }

    public void SetInitialVelocity(Vector3 velocity)
    {
        _initialVelocity = velocity;
    }


    // Update is called once per frame
    void Update()
    {
        // TODO: Should refractore this whole class. It's too messy and hard to change anything here.
        //! Shouldn't do this
        if (!gameObject.CompareTag("Player")) return;
        _startingPosition = transform.position;
        if (Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);

            if (t.phase == TouchPhase.Began)
            {
                _firstPressPos = new Vector2(t.position.x, t.position.y);

            }
            else if (t.phase == TouchPhase.Ended)
            {
                Shoot();
                ResetPath();
            }


            if (t.phase == TouchPhase.Ended || t.phase == TouchPhase.Moved)
            {
                _secondPressPos = new Vector2(t.position.x, t.position.y);
                _currentSwipe = new Vector3(_secondPressPos.x - _firstPressPos.x, _secondPressPos.y - _firstPressPos.y);

                // Make sure it was a legit swipe, not a tap
                if (_currentSwipe.magnitude < _minSwipeLength) { return; }

                _currentSwipe.Normalize();

                if (_currentSwipe.y > 0 && _currentSwipe.x > -0.5f && _currentSwipe.x < 0.5f)
                {
                    if (_initialVelocity.y <= 45f || _initialVelocity.z <= 40f)
                    {
                        _initialVelocity += Vector3.forward * 1.6f;
                        _initialVelocity += Vector3.up * 1f;
                        if (_initialVelocity.y % 7 == 0 && _length <= 20)
                        {
                            _length += 2;
                        }
                    }
                }
                if (_currentSwipe.y < 0 && _currentSwipe.x > -0.5f && _currentSwipe.x < 0.5f)
                {
                    if (_initialVelocity.y >= 0 || _initialVelocity.z >= 0)
                    {
                        _initialVelocity += Vector3.back * 1.6f;
                        _initialVelocity += Vector3.down * 1f;

                        if (_initialVelocity.y % 7 == 0 && _length > 0)
                        {
                            _length -= 2;
                        }
                    }

                }
                if (_currentSwipe.x < 0 && _currentSwipe.y > -0.5f && _currentSwipe.y < 0.5f)
                {
                    _initialVelocity += Vector3.left * 1.3f;
                }
                if (_currentSwipe.x > 0 && _currentSwipe.y > -0.5f && _currentSwipe.y < 0.5f)
                {
                    _initialVelocity += Vector3.right * 1.3f;
                }


            }
            CalculateTrajectory();

            if (DidEnterFinishLine)
            {
                EndlessShooter();
            }
        }



    }

    private void EndlessShooter()
    {
        _timer -= Time.deltaTime;
        if (_timer <= _cooldown)
        {
            _timer = 1f;
            _canShoot = true;

        }
        else if (_canShoot)
        {
            _canShoot = false;
            Shoot();
            //     ResetPath();

        }
    }

    private void ResetPath()
    {
        _lineRenderer.positionCount = 0;
        _initialVelocity = Vector3.zero;
        _length = 2;

    }

    public void Shoot()
    {
        // TODO: Refractor this to use Object Pooling instead
        GameObject obj = Instantiate(_ball, _startingPosition, Quaternion.identity);
        obj.GetComponent<Rigidbody>().AddForce(_initialVelocity, ForceMode.Impulse);
        obj.GetComponent<Bullet>().Owner = GetComponent<BaseController>();
        Destroy(obj, 5f);

    }
    private void CalculateTrajectory()
    {
        // Set the vertices count of the linerenderer to the length of the path
        _lineRenderer.positionCount = _length;
        Vector3 prevPos = _startingPosition;
        float ty = _initialVelocity.y;

        for (int i = 0; i < _length; i++)
        {
            // Calculate the trajectory of the Ball, by diving the result by Resolution we get a smooth curves
            Vector3 nextPos = prevPos + Vector3.up * (ty / _resolution);
            nextPos.x += (_initialVelocity.x / _resolution);
            nextPos.z += (_initialVelocity.z / _resolution);
            // Set the position of the vertices of the linerenderer
            _lineRenderer.SetPosition(i, prevPos);
            prevPos = nextPos;
            // Apply gravity
            ty -= _gravity;

        }
    }

}
