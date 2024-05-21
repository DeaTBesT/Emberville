using UnityEngine;

public class PlayerController : Controller
{
    [SerializeField] private float _speed;

    [SerializeField] private Transform _body;

    [SerializeField] private Animator _animator;
    
    private Vector3 _input;
    private Rigidbody _rigidbody;

    private const string HORIZONTAL_AXIS = "Horizontal";
    private const string VERTICAL_AXIS = "Vertical";

    private readonly int RUN_ANIMATION = Animator.StringToHash("IsRun");

    public bool IsMove { get; set; } = true;
    
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
        _input.x = Input.GetAxis(HORIZONTAL_AXIS);
        _input.z = Input.GetAxis(VERTICAL_AXIS);
    }

    private void Move()
    {
        if (!IsMove)
        {
            return;
        }
        
        Vector3 move = _input;

        _rigidbody.MovePosition(transform.position + move * _speed * Time.fixedDeltaTime);

        if (move != Vector3.zero)
        {
            float angle = Mathf.Atan2(move.z, -move.x) * Mathf.Rad2Deg - 90;
            _body.rotation = Quaternion.AngleAxis(angle, Vector3.up);
            _animator.SetBool(RUN_ANIMATION, true);
        }
        else
        {
            _animator.SetBool(RUN_ANIMATION, false);
        }
    }
}