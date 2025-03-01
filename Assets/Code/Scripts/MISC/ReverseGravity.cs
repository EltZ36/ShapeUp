using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseGravity : MonoBehaviour
{
    void Awake()
    {
        Physics2D.gravity = new UnityEngine.Vector2(0f, 9.8f);
    }
}
