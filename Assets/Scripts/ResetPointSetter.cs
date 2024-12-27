using UnityEngine;

public sealed class ResetPointSetter : MonoBehaviour
{
    void Start()
      => FindFirstObjectByType<ResetSwitch>().ResetPoint = transform.position;
}
