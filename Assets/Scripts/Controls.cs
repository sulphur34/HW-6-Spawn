using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Controls : MonoBehaviour
{
    private const string Shoot = "Shoot";
    private const string IsArmed = "isArmed";
    private const string MoveX = "moveX";
    private const string Damage = "Damage";

    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;

    private Animator _animator;
    private Vector2 _moveVector;
    private int _shootIndex;
    private int _isArmedIndex;
    private int _moveXIndex;
    private int _damageIndex;
    private bool _isFlip;
    private float _direction;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _shootIndex = Animator.StringToHash(Shoot);
        _isArmedIndex = Animator.StringToHash(IsArmed);
        _moveXIndex = Animator.StringToHash(MoveX);
        _damageIndex = Animator.StringToHash(Damage);
        _isFlip = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _animator.SetTrigger(_shootIndex);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            bool isArmed = _animator.GetBool(_isArmedIndex);
            _animator.SetBool(_isArmedIndex, !isArmed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            _direction = 1;
            ProcessMovementKey(KeyCode.D);
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            _animator.SetFloat(_moveXIndex, 0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            _direction = -1;
            ProcessMovementKey(KeyCode.A);
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            _animator.SetFloat(_moveXIndex, 0);
        }
    }

    private void ProcessMovementKey(KeyCode keyCode)
    {
        Flip();

        if (Input.GetKey(keyCode) && Input.GetKey(KeyCode.LeftShift))
            Move(_runSpeed);
        else
            Move(_walkSpeed);
    }

    private void Move(float speed)
    {
        _moveVector.x = speed * Time.deltaTime;
        _animator.SetFloat(_moveXIndex, Mathf.Abs(_moveVector.x));
        transform.Translate(_moveVector.x, 0, 0);
    }

    private void Flip()
    {
        Debug.Log(_isFlip + " - " + _direction);
        if (_isFlip == false && _direction == -1)
        {
            transform.Rotate(0, 180, 0);
            _isFlip = true;
        }
        else if (_isFlip && _direction == 1)
        {
            transform.Rotate(0, -180, 0);
            _isFlip = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float bulletforce = 10f;
        collision.gameObject.GetComponent<Rigidbody>();
        Vector3 impactForce = collision.relativeVelocity;

        if (impactForce.magnitude > bulletforce)
            _animator.SetTrigger(_damageIndex);
    }
}
