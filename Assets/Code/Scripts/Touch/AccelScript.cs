using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelScriptNew : MonoBehaviour
{
    // Start is called before the first frame update
    //from https://docs.unity3d.com/6000.0/Documentation/ScriptReference/Input-acceleration.html
    float speed = 10.0f;

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = Vector3.zero;
        dir.x = -Input.acceleration.y;
        dir.z = Input.acceleration.x;

        // clamp acceleration vector to unit sphere
        if (dir.sqrMagnitude > 1)
            dir.Normalize();

        // Make it move 10 meters per second instead of 10 meters per frame...
        dir *= Time.deltaTime;

        // Move object
        transform.Translate(dir * speed);
    }
}
