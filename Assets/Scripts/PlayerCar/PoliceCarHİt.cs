using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceCarHİt : MonoBehaviour
{
    private bool isColliding;
    private void Update()
    {
        if(isColliding)
        {
            ReduceLife();
        }
       
    }

    private void OnCollisionStay(Collision collision)
    {
      
        if (collision.collider.CompareTag("Enemy"))
        {
            isColliding = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            isColliding = false;
        }
    }
    private void ReduceLife()
    {
        IDamageable damageable = this.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(10f);
            
        }
    }
  
}
