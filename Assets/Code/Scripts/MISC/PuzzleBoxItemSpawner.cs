using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleBoxItemSpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> items;

    [SerializeField]
    private int spawnCount = 5;

    [SerializeField]
    private float spawnInterval = 1f;

    private string spawnLayerName = "OutsideBox";

    void Start()
    {
        StartCoroutine(SpawnItems());
    }

    IEnumerator SpawnItems()
    {
        int spawned = 0;

        while (spawned < spawnCount)
        {
            if (items.Count == 0)
            {
                Debug.Log("Item spawner list is empty");
                yield break;
            }

            GameObject prefab = items[Random.Range(0, items.Count)];

            GameObject obj = Instantiate(prefab, transform.position, Quaternion.identity);

            obj.layer = LayerMask.NameToLayer(spawnLayerName);

            spawned++;

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
