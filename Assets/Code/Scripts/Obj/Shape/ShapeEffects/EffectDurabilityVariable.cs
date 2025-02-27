using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            StartCoroutine(waitForSwipeEnd());
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

    IEnumerator waitForSwipeEnd()
    {
        float timer = 0.0f;
        while (swipeEnd == false || timer < 5.0F)
        {
            if (Input.touchCount >= 1)
            {
                if (Input.touches[0].phase == TouchPhase.Ended)
                {
                    swipeEnd = true;
                }
                timer += Time.deltaTime;
            }
            yield return null;
        }
        yield return null;
    }
}
