using UnityEngine;
using UnityEngine.UIElements;

public sealed class Checkpoint : MonoBehaviour
{
    [SerializeField] UIDocument _ui = null;
    [SerializeField] AudioSource _audio = null;
    [SerializeField] ParticleSystem _confetti = null;
    [SerializeField] GameObject _prefab = null;
    [SerializeField] Transform _spawnPoint = null;
    [SerializeField] float _stride = 50;

    Label _label;
    bool _touched;

    static Color Get8BitColor(int i)
      => new Color((i & 1) > 0 ? 1 : 0,
                   (i & 2) > 0 ? 1 : 0,
                   (i & 4) > 0 ? 1 : 0, 1);

    async void StartBlinkLabel()
    {
        _label.visible = true;

        var style = _label.style;
        for (var i = 0; i < 32; i++)
        {
            style.color = Get8BitColor(i);
            await Awaitable.WaitForSecondsAsync(0.1f);
        }

        _label.visible = false;
    }

    void OnCollisionEnter(Collision collision)
      => _touched = true;

    void Start()
      => _label = _ui.rootVisualElement.Q<Label>("checkpoint-label");

    void Update()
    {
        if (_touched)
        {
            StartBlinkLabel();

            _confetti.Emit(500);
            _audio.Play();

            Instantiate(_prefab, _spawnPoint.position, Quaternion.identity);
            transform.position += Vector3.forward * _stride;

            _touched = false;
        }
    }
}
