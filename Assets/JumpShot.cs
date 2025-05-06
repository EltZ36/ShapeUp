using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpShot : MonoBehaviour
{
    private bool hoop = false;
    private bool ball = false;

    private Rigidbody2D rb;

    [SerializeField]
    Vector2 forceVector;

    [SerializeField]
    GameObject winBoundary;

    public void setHoop()
    {
        hoop = true;
        shoot();
    }

    public void setBall()
    {
        ball = true;
        shoot();
    }

    public void shoot()
    {
        if (hoop && ball)
        {
            winBoundary.SetActive(true);
            rb = GetComponent<Rigidbody2D>();
            rb.gravityScale = 1;
            rb.constraints = RigidbodyConstraints2D.None;
            rb.AddForce(forceVector, ForceMode2D.Impulse);
            Debug.Log("shoot!");
        }
    }
}
