using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneHit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
        if(damageable!=null)
        {
            damageable.TakeDamage(50f);
        }
    }
}
