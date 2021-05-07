using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailController : MonoBehaviour
{
    public Vector3 target;
    public Quaternion targetRot;

    private Vector3 offset;
    private Rigidbody tailRb;

    void Start()
    {
        offset = new Vector3(0, 0, -0.1f);
        tailRb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        tailRb.MovePosition(target + offset);
        tailRb.MoveRotation(targetRot.normalized);
    }
}
