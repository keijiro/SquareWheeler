using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public sealed class TireController : MonoBehaviour
{
    [SerializeField] HingeJoint[] _joints = null;
    [SerializeField] InputAction _accelKey = null;
    [SerializeField] string _uiButtonName = "button";
    [SerializeField] float _force = 500;
    [SerializeField] float _velocity = 500;
    [SerializeField] ParticleSystem _effect = null;
    [SerializeField] float _effectRate = 10;

    Button _button;
    bool _pressed;

    void OnEnable()
    {
        _button = FindFirstObjectByType<UIDocument>()
          .rootVisualElement.Q<Button>(_uiButtonName);

        _accelKey.Enable();

        _button.RegisterCallback<PointerDownEvent>(ButtonEvent, TrickleDown.TrickleDown);
        _button.RegisterCallback<PointerUpEvent>(ButtonEvent, TrickleDown.TrickleDown);
    }

    void OnDisable()
    {
        _accelKey.Disable();

        _button.UnregisterCallback<PointerDownEvent>(ButtonEvent);
        _button.UnregisterCallback<PointerUpEvent>(ButtonEvent);
    }

    void ButtonEvent<TEvent>(PointerEventBase<TEvent> evt)
      where TEvent : PointerEventBase<TEvent>, new()
    {
        _pressed |=  (evt.eventTypeId == PointerDownEvent.TypeId());
        _pressed &= !(evt.eventTypeId == PointerUpEvent.TypeId());
    }

    void Update()
    {
        var accel = _pressed ? 1.0f : _accelKey.ReadValue<float>();

        var motor = new JointMotor()
          { force = _force * accel, targetVelocity = _velocity * accel };

        foreach (var joint in _joints) joint.motor = motor;

        var em = _effect.emission;
        em.rateOverTime = _effectRate * accel;
    }
}
