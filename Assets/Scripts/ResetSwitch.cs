using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public sealed class ResetSwitch : MonoBehaviour
{
    [field:SerializeField] public Vector3 ResetPoint { get; set; }

    [SerializeField] GameObject _prefab = null;
    [SerializeField] InputAction _trigger = null;
    [SerializeField] string _uiButtonName = "give-up-button";

    Button _button;

    void OnEnable()
    {
        _button = FindFirstObjectByType<UIDocument>()
          .rootVisualElement.Q<Button>(_uiButtonName);

        _trigger.Enable();

        _button.clicked += ResetPlayer;
    }

    void OnDisable()
    {
        _trigger.Disable();

        _button.clicked -= ResetPlayer;
    }

    void Update()
    {
        if (_trigger.WasPerformedThisFrame()) ResetPlayer();
    }

    void ResetPlayer()
    {
        Destroy(GameObject.FindWithTag("Player"));
        Instantiate(_prefab, ResetPoint, Quaternion.identity);
    }
}
