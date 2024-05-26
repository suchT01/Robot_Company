using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : MonoBehaviour
{

    public float Speed;
    public int Damage;


    private Rigidbody2D rb;
    private Vector2 Direction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        rb.velocity = Direction * Speed;
    }

    public void SetDirection(Vector2 direction)
    {
        Direction = direction;
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }

    public void SetDamage(int Damage)
    {
        this.Damage = Damage;
    }

    private void OnTriggerEnter2D(Collider2D other){
        
        FollowAI enemy = other.GetComponent<FollowAI>();
        if (enemy != null)
        {
            enemy.Hit(Damage);
        }

        Destroy(gameObject);  
    }
}
