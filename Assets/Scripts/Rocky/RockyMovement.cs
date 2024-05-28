using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RockyMovement : MonoBehaviour
{
    [SerializeField] private GameObject derrota;
    [SerializeField] private GameObject pausa;
    [SerializeField] private AudioSource disparo;
    [SerializeField] private AudioSource salto;
    [SerializeField] private AudioSource recibeDmg;
    [SerializeField] private AudioSource muere;
    [SerializeField] private BarraDeVida barraDeVida;
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

    public float maxFallSpeed = -10f;

    // Variable para almacenar la velocidad antes de cambiar la orientación
    private Vector2 savedVelocity;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D =  GetComponent<Rigidbody2D>();
        Rigidbody2D.interpolation = RigidbodyInterpolation2D.Interpolate; // Suaviza el movimiento
        Animator = GetComponent<Animator>();
        barraDeVida.InicializarBarraDeVida(Health);
        // audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        
        if (Horizontal < 0.0f && isPlayerFacingRight)
        {
            isPlayerFacingRight = !isPlayerFacingRight;
            Vector3 scale = transform.localScale;
            // Guarda la velocidad antes de cambiar la orientación
            savedVelocity = Rigidbody2D.velocity;
        }
        else if (Horizontal > 0.0f && !isPlayerFacingRight)
        {
            isPlayerFacingRight = !isPlayerFacingRight;
            Vector3 scale = transform.localScale;
            // Guarda la velocidad antes de cambiar la orientación
            savedVelocity = Rigidbody2D.velocity;
        }

        Animator.SetFloat("Movement", Horizontal);
        
        if (Horizontal != 0)
        {
            Animator.SetFloat("LastX", Horizontal);
        }

        if (Physics2D.Raycast(transform.position, Vector3.down, 1f))
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

        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            bool valor = true;
            if(pausa.activeSelf){
                pausa.SetActive(!valor);
            }
            else{
                pausa.SetActive(valor);
            }
        }

    }

    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
        salto.Play();
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
        disparo.Play();
    }

    private void FixedUpdate()
    {
        // Restaura la velocidad después de cambiar la orientación
        Rigidbody2D.velocity = new Vector2(Horizontal * Speed, Rigidbody2D.velocity.y);


        if (Rigidbody2D.velocity.y < maxFallSpeed)
        {
            Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, maxFallSpeed);
        }
    }

    public void Hit(int Damage)
    {
        Health = Health - Damage;
        
        if (Health <= 0)
        {
            Destroy(gameObject);
            muere.Play();
            derrota.SetActive(true);
        }
        else{
            barraDeVida.CambiarVidaActual(Health);
            recibeDmg.Play();
        }
    }

    private void CargarMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Curar(int Cura){
        if(Health <= 95){
            Health = Health + Cura;
            barraDeVida.CambiarVidaActual(Health);
        }
        else if(Health > 100){
            Health = 100;
            barraDeVida.CambiarVidaActual(Health);
        }
    }
}
