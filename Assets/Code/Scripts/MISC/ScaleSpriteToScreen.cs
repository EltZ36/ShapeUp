using UnityEngine;

public class ScaleSpriteToScreen : MonoBehaviour
{
    [SerializeField]
    float xScale = 1f,
        yScale = 1f;

    [SerializeField]
    bool scaleWidth = false,
        scaleHeight = false;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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

        if (scaleWidth)
        {
            scale.x = (screenWidth / spriteWidth) * xScale;
        }

        if (scaleHeight)
        {
            scale.y = (screenHeight / spriteHeight) * yScale;
        }

        transform.localScale = scale;
    }
}
