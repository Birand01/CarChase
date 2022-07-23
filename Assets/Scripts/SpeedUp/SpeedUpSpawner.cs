using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpSpawner : Singleton<SpeedUpSpawner>
{
    [SerializeField] Transform[] speedUpSpawnPos;
    [SerializeField] private int speeUpGemRequired;

    private int currentSpeedUpGem;
    public int CurrentSpeedUpGem
    {
        get { return currentSpeedUpGem; }
        set { currentSpeedUpGem = value; }
    }


    private void Update()
    {
        SpeedUpIncreaser();
        if (currentSpeedUpGem < speeUpGemRequired && UIManager.GetInstance().GameStarted)
        {
            SpawnSpeedUpGem();
        }
       
    }
    private void SpawnSpeedUpGem()
    {
        GameObject speedUpGem = ObjectPooling.GetInstance().GetPooledObject("SpeedPowerUp");
        int randomPosition = Random.Range(0, speedUpSpawnPos.Length);
        speedUpGem.transform.position = new Vector3(speedUpSpawnPos[randomPosition].position.x, 1f, speedUpSpawnPos[randomPosition].position.z);
        speedUpGem.SetActive(true);
        currentSpeedUpGem++;
    }
    private void SpeedUpIncreaser()
    {
        if (UIManager.GetInstance().Score % 10 == 0 && UIManager.GetInstance().Score > 0)
        {
            if (speeUpGemRequired < 3)
                speeUpGemRequired++;
        }
        if (UIManager.GetInstance().Score % 30 == 0 && UIManager.GetInstance().Score > 0)
        {
            if (speeUpGemRequired < 4)
                speeUpGemRequired++;
        }
        if (UIManager.GetInstance().Score % 50 == 0 && UIManager.GetInstance().Score > 0)
        {
            if (speeUpGemRequired < 5)
                speeUpGemRequired++;
        }

    }
}
