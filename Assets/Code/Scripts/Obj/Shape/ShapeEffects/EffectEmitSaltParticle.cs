using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectEmitSaltParticle : EffectEmitParticle
{
    //just give some random particles that say that you tapped it but it did nothing

    public override void EmitParticles(EventInfo eventInfo)
    {
        //this part of the code is taken from https://www.youtube.com/watch?v=0HKSvT2gcuk at 7:29 to 8:08
        //instantiate particles
        //Vector3 saltPosition = new Vector3(0f, 0.880999982f, 0);
        Vector3 saltPosition = new Vector3(1f, 2.5f, 0);
        if (tapParticlesInstance == null)
        {
            tapParticlesInstance = Instantiate(
                tapParticles,
                eventInfo.TargetObject.transform.position - saltPosition,
                Quaternion.identity
            );
        }

        if (tapParticlesInstance != null && !tapParticlesInstance.isPlaying)
        {
            tapParticlesInstance.Play();
        }
    }
}
