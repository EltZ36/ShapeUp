using System.Collections;
using UnityEngine;

public class ShakeShape : MonoBehaviour
{
    private bool isShaking = false;

    [SerializeField]
    private int health = 3;

    public void TakeDamage(EventInfo eventInfo)
    {
        health -= 1;
        if (health <= 0)
        {
            eventInfo.TargetObject.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
        else
        {
            Shake(eventInfo);
        }
    }

    public void Shake(EventInfo eventInfo)
    {
        if (isShaking == false)
        {
            isShaking = true;
            StartCoroutine(ShakeCoroutine(eventInfo.TargetObject));
        }
        else
        {
            return;
        }
    }

    IEnumerator ShakeCoroutine(GameObject target)
    {
        var time = 0f;
        var elapsed = 1f;

        var originalPosition = target.transform.position;
        while (time < elapsed)
        {
            time += Time.deltaTime;

            target.transform.position = new Vector3(
                EaseInOutElastic(originalPosition.x, originalPosition.x + 0.5f, time),
                target.transform.position.y,
                target.transform.position.z
            );
            target.transform.position = new Vector3(
                EaseInOutElastic(target.transform.position.x, originalPosition.x, time),
                target.transform.position.y,
                target.transform.position.z
            );

            isShaking = false;
            yield return null;
        }

        if (isShaking == false)
        {
            target.transform.position = originalPosition;
        }
    }

    //from https://gist.github.com/cjddmut/d789b9eb78216998e95c
    public static float EaseInOutElastic(float start, float end, float value)
    {
        end -= start;

        float d = 1f;
        float p = d * .3f;
        float s;
        float a = 0;

        if (value == 0)
            return start;

        if ((value /= d * 0.5f) == 2)
            return start + end;

        if (a == 0f || a < Mathf.Abs(end))
        {
            a = end;
            s = p / 4;
        }
        else
        {
            s = p / (2 * Mathf.PI) * Mathf.Asin(end / a);
        }

        if (value < 1)
            return -0.5f
                    * (
                        a
                        * Mathf.Pow(2, 10 * (value -= 1))
                        * Mathf.Sin((value * d - s) * (2 * Mathf.PI) / p)
                    )
                + start;
        return a
                * Mathf.Pow(2, -10 * (value -= 1))
                * Mathf.Sin((value * d - s) * (2 * Mathf.PI) / p)
                * 0.5f
            + end
            + start;
    }

    public static float EaseOutSine(float start, float end, float value)
    {
        end -= start;
        return end * Mathf.Sin(value * (Mathf.PI * 0.5f)) + start;
    }
}
