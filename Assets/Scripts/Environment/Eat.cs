using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eat : MonoBehaviour
{
    public Color color;

    private MeshRenderer render;
    private Animator anim;

    private void Awake()
    {
        render = GetComponent<MeshRenderer>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        render.material.color = color;
    }

    private void StartAnimation()
    {
        anim.SetTrigger("Eating");
    }

    private void OnTriggerEnter(Collider other)
    {
        StartAnimation();
    }
}
