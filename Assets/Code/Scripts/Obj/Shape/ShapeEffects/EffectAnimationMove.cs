using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

public class EffectAnimationMove : MonoBehaviour
{
    [SerializeField]
    private GameObject OriginalObject,
        ObjectToMoveTo;

    [SerializeField]
    private ParticleSystem particles;

    [SerializeField]
    private bool isCounterClockwise;
    private ParticleSystem particlesInstance;

    public void MoveToObject(EventInfo eventInfo)
    {
        // OriginalObject.transform.position = ObjectToMoveTo.transform.position;
        //if (ObjectToMoveTo.GetComponent<Collider>() != null)
        //{
        StartCoroutine(MoveObject(eventInfo.TargetObject, ObjectToMoveTo, isCounterClockwise));
    }

    //from gpt: https://chatgpt.com/share/67ce1ee5-0044-800c-9802-0f01071f1320
    IEnumerator MoveObject(
        GameObject OriginalObject,
        GameObject ObjectToMoveTo,
        bool counterClockwise
    )
    {
        float duration = 4.0f; // Reduce duration for faster movement
        float time = 0;

        Vector3 startPos = OriginalObject.transform.position;
        Vector3 endPos = ObjectToMoveTo.transform.position;

        // Calculate a midpoint for a flatter arc
        Vector3 center = (startPos + endPos) / 2 + Vector3.up * 1.0f; // Lower height for a flatter arc

        while (time < duration)
        {
            float t = time / duration;
            t = Mathf.SmoothStep(0, 1, t); // Ease-in-out for a natural feel

            // Calculate the angle for the arc
            float angle = t * 180f; // Less than 180° for a flatter arc
            if (counterClockwise)
                angle = -angle;

            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward); // Rotate around the center
            Vector3 newPos = center + rotation * (startPos - center);

            OriginalObject.transform.position = newPos;

            time += Time.deltaTime;
            yield return null;
        }

        OriginalObject.transform.position = endPos; // Snap to final position
        OriginalObject.SetActive(false);
        particlesInstance = Instantiate(
            particles,
            OriginalObject.transform.position,
            Quaternion.identity
        );
        Debug.Log("Done");
    }
}
