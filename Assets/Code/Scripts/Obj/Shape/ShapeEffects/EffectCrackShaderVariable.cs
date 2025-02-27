using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EffectCrackShaderVariable : MonoBehaviour
{
    int i = 0;

    [SerializeField]
    Material crackMaterial;

    [SerializeField]
    int max = 3;

    [SerializeField]
    int tapDamage,
        swipeDamage,
        shakeDamage;

    [SerializeField]
    float regen = -1;

    bool swipeEnd = true;

    float percent
    {
        get { return i * (1 / (float)max); }
    }
    SpriteRenderer sr;
    MaterialPropertyBlock block;

    void Start()
    {
        block = new MaterialPropertyBlock();
        sr = GetComponent<SpriteRenderer>();
        sr.material = crackMaterial;

        InitializeMaterialProperties();
    }

    public void AddTapCrack(EventInfo eventInfo)
    {
        if (i < max)
        {
            i += tapDamage;
        }
        if (i > 0)
        {
            SetThreshold(percent);
            if (regen > 0)
            {
                StartCoroutine(Regen(regen));
            }
        }
    }

    public void AddSwipeCrack(EventInfo eventInfo)
    {
        if (swipeEnd)
        {
            if (i < max)
            {
                i += swipeDamage;
                swipeEnd = false;
                StartCoroutine(waitForSwipeEnd());
            }
            if (i > 0)
            {
                SetThreshold(percent);
                if (regen > 0)
                {
                    StartCoroutine(Regen(regen));
                }
            }
        }
    }

    public void AddShakeCrack(EventInfo eventInfo)
    {
        if (i < max)
        {
            i += shakeDamage;
        }
        if (i > 0)
        {
            SetThreshold(percent);
            if (regen > 0)
            {
                StartCoroutine(Regen(regen));
            }
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

    IEnumerator Regen(float time)
    {
        yield return new WaitForSeconds(time);
        i--;
        SetThreshold(percent);
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
