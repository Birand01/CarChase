using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEffects : MonoBehaviour
{
    // The following particle systems are used as tire smoke when the car drifts.
    [SerializeField] private ParticleSystem RLWParticleSystem;
    [SerializeField] private ParticleSystem RRWParticleSystem;
    [SerializeField] private ParticleSystem hitParticle;
    [SerializeField] private ParticleSystem speedUpPowerCollect;

    [Space(10)]
    // The following trail renderers are used as tire skids when the car loses traction.
    public TrailRenderer RLWTireSkid;
    public TrailRenderer RRWTireSkid;
    public TrailRenderer speedPowerUpTrail;
   
    //public GameObject driftSoundPrefab;
    private void OnEnable()
    {
        SpeedPowerUp.OnSpeedPowerUp += SpeedPowerUpTrailEffect;
        PoliceHit.OnPoliceHit += HitParticleEffect;
        PlayerController.OnParticleEffect += ParticleEffect;
        PlayerController.OnTrailEffect += TrailEffects;   
    }  
    private void ParticleEffect(bool state)
    {
        if(state)
        {
            RLWParticleSystem.Play();
            RRWParticleSystem.Play();
        }
        else
        {
            RLWParticleSystem.Stop();
            RRWParticleSystem.Stop();
        }
    }

    private void TrailEffects(bool state)
    {
        if (RLWTireSkid != null)
        {
            RLWTireSkid.emitting = state;
        }
        if (RRWTireSkid != null)
        {
            RRWTireSkid.emitting = state;
        }

    }
    private void SpeedPowerUpTrailEffect(bool state)
    {
        speedPowerUpTrail.enabled = state;

        //Spawns collect particle
        if(speedUpPowerCollect!=null && state==true)
        {
            Instantiate(speedUpPowerCollect, transform.position, Quaternion.identity);
        }
    }
    private void HitParticleEffect(bool state)
    {
        if(hitParticle!=null)
        {
            if (state)
            {
                hitParticle.Play();
            }
            else
            {
                hitParticle.Stop();
            }
        }
        
       
    }
    
    private void OnDisable()
    {
       
        SpeedPowerUp.OnSpeedPowerUp -= SpeedPowerUpTrailEffect;
        PlayerController.OnParticleEffect -= ParticleEffect;
        PlayerController.OnTrailEffect -= TrailEffects;
        PoliceHit.OnPoliceHit -= HitParticleEffect;
       
    }
}
