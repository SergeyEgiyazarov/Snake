using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;

    public static InputManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        Singelton();
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

    public Vector3 GetMousePosition()
    {
        return Mouse.current.position.ReadValue();
    }

    public bool GetMouseAction()
    {
        return Mouse.current.leftButton.IsPressed();
    }
}
