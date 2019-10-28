using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private ObjectPooler objectPooler;
    
    // Start is called before the first frame update
    void Start()
    {
        objectPooler = ObjectPooler.Instance; 
    }

    // Update is called once per frame

    private void SpawnEnemies()
    {
        objectPooler.SpawnFromPool("Enemy", transform.position);
    }
}

