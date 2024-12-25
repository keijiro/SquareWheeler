using UnityEngine;
using UnityEngine.InputSystem;

public sealed class TireController : MonoBehaviour
{
    [SerializeField] HingeJoint[] _joints = null;
    [SerializeField] InputAction _accelKey = null;
    [SerializeField] float _force = 500;
    [SerializeField] float _velocity = 500;

    void OnEnable()
      => _accelKey.Enable();

    void OnDisable()
      => _accelKey.Disable();

    void Update()
    {
        var accel = _accelKey.ReadValue<float>();

        var motor = new JointMotor()
          { force = _force * accel, targetVelocity = _velocity * accel };

        foreach (var joint in _joints) joint.motor = motor;
    }
}
