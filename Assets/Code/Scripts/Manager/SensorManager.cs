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
    float gyroCooldown;

    [SerializeField]
    float gyroSens;

    public event Action OnAccelChange;

    //public event Action OnGyroChange;

    private Vector3 pastAccel = Vector3.zero;
    private Vector3 newAccel;
    bool accelRecent = false;

    void Update()
    {
        newAccel = Input.acceleration;
        Vector3 accelDiff = newAccel - pastAccel;
        if (accelDiff.magnitude > accelSens && accelRecent == false)
        {
            accelRecent = true;
            OnAccelChange?.Invoke();
            StartCoroutine(resetAccel());
        }
        pastAccel = newAccel;
    }

    private IEnumerator resetAccel()
    {
        yield return new WaitForSeconds(accelCooldown);
        accelRecent = false;
    }
}
