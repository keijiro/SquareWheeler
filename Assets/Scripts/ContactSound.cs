using UnityEngine;
using Unity.Collections;

public sealed class ContactSound : MonoBehaviour
{
    [SerializeField] Vector2 _impulseRange = new Vector2(3, 8);
    [SerializeField] Vector2 _volumeRange = new Vector2(0.5f, 0.8f);
    [SerializeField] Vector2 _pitchRange = new Vector2(0.6f, 0.8f);

    float _impulseSum;

    void OnEnable()
      => Physics.ContactEvent += ContactEvent;

    void OnDisable()
      => Physics.ContactEvent -= ContactEvent;

    void Update()
    {
        if (_impulseSum > _impulseRange.x)
        {
            var pow = Mathf.Clamp01((_impulseSum - _impulseRange.x) /
                                    (_impulseRange.y - _impulseRange.x));
            var vol = Mathf.Lerp(_volumeRange.x, _volumeRange.y, pow);

            var audio = GetComponent<AudioSource>();
            audio.pitch = Random.Range(_pitchRange.x, _pitchRange.y);
            audio.PlayOneShot(audio.clip, vol);
        }

        _impulseSum = 0;
    }

    void ContactEvent(PhysicsScene scene, NativeArray<ContactPairHeader>.ReadOnly pairHeaders)
    {
        foreach (var header in pairHeaders)
            for (var i = 0; i < header.pairCount; i++)
                _impulseSum += header.GetContactPair(i).impulseSum.magnitude;
    }
}
