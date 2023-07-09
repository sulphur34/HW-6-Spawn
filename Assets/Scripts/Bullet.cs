using UnityEngine;

public class Bullet : MonoBehaviour
{        
    private void Update()
    {
        Destroy(gameObject, 2);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
