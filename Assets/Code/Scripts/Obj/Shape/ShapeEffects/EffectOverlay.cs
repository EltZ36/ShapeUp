using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EffectOverlay : MonoBehaviour
{
    int i = 0;

    [SerializeField]
    Sprite[] sprites;

    [SerializeField]
    bool pickRandom = false;

    [SerializeField]
    bool randomRotation = false;

    [SerializeField]
    bool randomPosition = false;

    int order = 0;

    [SerializeField]
    float lifespan = 1;

    void Awake()
    {
        order = GetComponent<SpriteRenderer>().sortingOrder;
    }

    GameObject GetSprite()
    {
        GameObject overlay = new GameObject("overlay " + i);
        overlay.transform.parent = transform;
        overlay.transform.localRotation = randomRotation ? Random.rotation : Quaternion.identity;
        overlay.transform.localPosition = randomPosition ? Random.onUnitSphere / 2 : Vector3.zero;
        overlay.transform.localScale = Vector3.one;

        Sprite sprite = sprites[i % sprites.Count()];
        i++;

        if (pickRandom)
        {
            sprite = sprites[Random.Range(0, sprites.Count())];
        }
        SpriteRenderer sr = overlay.AddComponent<SpriteRenderer>();
        sr.sortingOrder = order + i;
        sr.sprite = sprite;
        return overlay;
    }

    public void AddOverlay(EventInfo eventInfo)
    {
        GetSprite();
        if (transform.childCount > sprites.Count())
        {
            Destroy(transform.GetChild(0).gameObject);
        }
    }

    public void AddOverlayTemp(EventInfo eventInfo)
    {
        GameObject overlay = GetSprite();
        StartCoroutine(DestroyOverlay(overlay, lifespan));
    }

    IEnumerator DestroyOverlay(GameObject overlay, float lifespan)
    {
        yield return new WaitForSeconds(lifespan);
        if (overlay != null)
        {
            Destroy(overlay);
        }
        i--;
    }
}
