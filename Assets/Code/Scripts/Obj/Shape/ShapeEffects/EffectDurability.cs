using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EffectDurability : MonoBehaviour
{
    [SerializeField]
    Sprite[] sprites;

    [SerializeField]
    GameObject target;
    int i = 0;

    public void TakeDamage(EventInfo eventInfo)
    {
        if (i < sprites.Count())
        {
            target.GetComponent<SpriteRenderer>().sprite = sprites[i];
            i++;
        }
        else
        {
            Destroy(target);
        }
    }

    public void HealDamage(EventInfo eventInfo)
    {
        if (target == null)
        {
            return;
        }

        if (i > 0)
        {
            i--;
            target.GetComponent<SpriteRenderer>().sprite = sprites[i];
        }
    }
}
