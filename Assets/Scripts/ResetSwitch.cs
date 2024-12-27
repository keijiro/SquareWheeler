using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public sealed class ResetSwitch : MonoBehaviour
{
    [field:SerializeField] public Vector3 ResetPoint { get; set; }

    [SerializeField] GameObject _prefab = null;
    [SerializeField] InputAction _trigger = null;

    void OnEnable()
      => _trigger.Enable();

    void OnDisable()
      => _trigger.Disable();

    void Update()
    {
        if (_trigger.WasPerformedThisFrame())
        {
            Destroy(GameObject.FindWithTag("Player"));
            Instantiate(_prefab, ResetPoint, Quaternion.identity);
        }
    }
}
