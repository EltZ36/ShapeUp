using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectMakeChild : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject parent;

    public void MakeChild(EventInfo eventInfo)
    {
        eventInfo.TargetObject.transform.SetParent(parent.transform);
    }
}
