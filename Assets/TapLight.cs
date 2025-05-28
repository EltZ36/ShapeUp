using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TapLight : MonoBehaviour
{
    [SerializeField]
    public float holdTime,
        fadeTime;

    private Light2D tapLight;
    public GameObject fireFlyPrefab;
    private GameObject fireFly;

    // Start is called before the first frame update
    public void OnEnable()
    {
        tapLight = gameObject.GetComponent<Light2D>();
        tapLight.pointLightOuterRadius = 2;
        fireFly = Instantiate(fireFlyPrefab, transform.position, Quaternion.identity);
        fireFly.transform.SetParent(transform);
        fireFly.SetActive(false);
        StartCoroutine(HoldAndFade(gameObject, fireFly, tapLight, holdTime, fadeTime));
    }

    public static IEnumerator HoldAndFade(
        GameObject targetObject,
        GameObject fireFlyPrefab,
        Light2D _light,
        float _holdTime,
        float _fadeTime
    )
    {
        float elapsed = 0.0f;
        float initialLightRadius = _light.pointLightOuterRadius;
        fireFlyPrefab.SetActive(true);
        while (elapsed < _holdTime + _fadeTime)
        {
            if (elapsed > _holdTime)
            {
                _light.pointLightOuterRadius = EaseInOutQuart(
                    initialLightRadius,
                    0,
                    elapsed - _holdTime
                );
            }
            elapsed += Time.deltaTime;
            yield return null;
        }
        targetObject.SetActive(false);
        yield return null;
    }

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
