using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    public float speed;
    private float zoom;

    private Bounds _cameraBounds;
    private Vector3 _targetPosition, deltaPosition;

    private void Start()
    {
        zoom = 1f;
        resetBounds();
    }

    void Update()
    {
        if (Input.touches.Length == 0) { }
        else if (Input.touches.Length == 1)
        {
            handleOneTouch();
        }
        else if (Input.touches.Length == 2)
        {
            handleOneTouch();
            handleTwoTouches();
        }
    }

    void handleOneTouch()
    {
        Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
        deltaPosition = new Vector3(
            -touchDeltaPosition.x * speed * Time.deltaTime,
            -touchDeltaPosition.y * speed * Time.deltaTime,
            0f
        );
        _targetPosition = transform.position + deltaPosition;
        _targetPosition = getCameraBounds();
        transform.position = _targetPosition;
    }

    private Vector3 getCameraBounds()
    {
        return new Vector3(
            Mathf.Clamp(_targetPosition.x, _cameraBounds.min.x, _cameraBounds.max.x),
            Mathf.Clamp(_targetPosition.y, _cameraBounds.min.y, _cameraBounds.max.y),
            transform.position.z
        );
    }

    private void resetBounds()
    {
        var height = Camera.main.orthographicSize;
        var width = height * Camera.main.aspect;

        var minX = Globals.WorldBounds.min.x + width;
        var maxX = Globals.WorldBounds.extents.x - width;

        var minY = Globals.WorldBounds.min.y + height;
        var maxY = Globals.WorldBounds.extents.y - height;

        _cameraBounds = new Bounds();
        _cameraBounds.SetMinMax(new Vector3(minX, minY, 0f), new Vector3(maxX, maxY, 0f));
    }

    void handleTwoTouches()
    {
        if (
            Input.touches[0].phase == TouchPhase.Began
            || Input.touches[1].phase == TouchPhase.Began
        )
        {
            zoom = Vector2.Distance(Input.touches[0].position, Input.touches[1].position);
        }
        else if (
            Input.touches[0].phase == TouchPhase.Moved
            || Input.touches[1].phase == TouchPhase.Moved
        )
        {
            float scaleChange = Mathf.Clamp(
                Camera.main.orthographicSize
                    - (
                        Vector2.Distance(Input.touches[0].position, Input.touches[1].position)
                        - zoom
                    ) / 10f,
                5f,
                25f
            );
            Camera.main.orthographicSize = scaleChange;
            zoom = Vector2.Distance(Input.touches[0].position, Input.touches[1].position);
            resetBounds();
        }
    }

    public static IEnumerator ZoomOut(float EndPos = 10, float time = 1f)
    {
        float StartPos = Camera.main.orthographicSize;
        float elapsed = 0.0f;
        while (elapsed / time < 1)
        {
            elapsed += Time.deltaTime;
            Camera.main.orthographicSize = EaseOutQuad(StartPos, EndPos, elapsed / time);
            yield return null;
        }
        Camera.main.orthographicSize = EndPos;
        GameManager.Instance.SaveGame();
        SceneManager.UnloadSceneAsync("LevelUI");
        LevelManager.Instance.UnloadCurrentSubLevel();
    }

    //Created by C.J. Kimberlin https://gist.github.com/cjddmut/d789b9eb78216998e95c
    private static float EaseOutQuad(float start, float end, float value)
    {
        end -= start;
        return -end * value * (value - 2) + start;
    }
}
