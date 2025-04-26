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
        Slice,
        Pinch,
        Accelerate,
        Tilt,
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
            case GlobalEvents.Slice:
                Shape.GlobalSlice += SetGray;
                break;
            case GlobalEvents.Pinch:
                Shape.GlobalPinch += SetGray;
                break;
            case GlobalEvents.Accelerate:
                Shape.GlobalAccel += SetGray;
                break;
            case GlobalEvents.Tilt:
                Shape.GlobalTilt += SetGray;
                break;
        }
    }

    public void SetGray()
    {
        gameObject.GetComponent<Image>().color = Color.gray;
    }

    public void SetWhite()
    {
        gameObject.GetComponent<Image>().color = Color.white;
    }

    public void OnDisable()
    {
        Shape.GlobalTap -= SetGray;
        Shape.GlobalDrag -= SetGray;
        Shape.GlobalSlice -= SetGray;
        Shape.GlobalPinch -= SetGray;
        Shape.GlobalAccel -= SetGray;
        Shape.GlobalTilt -= SetGray;
    }
}
