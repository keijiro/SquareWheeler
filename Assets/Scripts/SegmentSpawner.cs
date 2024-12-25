using UnityEngine;

public sealed class SegmentSpawner : MonoBehaviour
{
    [SerializeField] GameObject _prefab = null;
    [SerializeField] Transform _nextOrigin = null;

    void OnCollisionEnter(Collision collision)
    {
        Instantiate(_prefab, _nextOrigin.position, _nextOrigin.rotation);
        GetComponent<Collider>().enabled = false;
    }
}
