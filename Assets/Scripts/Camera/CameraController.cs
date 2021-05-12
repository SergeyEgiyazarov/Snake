using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform snake;

    private Vector3 offset;

    private void Start()
    {
        offset = transform.position - snake.position;
    }

    private void LateUpdate()
    {
        Vector3 newCamPosition = new Vector3(0.0f, 0.0f, snake.position.z);
        transform.position = newCamPosition + offset;
    }
}
