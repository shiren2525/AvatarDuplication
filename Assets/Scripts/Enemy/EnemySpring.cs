using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpring : EnemyBase {

    public float jumpForce;

    private Rigidbody2D rig2D;

    private void Awake()
    {
        rig2D = GetComponent<Rigidbody2D>();
        EnemySetUp();
    }

    private void Update()
    {
       Move();
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
           rig2D.AddForce(Vector2.up * jumpForce);            
        }
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
