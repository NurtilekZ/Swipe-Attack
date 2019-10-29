﻿﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
  using UnityEngine.Assertions.Must;
  using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public Transform center;
    public float speed;
    public float range;
    
    private float circlePosition = -1.58f;
    private Vector2 direction;
    private float currentMovementSpeed;

    private ObjectPooler objectPooler;
    private GameManager gameManager;
    
    #region Singleton
    public static PlayerController Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        objectPooler = ObjectPooler.Instance;
        gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    private void Update()
    {
        if (gameManager.currentGameState == GameManager.GameState.MENU)
        {
            circlePosition = -1.58f;
            return;
        }
        if (Input.touchCount == 0)
        {
            currentMovementSpeed = 0f;
            return;
        }

        //circle Movement

        if (Input.GetTouch(0).position.x < 150f ||
            Math.Abs(Input.GetTouch(0).position.x - Screen.width) < 150f)
        {  
            if (Math.Abs(Input.GetTouch(0).deltaPosition.x) > 2.5f)
                currentMovementSpeed = Input.GetTouch(0).deltaPosition.x;
        }
        else
            currentMovementSpeed = Input.GetTouch(0).deltaPosition.x;
        
        circlePosition += 0.05f * currentMovementSpeed * Time.deltaTime * speed;
        float x = Mathf.Cos(circlePosition) * range;
        float y = Mathf.Sin(circlePosition) * range;
        Vector2 position = new Vector2(x, y);
        
        //circle Rotation, Look at the center
        direction = center.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
        transform.SetPositionAndRotation(position, rotation);
    }
    
    //Method called in Animation Event
    private void Fire()
    {
        objectPooler.SpawnFromPool("Bullet", transform.position);
    }
}
