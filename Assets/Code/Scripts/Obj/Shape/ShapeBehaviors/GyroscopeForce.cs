using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroscopeForce : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 dir = Vector3.zero;

    void Awake()
    {
        SensorManager.Instance.OnGyroChange += PullShape;
        rb = GetComponent<Rigidbody2D>();
    }

    void OnDestroy()
    {
        SensorManager.Instance.OnGyroChange -= PullShape;
    }

    void PullShape(float rotation)
    {
        dir = Quaternion.Euler(0, 0, rotation) * Vector3.right;
        dir = new Vector3(-dir.x, dir.y, dir.z);
    }

    void FixedUpdate()
    {
        //5 is a test value
        rb.AddForce(dir * 9.81f, ForceMode2D.Force);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + dir.normalized);
    }
}
