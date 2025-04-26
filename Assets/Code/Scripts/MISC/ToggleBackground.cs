using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleBackground : MonoBehaviour
{
    [SerializeField]
    Collider2D sun;

    [SerializeField]
    SpriteRenderer paperBackground,
        darkBackground;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider == sun)
        {
            setBright();
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider == sun)
        {
            setDark();
        }
    }

    void setBright()
    {
        paperBackground.sortingOrder = 0;
        darkBackground.sortingOrder = -2;
    }

    void setDark()
    {
        paperBackground.sortingOrder = -2;
        darkBackground.sortingOrder = 0;
    }
}
