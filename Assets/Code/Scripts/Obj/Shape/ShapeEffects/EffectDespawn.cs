using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDespawn : MonoBehaviour
{
    [SerializeField]
    private float despawnTime = 2f;

    [SerializeField]
    private bool randomDespawnTime = false;

    [SerializeField]
    private float minDespawnTime = 0f,
        maxDespawnTime = 2f;

    [Tooltip("Either destroy or deactivate object (deactivate is more efficient)")]
    [SerializeField]
    private bool isDestroying = true;

    void Start()
    {
        float lifeTime = randomDespawnTime
            ? Random.Range(minDespawnTime, maxDespawnTime)
            : despawnTime;

        StartCoroutine(Despawn(lifeTime));
    }

    IEnumerator Despawn(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (isDestroying)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
