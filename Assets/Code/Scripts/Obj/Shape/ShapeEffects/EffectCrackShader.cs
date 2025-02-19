using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectCrackShader : MonoBehaviour
{
    int i = 0;

    [SerializeField]
    Material crackMaterial;

    [SerializeField]
    int max = 3;

    [SerializeField]
    float regen = -1;

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

    public void AddCrack(EventInfo eventInfo)
    {
        if (i < max)
        {
            i++;
        }
        if (i > 0)
        {
            SetThreshold(percent);
        }
        if (regen > 0)
        {
            StartCoroutine(Regen(regen));
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
}
