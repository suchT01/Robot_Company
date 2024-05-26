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
        Asimov asimov = other.GetComponent<Asimov>();
        AsimovR asimovr = other.GetComponent<AsimovR>();
        Scara scara = other.GetComponent<Scara>();
        Tank tank = other.GetComponent<Tank>();

        if(asimov != null){
            asimov.Hit(Damage);
            DestroyBullet();
        }

        else if (asimovr != null)
        {
            asimovr.Hit(Damage);
            DestroyBullet();
        }

        else if (scara != null)
        {
            scara.Hit(Damage);
            DestroyBullet();
        }

        else if (tank != null)
        {
            tank.Hit(Damage);
            DestroyBullet();
        }
        else if (other.CompareTag("Wall"))
        {
            DestroyBullet(); // Destruir la bala al colisionar con un muro
        }
    }
}
