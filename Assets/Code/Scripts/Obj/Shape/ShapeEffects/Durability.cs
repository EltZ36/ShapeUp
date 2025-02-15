using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Durability : MonoBehaviour
{
    [SerializeField]
    Sprite[] sprites;
    int i = 0;

    public void TakeDamage(EventInfo eventInfo)
    {
        if (i < sprites.Count())
        {
            eventInfo.TargetObject.GetComponent<SpriteRenderer>().sprite = sprites[i];
            i++;
        }
        else
        {
            Destroy(eventInfo.TargetObject);
        }
    }
}
