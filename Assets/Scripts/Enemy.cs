using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public SpriteRenderer sprite;

    public void OnEnable()
    {
        sprite.enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        if (transform.parent.GetChild(0) != transform)
            gameObject.SetActive(Random.Range(0,2) == 1);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Zone":
            case "Player":
                GameManager.Instance.GameOver();
                break;
            case "Bullet":
                if (sprite.enabled)
                {
                    GameManager.Instance.UpdateScore();
                    other.gameObject.SetActive(false);
                    animator.SetTrigger("Pop");
                }
                break;     
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("DisabledZone"))
        {
            sprite.enabled = true;
        }
    }

    //Methods called in Animation Event
    private void DisableGameObject()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.SetActive(false);
    }
    
    private void DisableCollider()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
}