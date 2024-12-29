using UnityEngine;
using Klak.Math;

public sealed class EngineSound : MonoBehaviour
{
    [SerializeField] HingeJoint[] _joints = null;
    [SerializeField] float _maxVelocity = 2000;
    [SerializeField] Vector2 _pitchRange = new Vector2(0.5f, 1.8f);
    [SerializeField] Vector2 _volumeRange = new Vector2(0.3f, 0.6f);
    [SerializeField] float _tweenSpeed = 8;

    AudioSource _audio;

    void Start()
      => _audio = GetComponent<AudioSource>();

    void Update()
    {
        var total = 0.0f;
        foreach (var joint in _joints) total += joint.velocity;

        var p = Mathf.Clamp01(total / _maxVelocity);
        var pitch = Mathf.Lerp(_pitchRange.x, _pitchRange.y, p);
        var volume = Mathf.Lerp(_volumeRange.x, _volumeRange.y, p);

        _audio.pitch = ExpTween.Step(_audio.pitch, pitch, _tweenSpeed);
        _audio.volume = ExpTween.Step(_audio.volume, volume, _tweenSpeed);
    }
}
