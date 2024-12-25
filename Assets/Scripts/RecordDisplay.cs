using UnityEngine;
using UnityEngine.UIElements;

public sealed class RecordDisplay : MonoBehaviour
{
    [SerializeField] Transform _basePoint = null;

    Label _label;

    void Start()
      => _label = GetComponent<UIDocument>().rootVisualElement.Q<Label>("record-label");

    void Update()
      => _label.text = $"Current Progress: {_basePoint.position.z:F1} m";
}
