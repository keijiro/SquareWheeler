using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public sealed class TitleScreen : MonoBehaviour
{
    [SerializeField] UIDocument _ui = null;

    VisualElement _render;
    VisualElement _title;
    VisualElement _inst;

    bool CheckAnyKeyPressed()
      => Keyboard.current.anyKey.wasPressedThisFrame ||
         Pointer.current.press.wasPressedThisFrame;

    void Start()
    {
        var root = _ui.rootVisualElement;
        _render = root.Q("render");
        _title = root.Q("title-image");
        _inst = root.Q("instruction-label");

        Application.targetFrameRate = 60;

        WaitKeyAndStartGame();
    }

    void Update()
    {
        var t = Time.time;
        var h = _title.contentRect.height;
        var y = (0.5f - Mathf.Abs(Mathf.Sin(t * 5.7f))) * 0.1f * h;
        _title.transform.position = new Vector3(0, y, 0);
    }

    async void WaitKeyAndStartGame()
    {
        await Awaitable.WaitForSecondsAsync(0.5f);

        while (!CheckAnyKeyPressed())
        {
            _inst.visible = Time.time * 3 % 1 < 0.5f;
            await Awaitable.NextFrameAsync();
        }

        _inst.visible = false;
        _ui.rootVisualElement.AddToClassList("hide");

        await Awaitable.WaitForSecondsAsync(0.35f);

        SceneManager.LoadScene(1);
    }
}
