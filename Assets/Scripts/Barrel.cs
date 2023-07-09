using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Barrel : MonoBehaviour
{
    private Animator _animator;
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        float bulletforce = 14f;
        collision.gameObject.GetComponent<Rigidbody>();
        Vector3 impactForce = collision.relativeVelocity;

        if (impactForce.magnitude > bulletforce)
            _animator.SetTrigger("Damage");
    }
}
