using UnityEngine;

public sealed class PopupMotion : MonoBehaviour
{
    [field:SerializeField] public float Length { get; set; } = 5;
    [field:SerializeField] public float Duration { get; set; } = 2;

    float _time;

    void Update()
    {
        _time = Mathf.Clamp01(_time + Time.deltaTime / Duration);
        var y = Mathf.Pow(1 - _time, 3) * -Length;
        var p = transform.localPosition;
        p.y = y;
        transform.localPosition = p;
    }
}
