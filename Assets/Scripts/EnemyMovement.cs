using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour, IPooledObject
{
    public float speed;

    private Vector3 destination;
    private Vector3 direction;

    public void OnObjectSpawn()
    {
        EnableChildren();
        destination = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f));
        direction = destination - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;
    }

    private void EnableChildren()
    {
        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(true);

    }

    // Update is called once per frame
    private void Update()
    {
        if (NoActiveChildren()) gameObject.SetActive(false);
        transform.position += speed * Time.deltaTime * destination;
    }

    private bool NoActiveChildren()
    {
        for (int i = 0; i < transform.childCount; i++)
            if (transform.GetChild(i).gameObject.activeSelf)
                return false; 
        return true;
    }
}
