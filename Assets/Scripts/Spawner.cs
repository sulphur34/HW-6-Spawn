using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _spawnObject;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private int _spawnedCyclesValue;
    [SerializeField] private Vector3[] _spawnCoordinates;
    
    private void Start()
    {
        StartCoroutine(SpawnWithDelay());
    }

    private IEnumerator SpawnWithDelay()
    {
        for (int i = 0; i < _spawnedCyclesValue; i++)
        {
            for (int j = 0; j < _spawnCoordinates.Length; j++)
            {
                Instantiate(_spawnObject, _spawnCoordinates[j], Quaternion.identity);
                yield return new WaitForSeconds(_spawnDelay);
            }
        }
    }
}
