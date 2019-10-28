using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    #region Singleton
    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion   

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    
    // Start is called before the first frame update
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            
            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string objTag, Vector3 position)
    {
        if (!poolDictionary.ContainsKey(objTag))
        {
            Debug.LogWarning($"Pool haven't {objTag} tag");
            return null;
        }
        GameObject objectToSpawn = poolDictionary[objTag].Dequeue();
        
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = new Quaternion(0,0,90,90);

        IPooledObject pooledObject = objectToSpawn.GetComponent<IPooledObject>();

        if (pooledObject != null)
        {
            pooledObject.OnObjectSpawn();
        }

        poolDictionary[objTag].Enqueue(objectToSpawn);
        return objectToSpawn;
    }
    
    // Update is called once per frame
    public void DisableAllPooledObjects()
    {
        foreach (KeyValuePair<string,Queue<GameObject>> keyValuePair in poolDictionary)
        {
            foreach (GameObject gameObject in keyValuePair.Value)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
