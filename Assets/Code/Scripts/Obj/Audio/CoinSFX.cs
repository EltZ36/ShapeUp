using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSFX : MonoBehaviour
{
    public AudioClip impactSound;
    public AudioClip rollSound;
    private float rollLength,
        impactLength;
    private bool rolling,
        impacting;

    [SerializeField]
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rollLength = rollSound.length;
        rolling = false;

        impactLength = impactSound.length;
        impacting = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude > 5f && !impacting)
        {
            AudioManager.Instance.Play(false, impactSound, 0);
            impacting = true;
            StartCoroutine(DelayImpactReplay());
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (rb.velocity.sqrMagnitude > 0.5f && !rolling)
        {
            AudioManager.Instance.Play(false, rollSound, 0);
            rolling = true;
            StartCoroutine(DelayRollReplay());
        }
    }

    IEnumerator DelayRollReplay()
    {
        float elapsed = 0.0f;
        while (elapsed / rollLength < 1)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }
        rolling = false;
        yield return null;
    }

    IEnumerator DelayImpactReplay()
    {
        float elapsed = 0.0f;
        while (elapsed / impactLength < 1)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }
        impacting = false;
        yield return null;
    }
}
