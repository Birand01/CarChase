using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceHealth : UniversalHealth,IDamageable
{
    [SerializeField] private ParticleSystem deadParticleEffect;
    protected override void Start()
    {
        base.Start();

    }
    protected override void Update()
    {

        base.Update();
    }
    public void TakeDamage(float damage)
    {
        Health -= damage;
        Health = Mathf.Clamp(Health, 0, maxHealth);
        CheckIfDead();
    }
    private void CheckIfDead()
    {
        if (Health <= 0)
        {
            StartCoroutine(TimeToDie());
        }
    }
    IEnumerator TimeToDie()
    {
        deadParticleEffect.Play();
        PoliceCarSpawner.GetInstance().CurrentPoliceCar--;
        yield return new WaitForSeconds(1.0f);
        deadParticleEffect.Stop();
        this.gameObject.SetActive(false);

    }
}
