using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetShapeState : MonoBehaviour
{
    [SerializeField]
    GameObject[] shapes;

    public void ActivateShape(EventInfo eventInfo)
    {
        foreach (GameObject gameObject in shapes)
        {
            gameObject.SetActive(true);
        }
    }

    public void DisableShape(EventInfo eventInfo)
    {
        foreach (GameObject gameObject in shapes)
        {
            gameObject.SetActive(false);
        }
    }
}
