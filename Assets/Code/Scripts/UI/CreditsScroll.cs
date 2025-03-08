using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsScroll : MonoBehaviour
{
    private RectTransform rectTransform;

    [SerializeField]
    float speed = 100f;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        rectTransform.position += Vector3.up * Time.deltaTime * speed;
        if (rectTransform.anchoredPosition.y > 0)
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
