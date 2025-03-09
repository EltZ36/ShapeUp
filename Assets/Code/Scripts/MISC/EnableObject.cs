using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class EnableObject : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private List<GameObject> objects = new List<GameObject>();

    [SerializeField]
    private GameObject house;
    public bool isComplete = false;

    public void EnableObjectsFromList(EventInfo eventInfo)
    {
        if (isComplete == true)
        {
            foreach (GameObject obj in objects)
            {
                if (obj != null)
                {
                    obj.SetActive(true);
                }
            }
        }
    }

    public void setComplete(EventInfo eventInfo)
    {
        isComplete = true;
    }

    public void setHouseComplete(EventInfo eventInfo)
    {
        if (house.activeSelf == true)
        {
            isComplete = true;
        }
    }
}
