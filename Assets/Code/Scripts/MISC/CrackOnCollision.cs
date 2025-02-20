using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CrackOnCollision : MonoBehaviour
{
    [SerializeField]
    private UnityEvent<EventInfo> OnCollision;
    private Rigidbody2D rb;

    [SerializeField]
    float sens = 0.5f;

    [SerializeField]
    float cooldown = 0.2f;

    bool check = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            if (collision.relativeVelocity.magnitude > sens)
            {
                if (check == true)
                {
                    return;
                }
                OnCollision.Invoke(new EventInfo(targetObject: gameObject));
                StartCoroutine(StartCooldown());
            }
        }
    }

    IEnumerator StartCooldown()
    {
        check = true;
        yield return new WaitForSeconds(cooldown);
        check = false;
    }
}
