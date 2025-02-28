using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectRedSwipe : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem dragParticles;
    private ParticleSystem dragParticlesInstance;

    public void DrawSparks(EventInfo eventInfo)
    {
        dragParticlesInstance = Instantiate(
            dragParticles,
            eventInfo.TargetObject.transform.position,
            Quaternion.identity
        );
    }
}
