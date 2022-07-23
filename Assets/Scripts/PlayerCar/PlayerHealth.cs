using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : UniversalHealth,IDamageable,IHealable
{
    public delegate void OnPlayerDeadHandler();
    public static event OnPlayerDeadHandler OnPlayerDead;

    public delegate void GameOverScreenHandler();
    public static event GameOverScreenHandler OnGameOverScreen;
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
            UIManager.GetInstance().GameStarted = false;
           OnPlayerDead?.Invoke();
            OnGameOverScreen?.Invoke();
        }
    }

    public void TakeHeal(float healthBooster)
    {
        Health += healthBooster;
        Health = Mathf.Clamp(Health, 0, maxHealth);
    }
}
