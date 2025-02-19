using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateWorleyNoiseTexture : MonoBehaviour
{
    [SerializeField]
    private Material material;

    [SerializeField]
    private int resolution = 512;

    [SerializeField]
    private int points = 5;

    private void Start()
    {
        if (material.GetTexture("_WorleyNoise") == null)
        {
            Texture2D noiseTexture = GenerateTexture(resolution, resolution, points, 0);
            material.SetTexture("_WorleyNoise", noiseTexture);
        }
    }

    //Generate Texture from ChatGPT : https://chatgpt.com/share/67b62e1d-7c84-8010-aada-69677a6b9483
    private Texture2D GenerateTexture(int width, int height, int numPoints, int seed)
    {
        Texture2D texture = new Texture2D(resolution, resolution, TextureFormat.RFloat, false);
        Random.InitState(seed);

        Vector2[] points = new Vector2[numPoints];
        int gridSize = Mathf.FloorToInt(Mathf.Sqrt(numPoints));
        float cellSize = (float)resolution / gridSize;

        // Generate evenly distributed points with some jitter
        for (int y = 0; y < gridSize; y++)
        {
            for (int x = 0; x < gridSize; x++)
            {
                float jitterX = Random.Range(-cellSize * 0.5f, cellSize * 0.5f);
                float jitterY = Random.Range(-cellSize * 0.5f, cellSize * 0.5f);
                points[y * gridSize + x] = new Vector2(
                    (x + 0.5f) * cellSize + jitterX,
                    (y + 0.5f) * cellSize + jitterY
                );
            }
        }

        // Fill texture with Worley noise
        float minDistGlobal = float.MaxValue;
        float maxDistGlobal = float.MinValue;
        float[,] distances = new float[resolution, resolution];

        for (int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
                Vector2 uv = new Vector2(x, y);
                float minDist = float.MaxValue;

                foreach (var point in points)
                {
                    // Tiling calculation
                    for (int ox = -1; ox <= 1; ox++)
                    {
                        for (int oy = -1; oy <= 1; oy++)
                        {
                            Vector2 wrappedPoint = point + new Vector2(ox, oy) * resolution;
                            float dist = Vector2.Distance(uv, wrappedPoint);
                            if (dist < minDist)
                                minDist = dist;
                        }
                    }
                }
                distances[x, y] = minDist;
                if (minDist < minDistGlobal)
                    minDistGlobal = minDist;
                if (minDist > maxDistGlobal)
                    maxDistGlobal = minDist;
            }
        }

        // Normalize distances and set pixels
        for (int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
                float normalizedValue =
                    (distances[x, y] - minDistGlobal) / (maxDistGlobal - minDistGlobal);
                texture.SetPixel(
                    x,
                    y,
                    new Color(normalizedValue, normalizedValue, normalizedValue)
                );
            }
        }

        texture.Apply();
        return texture;
    }
}
