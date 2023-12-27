using UnityEngine;

public class PlayerController : Controller
{
    [SerializeField] private float _speed;

    [SerializeField] private Transform _body;

    [SerializeField] private Animator _animator;
    
    private Vector3 _input;

    private Rigidbody _rigidbody;

    private const string _horizontalAxis = "Horizontal";
    private const string _verticalAxis = "Vertical";

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _input = Vector3.zero;
    }

    private void Update()
    {
        Inputs();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Inputs()
    {
        _input.x = Input.GetAxis(_horizontalAxis);
        _input.z = Input.GetAxis(_verticalAxis);
    }

    private void Move()
    {
        Vector3 move = _input;

        _rigidbody.MovePosition(transform.position + move * _speed * Time.fixedDeltaTime);

        if (move != Vector3.zero)
        {
            float angle = Mathf.Atan2(move.z, -move.x) * Mathf.Rad2Deg - 90;
            _body.rotation = Quaternion.AngleAxis(angle, Vector3.up);
            _animator.SetBool("IsRun", true);
        }
        else
        {
            _animator.SetBool("IsRun", false);
        }
    }
}