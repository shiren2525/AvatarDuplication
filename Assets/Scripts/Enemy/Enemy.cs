using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EnemyBase {

    private void Awake()
    {
        EnemySetUp();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            DamageEnemy(); 
        }
        if (collision.gameObject.tag == "Avatar")
        {
            DamageEnemy();
        }
    }

}
