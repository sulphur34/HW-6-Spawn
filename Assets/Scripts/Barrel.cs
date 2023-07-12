using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Barrel : MonoBehaviour
{
    private const string Damage = "Damage";

    private Animator _animator;
    private int _damageIndex;

    private void Start()
    {        
        _animator = GetComponent<Animator>();
        _damageIndex = Animator.StringToHash(Damage);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        float bulletForce = 14f;
        Vector3 impactForce = collision.relativeVelocity;

        if (impactForce.magnitude > bulletForce)
            _animator.SetTrigger(_damageIndex);
    }
}
