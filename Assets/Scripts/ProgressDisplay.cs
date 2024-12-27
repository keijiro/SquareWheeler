using UnityEngine;
using UnityEngine.UIElements;

public sealed class ProgressDisplay : MonoBehaviour
{
    Label _label;

    void Start()
      => _label = FindFirstObjectByType<UIDocument>().rootVisualElement.Q<Label>("progress-label");

    void Update()
      => _label.text = $"Current Progress: {transform.position.z:F1} m";
}
