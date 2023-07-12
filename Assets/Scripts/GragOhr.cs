using UnityEngine;

[RequireComponent(typeof(Animator))]

public class GragOhr : MonoBehaviour
{
    private const string MoveX = "moveX";
    private const string ShootRange = "ShootRange";
    private const string Dead = "Dead";
    
    [SerializeField] private float _speed;

    private Transform _target;
    private Animator _animator;
    private bool _isDead;
    private int _moveXIndex;
    private int _shootRangeIndex;
    private int _deadIndex;
    private bool _isFlip;
    private float _direction;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _isDead = false;
        _moveXIndex = Animator.StringToHash(MoveX);
        _shootRangeIndex = Animator.StringToHash(ShootRange);
        _deadIndex = Animator.StringToHash(Dead);
        FindPlayer();
    }

    // Update is called once per frame
    private void Update() 
    {
        float shootingRange = 1f;

        if (_isDead == false)
        {
            float distance = Mathf.Abs((_target.position.x - transform.position.x));
            _animator.SetFloat(_shootRangeIndex, distance);

            if (distance > shootingRange)
                MoveTowardsTarget();
            else
                _animator.SetFloat(_moveXIndex, 0);   
        }        
    }

    private void FindPlayer()
    {
        string playerTag = "Player";

        var player = GameObject.FindGameObjectWithTag(playerTag);

        if (player != null)
        {
            _target = player.transform;
        }
    }

    private void MoveTowardsTarget()
    {
        var moveVector = Vector2.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
        _animator.SetFloat(_moveXIndex, Mathf.Abs(moveVector.x));
        transform.position = moveVector;
        _direction = moveVector.x > 0 ? 1 : -1;
        Flip();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float bulletforce = 10f;
        float timeToDestroy = 1;
        collision.gameObject.GetComponent<Rigidbody>();
        Vector3 impactForce = collision.relativeVelocity;

        if (impactForce.magnitude > bulletforce && _isDead == false)
        {
            _animator.SetTrigger(_deadIndex);
            _isDead = true;
            Destroy(gameObject, timeToDestroy);
        }
    }

    private void Flip()
    {
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
}
