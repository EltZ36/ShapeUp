using UnityEngine;

[ExecuteAlways] // So it works in edit mode too
public class ScaleToFitScreen : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        ScaleSprite();
    }

    void Update()
    {
        ScaleSprite();
    }

    void ScaleSprite()
    {
        if (spriteRenderer == null)
            return;

        float screenHeight = Camera.main.orthographicSize * 2f;
        float screenWidth = screenHeight * Camera.main.aspect;

        Sprite sprite = spriteRenderer.sprite;
        if (sprite == null)
            return;

        float spriteWidth = sprite.bounds.size.x;
        float spriteHeight = sprite.bounds.size.y;

        Vector3 scale = transform.localScale;
        scale.x = (screenWidth / spriteWidth) * 1.1f;
        scale.y = (screenHeight / spriteHeight) * 1.1f;

        transform.localScale = scale;
    }
}
