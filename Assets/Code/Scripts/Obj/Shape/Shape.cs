using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Shape : MonoBehaviour
{
    public ShapeTags Tags;

    [SerializeField]
    private string spriteName = null;

    [SerializeField]
    private string uniqueID = null;
    public string ShapeName
    {
        get
        {
            if (String.IsNullOrEmpty(uniqueID))
            {
                return spriteName;
            }
            return $"{spriteName}-{uniqueID}";
        }
    }
    public string SpriteName
    {
        get { return spriteName; }
    }
    public string UniqueID
    {
        get { return uniqueID; }
    }

    bool combined;

    #region UnityEvents
    [SerializeField]
    private UnityEvent<EventInfo> OnFixedUpdateEvent;

    [SerializeField]
    private UnityEvent<EventInfo> OnDragStartEvent;

    [SerializeField]
    private UnityEvent<EventInfo> OnDragEvent;

    [SerializeField]
    private UnityEvent<EventInfo> OnDragEndEvent;

    [SerializeField]
    private UnityEvent<EventInfo> OnPinchEvent;

    [SerializeField]
    private UnityEvent<EventInfo> OnSliceEvent;

    [SerializeField]
    private UnityEvent<EventInfo> OnSliceEndEvent;

    [SerializeField]
    private UnityEvent<EventInfo> OnTapEvent;

    [SerializeField]
    private UnityEvent<EventInfo> OnAccelerateEvent;

    [SerializeField]
    private UnityEvent<EventInfo> OnAttitudeChangeEvent;

    [SerializeField]
    private UnityEvent<EventInfo> OnGravityChangeEvent;

    [SerializeField]
    private UnityEvent<EventInfo> OnCreateEvent;

    [SerializeField]
    private UnityEvent<EventInfo> OnDestroyEvent;

    public static event Action GlobalTap;

    public static event Action GlobalDrag;

    public static event Action GlobalSlice;

    public static event Action GlobalPinch;
    #endregion

    #region EventMethods
    public void OnDragStart(Vector3 touchPos)
    {
        if ((Tags & ShapeTags.OnDrag) != ShapeTags.OnDrag)
        {
            return;
        }
        OnDragStartEvent.Invoke(new EventInfo(targetObject: gameObject, vectorOne: touchPos));
    }

    public void OnDrag(Vector3 startPos, Vector3 currentPos)
    {
        if ((Tags & ShapeTags.OnDrag) != ShapeTags.OnDrag)
        {
            return;
        }
        OnDragEvent.Invoke(
            new EventInfo(targetObject: gameObject, vectorOne: startPos, vectorTwo: currentPos)
        );
        GlobalDrag?.Invoke();
    }

    public void OnDragEnd(Vector3 touchPos)
    {
        if ((Tags & ShapeTags.OnDrag) != ShapeTags.OnDrag)
        {
            return;
        }
        OnDragEndEvent.Invoke(new EventInfo(targetObject: gameObject, vectorOne: touchPos));
    }

    public void OnPinch(float initialDist, Vector3 fingerOne, Vector3 fingerTwo)
    {
        if ((Tags & ShapeTags.OnPinch) != ShapeTags.OnPinch)
        {
            return;
        }
        OnPinchEvent.Invoke(
            new EventInfo(
                targetObject: gameObject,
                floatValue: initialDist,
                vectorOne: fingerOne,
                vectorTwo: fingerTwo
            )
        );
        GlobalPinch?.Invoke();
    }

    public void OnSlice()
    {
        if ((Tags & ShapeTags.OnSlice) != ShapeTags.OnSlice)
        {
            return;
        }
        OnSliceEvent.Invoke(new EventInfo(targetObject: gameObject));
        GlobalSlice?.Invoke();
    }

    public void OnSliceEnd(Vector3 touchPos)
    {
        if ((Tags & ShapeTags.OnSlice) != ShapeTags.OnSlice)
        {
            return;
        }
        OnSliceEndEvent.Invoke(new EventInfo(targetObject: gameObject, vectorOne: touchPos));
    }

    public void OnTap()
    {
        if ((Tags & ShapeTags.OnTap) != ShapeTags.OnTap)
        {
            return;
        }
        OnTapEvent.Invoke(new EventInfo(targetObject: gameObject));
        GlobalTap?.Invoke();
    }

    public void OnAccelerate(Vector3 acceleration)
    {
        if ((Tags & ShapeTags.OnAccelerate) != ShapeTags.OnAccelerate)
        {
            return;
        }
        OnAccelerateEvent.Invoke(new EventInfo(targetObject: gameObject, vectorOne: acceleration));
    }

    public void OnAttitudeChange(Quaternion attitude)
    {
        if ((Tags & ShapeTags.OnAttitudeChange) != ShapeTags.OnAttitudeChange)
        {
            return;
        }
        OnAttitudeChangeEvent.Invoke(
            new EventInfo(targetObject: gameObject, quaternionValue: attitude)
        );
    }

    public void OnGravityChange(Vector3 gravity)
    {
        if ((Tags & ShapeTags.OnGravityChange) != ShapeTags.OnGravityChange)
        {
            return;
        }
        OnGravityChangeEvent.Invoke(new EventInfo(targetObject: gameObject, vectorOne: gravity));
    }

    void Awake()
    {
        gameObject.layer = LayerMask.NameToLayer("Shape");
    }

    public void Combined()
    {
        combined = true;
    }

    void Start()
    {
        if ((Tags & ShapeTags.OnAccelerate) == ShapeTags.OnAccelerate)
        {
            ShapeEventSystem.Instance.OnAccelChange += OnAccelerate;
        }
        if ((Tags & ShapeTags.OnAttitudeChange) == ShapeTags.OnAttitudeChange)
        {
            ShapeEventSystem.Instance.OnGyroChange += OnAttitudeChange;
        }
        if ((Tags & ShapeTags.OnGravityChange) == ShapeTags.OnGravityChange)
        {
            ShapeEventSystem.Instance.OnGravChange += OnGravityChange;
        }

        ShapeManager.Instance.CreateShapeEvent(this);
        if ((Tags & ShapeTags.OnCreate) != ShapeTags.OnCreate)
        {
            return;
        }
        OnCreateEvent.Invoke(new EventInfo(targetObject: gameObject));
    }

    void FixedUpdate()
    {
        if ((Tags & ShapeTags.OnFixedUpdate) != ShapeTags.OnFixedUpdate)
        {
            return;
        }
        OnFixedUpdateEvent.Invoke(new EventInfo(targetObject: gameObject));
    }

    void OnDestroy()
    {
        if ((Tags & ShapeTags.OnAccelerate) == ShapeTags.OnAccelerate)
        {
            ShapeEventSystem.Instance.OnAccelChange -= OnAccelerate;
        }
        if ((Tags & ShapeTags.OnAttitudeChange) == ShapeTags.OnAttitudeChange)
        {
            ShapeEventSystem.Instance.OnGyroChange -= OnAttitudeChange;
        }
        if ((Tags & ShapeTags.OnGravityChange) == ShapeTags.OnGravityChange)
        {
            ShapeEventSystem.Instance.OnGravChange -= OnGravityChange;
        }

        //https://stackoverflow.com/a/68126990
        if (!gameObject.scene.isLoaded)
        {
            return;
        }
        ShapeManager.Instance.DestroyShapeEvent(this);
        if (combined || (Tags & ShapeTags.OnDestroy) != ShapeTags.OnDestroy)
        {
            return;
        }
        OnDestroyEvent.Invoke(new EventInfo(targetObject: gameObject));
    }
    #endregion

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log("COLLLLLISON");
        Shape shape = collision.gameObject.GetComponent<Shape>();
        if (shape != null)
        {
            ShapeManager.Instance.CheckShapeCollide((shape, this));
        }
    }

#if UNITY_EDITOR
    [SerializeField]
    private bool showFixedUpdate;

    [SerializeField]
    private bool showDrag;

    [SerializeField]
    private bool showPinch;

    [SerializeField]
    private bool showSlice;

    [SerializeField]
    private bool showTap;

    [SerializeField]
    private bool showAccelerate;

    [SerializeField]
    private bool showAttitude;

    [SerializeField]
    private bool showGravity;

    [SerializeField]
    private bool showCreate;

    [SerializeField]
    private bool showDestroy;
#endif
}
