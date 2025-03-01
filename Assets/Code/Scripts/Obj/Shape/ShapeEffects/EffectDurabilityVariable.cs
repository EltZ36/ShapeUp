using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EffectDurabilityVariable : MonoBehaviour
{
    [SerializeField]
    int health;

    [SerializeField]
    int tapDamage,
        swipeDamage,
        shakeDamage;

    bool swipeEnd = true;

    [SerializeField]
    float regen = -1;

    [SerializeField]
    GameObject target;

    public AudioClip swipeSound;

    public void TapDamage(EventInfo eventInfo)
    {
        health -= tapDamage;
        if (health > 0)
        {
            if (regen > 0)
            {
                StartCoroutine(regenHealth(regen));
            }
        }
        else
        {
            Destroy(target);
        }
    }

    public void SwipeDamage(EventInfo eventInfo)
    {
        if (swipeEnd)
        {
            health -= swipeDamage;
            AudioManager.Instance.Play(swipeSound);
            if (health > 0)
            {
                if (regen > 0)
                {
                    StartCoroutine(regenHealth(regen));
                }
            }
            else
            {
                Destroy(target);
            }
            swipeEnd = false;
        }
    }

    public void ShakeDamage(EventInfo eventInfo)
    {
        health -= shakeDamage;
        if (health > 0)
        {
            if (regen > 0)
            {
                StartCoroutine(regenHealth(regen));
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

    public void SetSwipeEnd(EventInfo eventInfo)
    {
        swipeEnd = true;
    }
}
