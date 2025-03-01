using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSFX : MonoBehaviour
{
    public AudioClip impactSound;
    public AudioClip rollSound;
    private float rollLength;
    private bool rolling;
    [SerializeField] Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rollLength = rollSound.length;
        rolling = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        AudioManager.Instance.Play(impactSound);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (rb.velocity.sqrMagnitude > 0.1f && !rolling)
        {
            AudioManager.Instance.Play(rollSound);
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
}
