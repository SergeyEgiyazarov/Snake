using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eat : MonoBehaviour
{
    public Color color;

    private MeshRenderer render;

    private void Awake()
    {
        render = GetComponent<MeshRenderer>();
    }

    void Start()
    {
        render.material.color = color;
    }

}
