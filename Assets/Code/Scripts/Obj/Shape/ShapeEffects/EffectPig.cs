using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EffectPig : MonoBehaviour
{
    int i = 0;

    [SerializeField]
    Material crackMaterial;

    [SerializeField]
    int max = 3;

    [SerializeField]
    int health;

    [SerializeField]
    int tapDamage,
        swipeDamage,
        shakeDamage;

    [SerializeField]
    GameObject target,
        deadObject;
    public AudioClip tapSound,
        swipeSound,
        shakeSound;

    bool swipeEnd = true;
    bool tapEnd = true;

    [SerializeField]
    float tapBuffer;

    float percent
    {
        get { return i * (1 / (float)max); }
    }
    SpriteRenderer sr;

    [SerializeField]
    Collider2D coinBox;

    Collider2D coinTrigger;

    PiggyBank piggyBank;
    MaterialPropertyBlock block;

    void Start()
    {
        block = new MaterialPropertyBlock();
        sr = GetComponent<SpriteRenderer>();
        sr.material = crackMaterial;

        coinTrigger = GetComponent<Collider2D>();

        piggyBank = GetComponent<PiggyBank>();

        InitializeMaterialProperties();
    }

    public void AddTapCrack(EventInfo eventInfo)
    {
        if (tapEnd)
        {
            tapEnd = false;
            health -= tapDamage;
            AudioManager.Instance.Play(false, tapSound, 0);
            StartCoroutine(TapDelay());
            if (health <= 0)
            {
                Die();
            }
            if (i < max)
            {
                i += tapDamage;
            }
            if (i > 0)
            {
                SetThreshold(percent);
            }
        }
    }

    public void AddSwipeCrack(EventInfo eventInfo)
    {
        if (swipeEnd)
        {
            health -= swipeDamage;
            AudioManager.Instance.Play(false, swipeSound, 0);
            if (health <= 0)
            {
                Die();
            }
            if (i < max)
            {
                i += swipeDamage;
                swipeEnd = false;
            }
            if (i > 0)
            {
                SetThreshold(percent);
            }
        }
    }

    public void AddShakeCrack(EventInfo eventInfo)
    {
        health -= shakeDamage;
        AudioManager.Instance.Play(false, shakeSound, 0);
        if (health <= 0)
        {
            Die();
        }
        if (i < max)
        {
            i += shakeDamage;
        }
        if (i > 0)
        {
            SetThreshold(percent);
        }
    }

    void InitializeMaterialProperties()
    {
        sr.GetPropertyBlock(block);
        block.SetVector(
            "_CrackTex_ST",
            new Vector4(1, 1, Random.Range(0, 1f), Random.Range(0, 1f))
        );
        block.SetVector(
            "_WorleyNoise_ST",
            new Vector4(1, 1, Random.Range(0, 1f), Random.Range(0, 1f))
        );
        block.SetFloat("_Threshold", 0);
        sr.SetPropertyBlock(block);
    }

    void SetThreshold(float t)
    {
        sr.GetPropertyBlock(block);
        block.SetFloat("_Threshold", t);
        sr.SetPropertyBlock(block);
    }

    public void SetSwipeEnd(EventInfo eventInfo)
    {
        swipeEnd = true;
    }

    IEnumerator TapDelay()
    {
        float elapsed = 0.0f;
        while (elapsed / tapBuffer < 1)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }
        tapEnd = true;
        yield return null;
    }

    public void Die()
    {
        sr.enabled = false;
        coinBox.enabled = false;
        coinTrigger.enabled = false;
        deadObject.SetActive(true);
        piggyBank.Invoke();
    }
}
