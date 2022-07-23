using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceHit : MonoBehaviour
{
    public delegate void OnPoliceHitEffectHandler(bool state);
    public static event OnPoliceHitEffectHandler OnPoliceHit;
    
    private Transform target;
    [SerializeField] float attackDistanceThresold;
    
    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
      
    }

   
    private void Start()
    {
        StartCoroutine(UpdateDamage());
    }
    

    IEnumerator UpdateDamage()
    {
        float refreshRate = 0.2f;
        while (target != null)
        {
            float sqrtToDistance = (target.position - transform.position).sqrMagnitude;
            if (sqrtToDistance <= Mathf.Pow(attackDistanceThresold, 2))
            {
                IDamageable damageable = target.GetComponent<IDamageable>();
                if(damageable!=null)
                {
                    damageable.TakeDamage(10f);
                    OnPoliceHit?.Invoke(true);
                }
              
            }
            else if(sqrtToDistance > Mathf.Pow(attackDistanceThresold+2f, 2))
            {
                OnPoliceHit?.Invoke(false);
            }
            yield return new WaitForSeconds(refreshRate);
        }
    }

}
