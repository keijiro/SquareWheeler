using UnityEngine;
using UnityEngine.UIElements;

public sealed class Checkpoint : MonoBehaviour
{
    [SerializeField] GameObject _prefab = null;
    [SerializeField] Transform _spawnPoint = null;
    [SerializeField] float _stride = 50;

    Label _label;
    bool _touched;

    async void StartBlinkLabel()
    {
        for (var i = 0; i < 12; i++)
        {
            _label.visible ^= true;
            await Awaitable.WaitForSecondsAsync(0.2f);
        }
    }

    void OnCollisionEnter(Collision collision)
      => _touched = true;

    void Start()
      => _label = FindFirstObjectByType<UIDocument>()
                    .rootVisualElement.Q<Label>("checkpoint-label");

    void Update()
    {
        if (_touched)
        {
            Instantiate(_prefab, _spawnPoint.position, Quaternion.identity);
            transform.position += Vector3.forward * _stride;
            StartBlinkLabel();
            GetComponent<AudioSource>().Play();
            _touched = false;
        }
    }
}
