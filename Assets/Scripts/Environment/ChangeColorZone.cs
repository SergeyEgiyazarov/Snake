using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorZone : MonoBehaviour
{
    public Color color;

    private void OnTriggerEnter(Collider other)
    {
        MeshRenderer snakeRender = other.gameObject.GetComponent<MeshRenderer>();
        snakeRender.material.color = color;
    }
}
