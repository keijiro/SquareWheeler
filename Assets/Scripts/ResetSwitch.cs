using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public sealed class ResetSwitch : MonoBehaviour
{
    [SerializeField] InputAction _trigger = null;

    void OnEnable()
      => _trigger.Enable();

    void OnDisable()
      => _trigger.Disable();

    void Update()
    {
        if (_trigger.WasPerformedThisFrame())
            SceneManager.LoadScene(0);
    }
}
