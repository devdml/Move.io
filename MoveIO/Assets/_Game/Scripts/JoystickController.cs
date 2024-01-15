using UnityEngine;

public class JoystickController : MonoBehaviour
{
    public RectTransform joystickBG;
    public RectTransform joystickControl;
    public static Vector3 direct;
    public GameObject joystickPanel;

    private Vector3 startPoint;
    private Vector3 updatePoint;
    private float Magnitube = 200f;

    private Vector3 screen;
    private Vector3 MousePosition => Input.mousePosition - screen / 2;

    private void Awake()
    {
        screen.x = Screen.width;
        screen.y = Screen.height;

        direct = Vector3.zero;
    }

    void Update()
    {
            Joystick();
    }

    private void Joystick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPoint = MousePosition;
            joystickBG.anchoredPosition = startPoint;
            joystickPanel.SetActive(true);
        }

        if (Input.GetMouseButton(0))
        {
            updatePoint = MousePosition;
            joystickControl.anchoredPosition = Vector3.ClampMagnitude((updatePoint - startPoint), Magnitube) + startPoint;

            direct = (updatePoint - startPoint).normalized;
            direct.z = direct.y;
            direct.y = 0;
        }

        if (Input.GetMouseButtonUp(0))
        {
            joystickPanel.SetActive(false);
            direct = Vector3.zero;
        }
    }

    private void OnDisable()
    {
        direct = Vector3.zero;
    }
}
