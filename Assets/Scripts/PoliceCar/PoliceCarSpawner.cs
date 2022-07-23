using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PoliceCarSpawner : Singleton<PoliceCarSpawner>
{
    [SerializeField] Transform[] policeCarSpawnPos;
    [SerializeField] private int policeCarRequired;

    private int currentPoliceCar;
    private GameObject target;
    public int CurrentPoliceCar
    {
        get { return currentPoliceCar; }
        set { currentPoliceCar = value; }
    }
   
    private void Update()
    {

        if (target==null)
        {
            target = GameObject.FindGameObjectWithTag("Player");
            return;
        }
        PoliceCarIncreaser();
        if (currentPoliceCar<policeCarRequired && UIManager.GetInstance().GameStarted)
        {
            SpawnPoliceCar();
        }
    }

    private void SpawnPoliceCar()
    {
        GameObject policeCar = ObjectPooling.GetInstance().GetPooledObject("PoliceCar");
        int randomPosition = Random.Range(0, policeCarSpawnPos.Length);
        policeCar.transform.position = new Vector3(policeCarSpawnPos[randomPosition].position.x, 1f, policeCarSpawnPos[randomPosition].position.z);
        policeCar.SetActive(true);
        policeCar.gameObject.GetComponent<PoliceHealth>().Health = policeCar.gameObject.GetComponent<PoliceHealth>().maxHealth;
        currentPoliceCar++;
    }

    private void PoliceCarIncreaser()
    {
        if(UIManager.GetInstance().Score%10==0 && UIManager.GetInstance().Score>0)
        {
            if (policeCarRequired < 3)
                policeCarRequired++;
        }
        if (UIManager.GetInstance().Score % 20 == 0 && UIManager.GetInstance().Score > 0)
        {
            if (policeCarRequired < 4)
                policeCarRequired++;
        }
        if (UIManager.GetInstance().Score % 30 == 0 && UIManager.GetInstance().Score > 0)
        {
            if (policeCarRequired < 5)
                policeCarRequired++;
        }

    }
}
    