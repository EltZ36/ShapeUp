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
    bool oneActive = true;

    [SerializeField]
    bool pickRandom = false;

    [SerializeField]
    bool randomRotation = false;

    [SerializeField]
    bool randomPosition = false;

    int order = 0;

    [SerializeField]
    float lifespan = 1;

    GameObject oneOverlay;
    SpriteRenderer osr;

    void Awake()
    {
        order = GetComponent<SpriteRenderer>().sortingOrder;
        if (oneActive)
        {
            oneOverlay = new GameObject("overlay");
            oneOverlay.transform.parent = transform;
            osr = oneOverlay.AddComponent<SpriteRenderer>();
            osr.sortingOrder = order + 1;
        }
    }

    GameObject GetSprite()
    {
        GameObject overlay;
        if (!oneActive)
        {
            overlay = new GameObject("overlay " + i);
            overlay.transform.parent = transform;
        }
        else
        {
            overlay = oneOverlay;
        }

        overlay.transform.localRotation = randomRotation ? Random.rotation : Quaternion.identity;
        overlay.transform.localPosition = randomPosition ? Random.onUnitSphere / 2 : Vector3.zero;
        overlay.transform.localScale = Vector3.one;

        Sprite sprite = sprites[i % sprites.Count()];
        i++;

        if (pickRandom)
        {
            sprite = sprites[Random.Range(0, sprites.Count())];
        }
        if (!oneActive)
        {
            SpriteRenderer sr = overlay.AddComponent<SpriteRenderer>();
            sr.sortingOrder = order + i;
            sr.sprite = sprite;
        }
        else
        {
            osr.sprite = sprite;
        }

        return overlay;
    }

    public void AddOverlay(EventInfo eventInfo)
    {
        GetSprite();
        if (!oneActive && transform.childCount > sprites.Count())
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
            if (!oneActive)
            {
                Destroy(overlay);
            }
            else
            {
                int d = System.Array.IndexOf(sprites, osr.sprite) - 1;
                osr.sprite = d >= 0 ? sprites[d] : null;
            }
        }
        i--;
    }
}
