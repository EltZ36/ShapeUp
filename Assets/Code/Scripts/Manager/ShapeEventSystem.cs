using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeEventSystem : MonoBehaviour
{
    #region Singleton Pattern
    public static ShapeEventSystem _instance;
    public static ShapeEventSystem Instance
    {
        get { return _instance; }
    }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        Input.gyro.enabled = true;
    }
    #endregion
    private Camera cam
    {
        get { return Camera.main; }
    }

    [SerializeField]
    float tapTimer; //if it's longer than this it's not a tap;
    #region SensorVariables
    [SerializeField]
    float accelCooldown = 1;

    [SerializeField]
    float accelSens = 2;

    [SerializeField]
    float gyroSens = 1;

    public event Action<Vector3> OnAccelChange;

    public event Action<Quaternion> OnGyroChange;

    private Vector3 pastAccel = Vector3.zero;
    private Vector3 newAccel;
    bool accelRecent = false;

    private Quaternion pastGyro;
    #endregion

    private static int maxTouches = 10;
    private static float touchSize = 0.1f;

    Shape[] selectedShape = new Shape[maxTouches];
    Vector3[] start = new Vector3[maxTouches];

    private Shape pinchShape;
    private float initialPinchDist;

    void Update()
    {
        CheckAcceleration();
        CheckGyroscope();
        if (Input.touchCount < 1)
        {
            return;
        }
        if (pinchShape != null || checkPinching(out pinchShape))
        {
            pinchShape.OnPinch(
                initialPinchDist,
                cam.ScreenToWorldPoint(Input.touches[0].position),
                cam.ScreenToWorldPoint(Input.touches[1].position)
            );
            if (
                Input.touches[0].phase == TouchPhase.Ended
                || Input.touches[1].phase == TouchPhase.Ended
            )
            {
                initialPinchDist = 0;
                pinchShape = null;
            }
        }
        else
        {
            handleSingleGestures();
        }
    }

    void handleSingleGestures()
    {
        for (int i = 0; i < Mathf.Min(Input.touchCount, maxTouches); i++)
        {
            Vector2 pos = cam.ScreenToWorldPoint(Input.GetTouch(i).position);
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                RaycastHit2D[] hits = Physics2D.CircleCastAll(pos, touchSize, Vector2.zero);
                selectedShape[i] = null;
                foreach (RaycastHit2D hit in hits)
                {
                    if (hit.collider != null)
                    {
                        Shape shape = hit.collider.gameObject.GetComponent<Shape>();
                        if (shape != null)
                        {
                            start[i] = pos;
                            selectedShape[i] = shape;
                            selectedShape[i].OnTap();
                            selectedShape[i].OnDragStart(pos);
                            break;
                        }
                    }
                }
            }
            else if (Input.GetTouch(i).phase == TouchPhase.Moved)
            {
                if (selectedShape[i] != null)
                {
                    selectedShape[i].OnDrag(start[i], pos);
                }
                else
                {
                    RaycastHit2D[] hits = Physics2D.CircleCastAll(pos, touchSize, Vector2.zero);
                    foreach (RaycastHit2D hit in hits)
                    {
                        if (hit.collider != null)
                        {
                            Shape shape = hit.collider.gameObject.GetComponent<Shape>();
                            if (shape != null)
                            {
                                shape.OnSlice();
                                break;
                            }
                        }
                    }
                }
            }
            else if (Input.GetTouch(i).phase == TouchPhase.Ended)
            {
                if (selectedShape[i] != null)
                {
                    selectedShape[i].OnDragEnd(pos);
                    selectedShape[i] = null;
                }
            }
        }
    }

    bool checkPinching(out Shape pinchShape)
    {
        if (Input.touchCount != 2)
        {
            initialPinchDist = 0;
            pinchShape = null;
            return false;
        }
        Vector3 pointOne = cam.ScreenToWorldPoint(Input.touches[0].position);
        Vector3 pointTwo = cam.ScreenToWorldPoint(Input.touches[1].position);
        Vector2 midpoint = (pointOne + pointTwo) / 2;
        RaycastHit2D[] hits = Physics2D.CircleCastAll(midpoint, 0.2f, Vector2.zero);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null)
            {
                Shape shape = hit.collider.gameObject.GetComponent<Shape>();
                if (shape != null && ((shape.Tags & ShapeTags.OnPinch) == ShapeTags.OnPinch))
                {
                    initialPinchDist = Vector2.Distance(pointOne, pointTwo);
                    pinchShape = shape;
                    return true;
                }
            }
        }
        initialPinchDist = 0;
        pinchShape = null;
        return false;
    }

    private void CheckAcceleration()
    {
        newAccel = Input.acceleration;
        Vector3 accelDiff = newAccel - pastAccel;
        if (accelDiff.magnitude > accelSens && accelRecent == false)
        {
            accelRecent = true;
            OnAccelChange?.Invoke(accelDiff);
            StartCoroutine(ResetAccel());
        }
        pastAccel = newAccel;
    }

    private void CheckGyroscope()
    {
        Quaternion currGyro = Input.gyro.attitude;
        if (Quaternion.Angle(currGyro, pastGyro) > gyroSens)
        {
            OnGyroChange?.Invoke(currGyro);
            pastGyro = currGyro;
        }
    }

    private IEnumerator ResetAccel()
    {
        yield return new WaitForSeconds(accelCooldown);
        accelRecent = false;
    }

    void OnDrawGizmos()
    {
        if (Input.touchCount > 0)
        {
            Gizmos.color = Color.red;
            for (int i = 0; i < Input.touchCount; i++)
            {
                Vector3 pos = cam.ScreenToWorldPoint(Input.GetTouch(i).position);
                Gizmos.DrawSphere(pos, touchSize);
            }
        }

        Quaternion currGyro = Input.gyro.attitude;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(
            Vector2.zero,
            Quaternion.Euler(0, 0, currGyro.eulerAngles.z) * Vector2.left
        );
    }
}
