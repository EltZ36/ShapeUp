using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorManager : MonoBehaviour
{
    #region Singleton Pattern
    public static SensorManager _instance;
    public static SensorManager Instance
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
    }
    #endregion
    [SerializeField]
    float accelCooldown = 1;

    [SerializeField]
    float accelSens = 1;

    [SerializeField]
    float gyroSens = 1;

    private float offset = 0;

    public event Action OnAccelChange;

    public event Action<float> OnGyroChange;

    private Vector3 pastAccel = Vector3.zero;
    private Vector3 newAccel;
    bool accelRecent = false;

    private Quaternion pastGyro;

    void Start()
    {
        Input.gyro.enabled = true;
        CalibrateOffset();
    }

    void Update()
    {
        newAccel = Input.acceleration;
        Vector3 accelDiff = newAccel - pastAccel;
        if (accelDiff.magnitude > accelSens && accelRecent == false)
        {
            accelRecent = true;
            OnAccelChange?.Invoke();
            StartCoroutine(ResetAccel());
        }
        pastAccel = newAccel;

        Quaternion currGyro = Input.gyro.attitude;
        if (Mathf.Abs(currGyro.eulerAngles.z - pastGyro.eulerAngles.z) > gyroSens)
        {
            OnGyroChange?.Invoke(currGyro.eulerAngles.z + offset);
            pastGyro = currGyro;
        }
    }

    private IEnumerator ResetAccel()
    {
        yield return new WaitForSeconds(accelCooldown);
        accelRecent = false;
    }

    private void CalibrateOffset()
    {
        //assumes current device orientation is neutral landscape right, probably needs to be inverted for landscape left.
        Debug.Log("change");
        float z = Input.gyro.attitude.eulerAngles.z;
        offset = 270 - z;
    }
}
