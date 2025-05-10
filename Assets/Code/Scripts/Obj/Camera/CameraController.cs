using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    public float speed;
    private float zoom;

    private Bounds _cameraBounds;
    private Vector3 _targetPosition,
        deltaPosition;

    float lastX,
        lastY;

    public float tapTime;

    private Dictionary<int, float> touchDict = new Dictionary<int, float>();

    private void Start()
    {
        zoom = 1f;
        tapTime = 0.2f;
        resetBounds();
    }

    void OnEnable()
    {
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
        resetBounds();
    }

    void handleOneTouch()
    {
        bool isOverUI = false;
        if (Input.touches[0].phase == TouchPhase.Began)
        {
            touchDict.Add(Input.touches[0].fingerId, Time.time);
            lastX = Input.GetTouch(0).position.x;
            lastY = Input.GetTouch(0).position.y;
            isOverUI = UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(
                Input.GetTouch(0).fingerId
            );
        }
        if (Input.touches[0].phase == TouchPhase.Ended)
        {
            touchDict[Input.touches[0].fingerId] = Time.time - touchDict[Input.touches[0].fingerId];
            if (touchDict[Input.touches[0].fingerId] < tapTime)
            {
                Vector3 pointOne = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
                Vector2 pointOne2D = new Vector2(pointOne.x, pointOne.y);
                RaycastHit2D hit = Physics2D.Raycast(pointOne2D, Camera.main.transform.forward);
                if (hit.collider != null && !isOverUI)
                {
                    LevelInfo levelInfo = LevelManager.Instance.Levels[
                        LevelManager.Instance.currentLevelID
                    ];
                    SubLevelInfo sublevel = levelInfo.SubLevels.FirstOrDefault(sublevel =>
                        sublevel.Thumbnail == hit.collider.gameObject
                    );
                    if (sublevel != null && sublevel.IsComplete != true)
                    {
                        Vector2 levelPosition = hit.transform.position;
                        StartCoroutine(ZoomIn(levelPosition));
                    }
                }
            }
            touchDict.Remove(Input.touches[0].fingerId);
        }
        float deltaX = Input.GetTouch(0).position.x - lastX;
        float deltaY = Input.GetTouch(0).position.y - lastY;
        Vector2 touchDeltaPosition = new Vector2(deltaX, deltaY);
#if !UNITY_EDITOR && UNITY_WEBGL
        deltaPosition = new Vector3(
            -touchDeltaPosition.x * speed * Time.deltaTime,
            -touchDeltaPosition.y * speed * Time.deltaTime,
            0f
        );
#else
        deltaPosition = new Vector3(
            -touchDeltaPosition.x * speed * Time.deltaTime,
            -touchDeltaPosition.y * speed * Time.deltaTime,
            0f
        );
#endif
        _targetPosition = transform.position + deltaPosition;
        _targetPosition = getCameraBounds();
        transform.position = _targetPosition;

        lastX = Input.GetTouch(0).position.x;
        lastY = Input.GetTouch(0).position.y;
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
        var maxX = Globals.WorldBounds.extents.x - width - 0.2f;

        var minY = Globals.WorldBounds.min.y + height;
        var maxY = Globals.WorldBounds.extents.y - height - 1;

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

    public static IEnumerator ZoomOut(bool fully, float time = 1f)
    {
        float EndPos = fully ? 20 : 10;
        float aspectRatio = (float)Screen.width / (float)Screen.height;
        if (aspectRatio < 16f / 9f)
        {
            EndPos = (float)EndPos * ((16f / 9f) / aspectRatio);
        }

        float StartSize = Camera.main.orthographicSize;

        Vector3 StartPos = Camera.main.transform.position;

        float elapsed = 0.0f;
        while (elapsed / time < 1)
        {
            elapsed += Time.deltaTime;
            Camera.main.orthographicSize = EaseOutQuad(StartSize, EndPos, elapsed / time);
            if (fully)
            {
                Camera.main.transform.position = new Vector3(
                    EaseOutQuad(StartPos.x, 0, elapsed / time),
                    EaseOutQuad(StartPos.y, 0, elapsed / time),
                    StartPos.z
                );
            }

            yield return null;
        }
        Camera.main.orthographicSize = EndPos;
        GameManager.Instance.SaveGame();
        LevelManager.Instance.UnloadCurrentSubLevel();
    }

    public static IEnumerator ZoomOutAndReset(float time = 1f, int LevelID = 0)
    {
        float EndPos = 20;
        float aspectRatio = (float)Screen.width / (float)Screen.height;
        if (aspectRatio < 16f / 9f)
        {
            EndPos = (float)EndPos * ((16f / 9f) / aspectRatio);
        }

        float StartSize = Camera.main.orthographicSize;

        Vector3 StartPos = Camera.main.transform.position;

        float elapsed = 0.0f;
        while (elapsed / time < 1)
        {
            elapsed += Time.deltaTime;
            Camera.main.orthographicSize = EaseOutQuad(StartSize, EndPos, elapsed / time);
            Camera.main.transform.position = new Vector3(
                EaseOutQuad(StartPos.x, 0, elapsed / time),
                EaseOutQuad(StartPos.y, 0, elapsed / time),
                StartPos.z
            );
            yield return null;
        }
        Camera.main.orthographicSize = EndPos;
        LevelManager.Instance.LoadLevel(LevelID);
        GameManager.Instance.SaveGame();
    }

    public static IEnumerator ZoomIn(Vector2 _levelPosition)
    {
        float StartSize = Camera.main.orthographicSize;
        float aspectRatio = (float)Screen.width / (float)Screen.height;
        float EndSize = 5f;
        if (aspectRatio < 16f / 9f)
        {
            EndSize = 5f * ((16f / 9f) / aspectRatio) * 1f;
        }
        float StartX = Camera.main.transform.position.x;
        float StartY = Camera.main.transform.position.y;
        float time = 1.0f;
        float elapsed = 0.0f;
        while (elapsed / time < 1)
        {
            elapsed += Time.deltaTime;
            Camera.main.orthographicSize = EaseOutQuad(StartSize, EndSize, elapsed / time);
            Camera.main.transform.position = new Vector3(
                EaseOutQuad(StartX, _levelPosition.x, elapsed / time),
                EaseOutQuad(StartY, _levelPosition.y, elapsed / time),
                Camera.main.transform.position.z
            );
            yield return null;
        }
    }

    //Created by C.J. Kimberlin https://gist.github.com/cjddmut/d789b9eb78216998e95c
    private static float EaseOutQuad(float start, float end, float value)
    {
        end -= start;
        return -end * value * (value - 2) + start;
    }
}
