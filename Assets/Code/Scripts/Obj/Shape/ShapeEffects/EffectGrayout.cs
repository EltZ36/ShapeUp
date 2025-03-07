using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectGrayout : MonoBehaviour
{
    enum GlobalEvents
    {
        Tap,
        Drag,
        Swipe,
        Pinch,
    }

    [SerializeField]
    GlobalEvents globalEvent;

    void Start()
    {
        switch (globalEvent)
        {
            case GlobalEvents.Tap:
                Shape.GlobalTap += SetGray;
                break;
            case GlobalEvents.Drag:
                Shape.GlobalDrag += SetGray;
                break;
            case GlobalEvents.Swipe:
                Debug.Log("correct");
                Shape.GlobalSlice += SetGray;
                break;
            case GlobalEvents.Pinch:
                Shape.GlobalPinch += SetGray;
                break;
        }
    }

    public void SetGray()
    {
        Debug.Log("setGray");
        gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
    }

    public void SetWhite()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
