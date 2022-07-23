using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PoliceController : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private float speed, rotationSpeed;
    private Transform target;
    private Rigidbody _rb;

    
    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        _rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        if(target==null)
        {
            agent.enabled = false;
        }
        else
        {
            StartCoroutine(UpdatePath());
        }
    }

    private void Start()
    {
        StartCoroutine(UpdatePath());
    }
    

     public IEnumerator UpdatePath()
    {
        
        float refreshRate = 0.2f;
        while(target!=null)
        {
            Vector3 targetPosition = new Vector3(target.position.x, 0f, target.position.z);
            agent.SetDestination(targetPosition);
            yield return new WaitForSeconds(refreshRate);
        }
    }





    private void PoliceCarDirectMovement()
    {  
        _rb.velocity = transform.forward * speed;
    }
    private void PoliceCarAngularMovement()
    {
        Vector3 pointTarget = transform.position - target.transform.position;
        pointTarget.Normalize();
        float value = Vector3.Cross(pointTarget, transform.forward).y;
        _rb.angularVelocity = rotationSpeed * value * new Vector3(0, 1, 0) * Time.fixedDeltaTime;
    }
}
