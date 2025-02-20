using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectRedTap : MonoBehaviour
{
    //just give some random particles that say that you tapped it but it did nothing
    [SerializeField]
    private ParticleSystem tapParticles;
    private ParticleSystem tapParticlesInstance;

    public void EmitParticles(EventInfo eventInfo)
    {
        //this part of the code is taken from https://www.youtube.com/watch?v=0HKSvT2gcuk at 7:29 to 8:08
        //instantiate particles
        tapParticlesInstance = Instantiate(
            tapParticles,
            new Vector2(0.02f, -0.07f),
            Quaternion.identity
        );
    }
}
