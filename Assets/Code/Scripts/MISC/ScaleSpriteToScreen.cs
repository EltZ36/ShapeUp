using UnityEngine;

[ExecuteAlways]
public class ScaleSpriteToScreen : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    [SerializeField] float xScalePadding = 1f, yScalePadding = 1f;

    [SerializeField] bool scaleWidth = false, scaleHeight = false;

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

        if (scaleWidth) {
            scale.x = (screenWidth / spriteWidth) * xScalePadding;
        }

        if (scaleHeight) {
            scale.y = (screenHeight / spriteHeight) * yScalePadding;
        }

        transform.localScale = scale;
    }
}
