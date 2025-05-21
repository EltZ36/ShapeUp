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
    private float spawnInterval = 1f,
        minForce = 1f,
        maxForce = 1f;

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

            Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y, -2f);

            GameObject prefab = items[Random.Range(0, items.Count)];

            GameObject obj = Instantiate(prefab, spawnPos, Quaternion.identity);
            obj.layer = LayerMask.NameToLayer(spawnLayerName);

            Rigidbody2D rb2D = obj.GetComponent<Rigidbody2D>();
            float force = Random.Range(minForce, maxForce);

            rb2D.AddForce(Vector2.down * force, ForceMode2D.Impulse);

            spawned++;

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
