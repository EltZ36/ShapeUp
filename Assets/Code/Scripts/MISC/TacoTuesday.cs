using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lights;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering.Universal;

/// <summary>
/// contains 'locks' for a given 'light'
/// locks determine whether or not the light is interactable.
/// for instance you dont want to spam click the light and just max it out fast
/// because thats bad design (lol)... and the easing function looks goofy
/// </summary>
public class LockContainer
{
    public bool isGrowing;
    public bool isShrinking;

    /**
        needs a ref to the shrinking cr so if you want to stop it from
        disapearing you can stop the coroutine without overwriting it
    **/
    public Coroutine cr;

    public LockContainer(bool grow, bool shrink)
    {
        this.isGrowing = grow;
        this.isShrinking = shrink;
        this.cr = null;
    }
}

/// <summary>
/// Main behavior for the level is 'lights'
/// they grow on tap
/// they will automatically start shrinking after delayBeforeShrinking seconds
/// they will continue to shrink until they disappear, then they will delete
/// you can interrupt the shrinking by tapping on it, sending it back to grow state
/// </summary>
public class TacoTuesday : MonoBehaviour
{
    [SerializeField]
    GameObject lightObj;

    // need a mask
    private List<(CircleCollider2D, GameObject)> activeLights;
    private List<LockContainer> locks;

    // bunch of constants to deal with scaling, initial sizes, and timings.
    private const float maxLightSize = 2.25f;
    private const float scaleFactor = 1.5f;
    private const float growTime = 1f;
    private const float shrinkTime = 5f;
    private const int delayBeforeShrinking = 2;
    private const int maxActiveLights = 5;

    // limit the number of lights that can exist in the scene at a given time.
    private int lightCounter = 0;

    void Start()
    {
        lightCounter = 0;
        activeLights = new List<(CircleCollider2D, GameObject)>();
        locks = new List<LockContainer>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Vector2 touchPosition = touch.position;
                HandleTouch(touchPosition);
            }
        }
    }

    private void HandleTouch(Vector2 position)
    {
        Vector2 tapPos = Camera.main.ScreenToWorldPoint(position);
        RaycastHit2D hit = Physics2D.Raycast(tapPos, Camera.main.transform.forward);

        if (hit.collider != null)
        {
            IncreaseLightSize(hit.collider.gameObject);
        }
        else
        {
            CreateLight(tapPos);
        }
    }

    /// <summary>
    /// Constuct a new light and begin the behavior on it
    /// </summary>
    /// <param name="pos"></param>
    private void CreateLight(Vector2 pos)
    {
        if (lightCounter >= maxActiveLights)
        {
            return;
        }

        GameObject light = Instantiate(lightObj, pos, quaternion.identity);

        CircleCollider2D collider = light.AddComponent<CircleCollider2D>();
        collider.isTrigger = true;
        collider.radius = 0.5f;
        collider.transform.position = new Vector3(pos.x, pos.y, 0);

        light.AddComponent<LightsEnable>();

        activeLights.Add((collider, light));
        locks.Add(new LockContainer(false, false));
        lightCounter++;

        IncreaseLightSize(light);
    }

    /// <summary>
    /// Increase the size of a light
    /// </summary>
    /// <param name="ob"></param>
    private void IncreaseLightSize(GameObject ob)
    {
        CircleCollider2D circleCollider2D = ob.GetComponent<CircleCollider2D>();

        if (circleCollider2D == null)
        {
            CreateLight(ob.transform.position);
            return;
        }
        // if the user tapped on an already existing light, get its scale
        float ls = circleCollider2D.radius;
        if (ls >= maxLightSize)
        {
            return;
        }

        // snag the index we are concerned with
        int index = activeLights.FindIndex((e) => e.Item1 == ob.GetComponent<Collider2D>());

        // not found    ||  the light is already growing
        if (index == -1 || locks[index].isGrowing == true)
        {
            return;
        }

        // the light is currently shrinking
        if (locks[index].isShrinking == true)
        {
            // stop the shrink!
            StopCoroutine(locks[index].cr);
            locks[index].isShrinking = false;
        }

        // time to grow!
        locks[index].isGrowing = true;
        StartCoroutine(Scale(true, activeLights[index].Item2, index));
    }

    /// <summary>
    /// Scale function scales a light either up or down
    /// </summary>
    /// <param name="grow">grow the light (true) | shrink the light (false)</param>
    /// <param name="sm">Sprite mask to grow</param>
    /// <param name="done">Callback to invoke when the scale is finished</param>
    /// <returns></returns>
    private IEnumerator Scale(bool grow, GameObject ob, int index)
    {
        int audioIndex;
        if (grow)
        {
            audioIndex = 2;
        }
        else
        {
            audioIndex = 3;
        }
        AudioManager.Instance.Play(true, null, audioIndex);

        float StartSizeSM = ob.GetComponent<Light2D>().pointLightOuterRadius;
        float EndSizeSM = grow ? StartSizeSM * scaleFactor : StartSizeSM * 0f;

        float time = grow ? growTime : shrinkTime;
        float elapsed = 0.0f;
        while (elapsed / time < 1)
        {
            elapsed += Time.deltaTime;
            ob.GetComponent<Light2D>().pointLightOuterRadius = EaseInBounce(
                StartSizeSM,
                EndSizeSM,
                elapsed / time
            );
            ob.GetComponent<CircleCollider2D>().radius =
                ob.GetComponent<Light2D>().pointLightOuterRadius;
            yield return null;
        }
        if (grow)
        {
            locks[index].isGrowing = false; // done!
            ob.GetComponent<CircleCollider2D>().radius =
                ob.GetComponent<Light2D>().pointLightOuterRadius;
            yield return new WaitForSeconds(delayBeforeShrinking);

            if (locks[index].isGrowing == true || locks[index].isShrinking == true) // if we got tapped on (non deterministic)
            {
                yield break;
            }
            locks[index].isShrinking = true; // shrink time
            Coroutine cr = StartCoroutine(Scale(false, activeLights[index].Item2, index));
            locks[index].cr = cr; // store the coroutine ref now
        }
        else
        {
            AudioManager.Instance.Play(true, null, 1);
            Destroy(activeLights[index].Item1.gameObject);
            activeLights[index] = (null, null); // these prob should get cleaned up, tricky to schedule a time when the user 'wont' tap to claim mutex on the list.
            locks[index] = null;
            lightCounter--;
        }
    }

    //Created by C.J. Kimberlin https://gist.github.com/cjddmut/d789b9eb78216998e95c
    private float EaseInBounce(float start, float end, float value)
    {
        end -= start;
        float d = 1f;
        return end - EaseOutBounce(0, end, d - value) + start;
    }

    //Created by C.J. Kimberlin https://gist.github.com/cjddmut/d789b9eb78216998e95c
    private float EaseOutBounce(float start, float end, float value)
    {
        value /= 1f;
        end -= start;
        if (value < (1 / 2.75f))
        {
            return end * (7.5625f * value * value) + start;
        }
        else if (value < (2 / 2.75f))
        {
            value -= (1.5f / 2.75f);
            return end * (7.5625f * (value) * value + .75f) + start;
        }
        else if (value < (2.5 / 2.75))
        {
            value -= (2.25f / 2.75f);
            return end * (7.5625f * (value) * value + .9375f) + start;
        }
        else
        {
            value -= (2.625f / 2.75f);
            return end * (7.5625f * (value) * value + .984375f) + start;
        }
    }
}
