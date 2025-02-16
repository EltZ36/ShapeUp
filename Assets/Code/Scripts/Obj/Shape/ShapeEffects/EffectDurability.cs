using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EffectDurability : MonoBehaviour
{
    [SerializeField]
    int health;

    [SerializeField]
    bool regen = false;

    [SerializeField]
    float regenDelay = 1;

    [SerializeField]
    GameObject target;

    public void TakeDamage(EventInfo eventInfo)
    {
        if (health > 0)
        {
            health--;
            if (regen)
            {
                StartCoroutine(regenHealth(regenDelay));
            }
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

        if (health > 0)
        {
            health++;
        }
    }

    IEnumerator regenHealth(float delay)
    {
        yield return new WaitForSeconds(delay);
        health++;
    }
}
