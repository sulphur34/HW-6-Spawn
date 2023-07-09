using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private Vector2 _muzzleEndPosition;

    private float _baseOrientationDirection = 1;
    private float _currentDirection;

    private void ShootEvent()
    {
        if (transform.rotation.y < 0)
            _currentDirection = -_baseOrientationDirection;
        else
            _currentDirection = _baseOrientationDirection;
        
        Vector3 currentPosition = transform.position + new Vector3(_muzzleEndPosition.x * _currentDirection, _muzzleEndPosition.y, 0f);
        var projectile = Instantiate(_bullet, currentPosition, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(_bulletSpeed * _currentDirection, 0f);
    }
}
