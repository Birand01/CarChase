using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthGainer : PickUp
{
    protected override void OnPickup(PlayerController playerController)
    {
        IHealable healable = playerController.GetComponent<IHealable>();
        if(healable!=null)
        {
            healable.TakeHeal(10f);
            HealthSpawner.GetInstance().CurrentHealthGem--;
        }
    }
}
