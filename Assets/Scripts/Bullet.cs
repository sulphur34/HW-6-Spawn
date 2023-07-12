using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _timeToDestroy;

    private void Start()
    {
        _timeToDestroy = 2;
        Destroy(gameObject, _timeToDestroy);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
