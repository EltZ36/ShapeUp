using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopParticle : MonoBehaviour
{
    [SerializeField]
    ParticleSystem target;

    [SerializeField]
    Vector2 velocity;

    public void Stop(EventInfo eventInfo)
    {
        target.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    }
}
