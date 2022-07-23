using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : Singleton<ObjectPooling>
{
  

    public ObjectPoolItems[] itemsToPool;

    private List<GameObject> pooledObjects;


    // Use this for initialization
    protected override void Awake()
    {

        pooledObjects = new List<GameObject>();

        foreach (ObjectPoolItems item in itemsToPool)
        {
            for (int i = 0; i < item.poolAmount; i++)
            {
                GameObject obj = Instantiate(item.poolObject);
                obj.name = item.name;
                obj.transform.parent = this.transform;
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }
    }

    public GameObject GetPooledObject(string name)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (pooledObjects[i].activeInHierarchy == false && pooledObjects[i].name == name)
                return pooledObjects[i];
        }

        foreach (ObjectPoolItems item in itemsToPool)
        {
            if (item.poolObject.name == name)
            {
                GameObject obj = Instantiate(item.poolObject);
                obj.name = item.name;
                obj.transform.parent = this.transform;
                obj.SetActive(false);
                pooledObjects.Add(obj);
                return obj;
            }
        }

        return null;
    }

}
[System.Serializable]
public class ObjectPoolItems
{
    public string name;
    public int poolAmount;
    public GameObject poolObject;
    public bool shouldExpland;
}
