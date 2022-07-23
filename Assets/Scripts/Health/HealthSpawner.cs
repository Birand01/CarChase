using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSpawner : Singleton<HealthSpawner>
{
    [SerializeField] Transform[] healthSpawnPos;
    [SerializeField] private int healthGemRequired;

    private int currentHealthGem;
    public int CurrentHealthGem
    {
        get { return currentHealthGem; }
        set { currentHealthGem = value; }
    }


    private void Update()
    {
        HealthGemIncreaser();
        if (currentHealthGem < healthGemRequired && UIManager.GetInstance().GameStarted)
        {
            SpawnSpeedUpGem();
        }

    }
    private void SpawnSpeedUpGem()
    {
        GameObject healthGem = ObjectPooling.GetInstance().GetPooledObject("Health");
        int randomPosition = Random.Range(0, healthSpawnPos.Length);
        healthGem.transform.position = new Vector3(healthSpawnPos[randomPosition].position.x, 1f, healthSpawnPos[randomPosition].position.z);
        healthGem.SetActive(true);
        currentHealthGem++;
    }
    private void HealthGemIncreaser()
    {
        if (UIManager.GetInstance().Score % 10 == 0 && UIManager.GetInstance().Score > 0)
        {
            if (healthGemRequired < 3)
                healthGemRequired++;
        }
        if (UIManager.GetInstance().Score % 30 == 0 && UIManager.GetInstance().Score > 0)
        {
            if (healthGemRequired < 4)
                healthGemRequired++;
        }
        if (UIManager.GetInstance().Score % 50 == 0 && UIManager.GetInstance().Score > 0)
        {
            if (healthGemRequired < 5)
                healthGemRequired++;
        }

    }
}
