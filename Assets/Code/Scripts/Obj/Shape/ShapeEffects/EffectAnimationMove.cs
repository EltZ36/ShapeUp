using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAnimationMove : MonoBehaviour
{
    [SerializeField]
    private GameObject OriginalObject,
        ObjectToMoveTo;

    [SerializeField]
    private ParticleSystem particles;
    private ParticleSystem particlesInstance;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == ObjectToMoveTo)
        {
            particlesInstance = Instantiate(
                particles,
                OriginalObject.transform.position,
                Quaternion.identity
            );
            OriginalObject.SetActive(false);
            Debug.Log("MoveToObject");
            //MoveToObject(null);
        }
    }

    public void MoveToObject(EventInfo eventInfo)
    {
        // OriginalObject.transform.position = ObjectToMoveTo.transform.position;
        //if (ObjectToMoveTo.GetComponent<Collider>() != null)
        //{
        StartCoroutine(EasingObject(eventInfo.TargetObject, ObjectToMoveTo));
        /*  particlesInstance = Instantiate(
              particles,
              eventInfo.TargetObject.transform.position,
              Quaternion.identity
          ); */
    }

    IEnumerator EasingObject(GameObject OriginalObject, GameObject ObjectToMoveTo)
    {
        float time = 0;
        while (time < 3.0f)
        {
            OriginalObject.transform.position = new Vector3(
                EaseInOutQuart(
                    OriginalObject.transform.position.x,
                    ObjectToMoveTo.transform.position.x,
                    time
                ),
                EaseInOutQuart(
                    OriginalObject.transform.position.y,
                    ObjectToMoveTo.transform.position.y,
                    time
                ),
                EaseInOutQuart(
                    OriginalObject.transform.position.z,
                    ObjectToMoveTo.transform.position.z,
                    time
                )
            );
            time += Time.deltaTime;
            yield return null;
        }
        Debug.Log("Done");
    }

    //easing function obtained from https://gist.github.com/cjddmut/d789b9eb78216998e95c
    public static float EaseInOutQuart(float start, float end, float value)
    {
        value /= .5f;
        end -= start;
        if (value < 1)
            return end * 0.5f * value * value * value * value + start;
        value -= 2;
        return -end * 0.5f * (value * value * value * value - 2) + start;
    }
}
