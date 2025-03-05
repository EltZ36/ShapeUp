using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public float radius = 5.0F;
    public float power = 10.0F;
    public AudioClip explodeSound;

    void Start()
    {
        Vector2 explosionPos = transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPos, radius);
        foreach (Collider2D hit in colliders)
        {
            Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();
            Vector2 colliderPos = hit.transform.position;
            Vector2 forceDirection = colliderPos - explosionPos;

            if (rb != null)
                rb.AddForce(forceDirection * power, ForceMode2D.Impulse);
        }
        AudioManager.Instance.Play(false, explodeSound, 0);
    }
}
