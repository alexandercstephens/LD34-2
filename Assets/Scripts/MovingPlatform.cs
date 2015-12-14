using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{

    public Vector3 Destination;
    public float Speed;

    private Vector3 _origin;
    enum States { MovingToDestionation, MovingToOrigin }

    private States _state;
    private Vector3 _direction;
    private Rigidbody2D _rigidbody;

    // Use this for initialization
    void Start()
    {
        _origin = transform.position;
        _state = States.MovingToDestionation;
        _direction = (Destination - _origin).normalized;

        _rigidbody = GetComponent<Rigidbody2D>();
        SetDirection(_direction);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        switch (_state)
        {
            case States.MovingToDestionation:
                if (Vector3.Distance(transform.position, Destination) <= 0.1f)
                {
                    SetDirection(-_direction);
                    _state = States.MovingToOrigin;
                }
                break;

            case States.MovingToOrigin:
                if (Vector3.Distance(transform.position, _origin) <= 0.1f)
                {
                    SetDirection(_direction);
                    _state = States.MovingToDestionation;
                }
                break;
        }
    }

    void SetDirection(Vector3 direction)
    {
        _rigidbody.velocity = direction*Speed;
    }

}