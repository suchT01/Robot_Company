using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockyMovement : MonoBehaviour
{
    public GameObject BulletPrefab;
    public float Speed;
    public float JumpForce;
    public int Health;

    private Animator Animator;
    private Rigidbody2D Rigidbody2D;
    private float Horizontal;
    private bool Grounded;
    private float LastShoot;
    private bool isPlayerFacingRight = true;

    // Variable para almacenar la velocidad antes de cambiar la orientación
    private Vector2 savedVelocity;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D =  GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");

        if (Horizontal < 0.0f && isPlayerFacingRight)
        {
            isPlayerFacingRight = !isPlayerFacingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
            // Guarda la velocidad antes de cambiar la orientación
            savedVelocity = Rigidbody2D.velocity;
        }
        else if (Horizontal > 0.0f && !isPlayerFacingRight)
        {
            isPlayerFacingRight = !isPlayerFacingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
            // Guarda la velocidad antes de cambiar la orientación
            savedVelocity = Rigidbody2D.velocity;
        }

        Animator.SetBool("Running", Horizontal != 0.0f);
        
        if (Physics2D.Raycast(transform.position, Vector3.down, 1.0f))
        {
            Grounded = true;
        }else {
            Grounded = false;
        }

        if (Input.GetKeyDown(KeyCode.W) && Grounded)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > LastShoot + 0.25f)
        {
            Shoot();
            LastShoot = Time.time;
        }

    }

    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
    }

    private void Shoot()
    {
        Vector3 direction;
        if (isPlayerFacingRight)
            direction = Vector2.right;
        else
            direction = Vector2.left;

        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
        bullet.GetComponent<BulletPlayer>().SetDirection(direction);
    }

    private void FixedUpdate()
    {
        // Restaura la velocidad después de cambiar la orientación
        Rigidbody2D.velocity = new Vector2(Horizontal * Speed, Rigidbody2D.velocity.y);
    }

    public void Hit(int Damage)
    {
        Health = Health - Damage;
        if (Health == 0)
        {
            Destroy(gameObject);
        }
    }
}
