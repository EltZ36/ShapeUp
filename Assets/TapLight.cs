using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TapLight : MonoBehaviour
{
    [SerializeField]
    public float holdTime,
        fadeTime;

    private Light2D tapLight;

    // Start is called before the first frame update
    public void OnEnable()
    {
        tapLight = gameObject.GetComponent<Light2D>();
        tapLight.pointLightOuterRadius = 2;
        StartCoroutine(HoldAndFade(gameObject, tapLight, holdTime, fadeTime));
    }

    public static IEnumerator HoldAndFade(
        GameObject targetObject,
        Light2D _light,
        float _holdTime,
        float _fadeTime
    )
    {
        float elapsed = 0.0f;
        float initialLightRadius = _light.pointLightOuterRadius;
        while (elapsed < _holdTime + _fadeTime)
        {
            Debug.Log(elapsed);
            if (elapsed > _holdTime)
            {
                _light.pointLightOuterRadius =
                    initialLightRadius - (initialLightRadius * (elapsed - _fadeTime) / _holdTime);
            }
            elapsed += Time.deltaTime;
            yield return null;
        }
        targetObject.SetActive(false);
        yield return null;
    }
}
