using System;
using UnityEngine;

public class Bullet : MonoBehaviour, IPooledObject
{
    public float speed;
    
    private Vector3 direction;
    private Vector3 target;

    public void OnObjectSpawn()
    {
        target = PlayerController.Instance.center.position;
        direction = target - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;
    }
    
    private void Update() 
    {
        transform.position += speed * Time.deltaTime * transform.right;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Center"))
        {
            gameObject.SetActive(false);
        }
    }
}