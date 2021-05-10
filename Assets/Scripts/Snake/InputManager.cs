using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;
    private TouchControls touchControls;

    private bool isHoold = false;

    public static InputManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        Singelton();
        touchControls = new TouchControls();
    }

    private void Singelton()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void OnEnable()
    {
        touchControls.Enable();
    }

    private void OnDisable()
    {
        touchControls.Disable();
    }

    private void Start()
    {
        touchControls.Touch.TouchPress.started += ctx => StartTouch(ctx);
        touchControls.Touch.TouchPress.canceled += ctx => EndTouch(ctx);
    }

    private void StartTouch(InputAction.CallbackContext context)
    {
        Debug.Log("Touch started " + touchControls.Touch.TouchPosition.ReadValue<Vector2>());
        isHoold = true;
    }

    private void EndTouch(InputAction.CallbackContext context)
    {
        Debug.Log("Touch ended ");
        isHoold = false;
    }

    public bool GetTouchPress()
    {
        return isHoold;
    }

    public Vector2 GetTouchPosition()
    {
        return touchControls.Touch.TouchPosition.ReadValue<Vector2>();
    }

    #region Mouse Use
    /*public Vector3 GetMousePosition()
    {
        return Mouse.current.position.ReadValue();
    }

    public bool GetMouseAction()
    {
        return Mouse.current.leftButton.IsPressed();
    }*/
    #endregion
}
