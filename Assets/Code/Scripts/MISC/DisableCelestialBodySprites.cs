using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableCelestialBodySprites : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        col.GetComponent<SpriteRenderer>().enabled = true;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        col.GetComponent<SpriteRenderer>().enabled = false;
    }
}
