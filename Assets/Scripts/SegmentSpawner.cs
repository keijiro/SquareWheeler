using UnityEngine;

public sealed class SegmentSpawner : MonoBehaviour
{
    [SerializeField] GameObject _prefab = null;
    [SerializeField] Transform _spawnPoint = null;
    [SerializeField] float _stride = 50;

    bool _touched;

    void OnCollisionEnter(Collision collision)
      => _touched = true;

    void Update()
    {
        if (_touched)
        {
            Instantiate(_prefab, _spawnPoint.position, Quaternion.identity);
            transform.position += Vector3.forward * _stride;
            _touched = false;
        }
    }
}
