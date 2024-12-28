using UnityEngine;
using UnityEngine.UIElements;

public sealed class Instruction : MonoBehaviour
{
    [SerializeField] HingeJoint _joint1 = null;
    [SerializeField] HingeJoint _joint2 = null;

    async void Start()
    {
        var root = FindFirstObjectByType<UIDocument>().rootVisualElement;
        var inst1 = root.Q("instruction1");
        var inst2 = root.Q("instruction2");

        await Awaitable.WaitForSecondsAsync(0.5f);

        inst1.visible = true;

        for (var acc = 0.0f; acc < 0.5f;)
        {
            var y = Mathf.Abs(Mathf.Sin(Time.time * 5)) * 20;
            inst1.transform.position = new Vector3(0, -y, 0);
            acc += (_joint1.motor.force > 0) ? Time.deltaTime : 0;
            await Awaitable.NextFrameAsync();
        }

        inst1.visible = false;

        await Awaitable.WaitForSecondsAsync(0.5f);

        inst2.visible = true;

        for (var acc = 0.0f; acc < 0.5f;)
        {
            var y = Mathf.Abs(Mathf.Sin(Time.time * 5)) * 20;
            inst2.transform.position = new Vector3(0, -y, 0);
            acc += (_joint2.motor.force > 0) ? Time.deltaTime : 0;
            await Awaitable.NextFrameAsync();
        }

        inst2.visible = false;
    }
}
