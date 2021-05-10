using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailController : MonoBehaviour
{
    public Vector3 target;
    public float offset;

    private void FixedUpdate()
    {
        Vector3 direction = target - transform.position;
        float distance = direction.magnitude;

        if (distance > offset)
        {
            transform.position += direction.normalized * (distance - offset);
            transform.LookAt(target);
        }
    }
}
