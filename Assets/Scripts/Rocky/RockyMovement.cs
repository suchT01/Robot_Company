using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RockyMovement : MonoBehaviour
{
    [SerializeField] private GameObject derrota;
    [SerializeField] private AudioSource disparo;
    [SerializeField] private AudioSource salto;
    [SerializeField] private AudioSource recibeDmg;
    [SerializeField] private AudioSource muere;
    [SerializeField] private BarraDeVida barraDeVida;
    [SerializeField] private GameObject soundEffects;
    [SerializeField] private GameObject iconoMute;
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
    public float maxJumpForce;
    private Collider2D platformCollider;

    // Variable para almacenar la velocidad antes de cambiar la orientación
    private Vector2 savedVelocity;
    [SerializeField] private GameObject escudo;
    public int tiempoEscudo = 3;
    public int contadorDisparos = 0;
    private GameObject camara;
    // public controlMute controladorCamara;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D =  GetComponent<Rigidbody2D>();
        Rigidbody2D.interpolation = RigidbodyInterpolation2D.Interpolate; // Suaviza el movimiento
        Animator = GetComponent<Animator>();
        barraDeVida.InicializarBarraDeVida(Health);
        // camara = 
        // controladorCamara = Find.GetComponent<controlMute>();

        // if(controladorCamara != null){
        //     Debug.Log("exito");
        // }
        // else{
        //     Debug.Log("error");
        // }
        // audioSource = GetComponent<AudioSource>();
        Time.timeScale = 1f;
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

        if (Input.GetKeyDown(KeyCode.S) && platformCollider != null)
        {
            StartCoroutine(DisablePlatformCollider());
        }
        
        if (Input.GetKeyDown(KeyCode.M))
        {
            mutear();
        }

    }

    private void mutear(){
        controlMute muteCamara = GameObject.Find("Main Camera").GetComponent<controlMute>();
        // GameObject iconoMute = GameObject.Find("Mute");

        if (muteCamara != null) {
            Debug.Log("exito");
        }
    
        bool valor = true;
    
    // Muta la música antes de cambiar el estado de los efectos de sonido
        muteCamara.muteaMusica(valor);

        if(iconoMute.activeSelf){
            iconoMute.SetActive(!valor);
        }
        else{
            iconoMute.SetActive(valor);
        }
    
    // Cambia el estado de los efectos de sonido después de mutar la música
        if(soundEffects.activeSelf){
            soundEffects.SetActive(!valor);
            // iconoMute.SetActive(!valor);
        }
        else{
            soundEffects.SetActive(valor);
            // iconoMute.SetActive(valor);
        }
    }

    private IEnumerator DisablePlatformCollider()
    {
        platformCollider.enabled = false;
        yield return new WaitForSeconds(0.2f); // Tiempo durante el cual el personaje puede atravesar la plataforma
        platformCollider.enabled = true;
    }

    public void desactivaEscudo()
    {
        escudo.SetActive(false);
    }

    public IEnumerator activarEscudoTemporal()
    {
        escudo.SetActive(true);
        Debug.Log("Escudo Activado");
        yield return new WaitForSeconds(tiempoEscudo); 
        desactivaEscudo();
        Debug.Log("Escudo Desactivado");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("CenterWall"))
        {
            platformCollider = collision.collider;
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
        contadorDisparos++;
        bullet.GetComponent<BulletPlayer>().SetDirection(direction);
        disparo.Play();
    }

    private void FixedUpdate()
    {
        // Restaura la velocidad después de cambiar la orientación
        Rigidbody2D.velocity = new Vector2(Horizontal * Speed, Rigidbody2D.velocity.y);

        // Condicion que evalua si la velocidad de caida del persona aumenta mucho, le pone un limite fijo
        if (Rigidbody2D.velocity.y < maxFallSpeed)
        {
            Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, maxFallSpeed);
        }

        // Condicion que evalua si la velocidad de salto es muy grande, le fija un limite
        if (Rigidbody2D.velocity.y > maxJumpForce)
        {
            Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, maxJumpForce);
        }
    }

    public void Hit(int Damage)
    {
        Health = Health - Damage;
        
        if (Health <= 0)
        {
            if(Health < 0){
                Health = 0;
                barraDeVida.CambiarVidaActual(Health);
                Destroy(gameObject);
                muere.Play();
                derrota.SetActive(true);
                Time.timeScale = 0f;
            }
            else{
                barraDeVida.CambiarVidaActual(Health);
                Destroy(gameObject);
                muere.Play();
                derrota.SetActive(true);
                Time.timeScale = 0f;
            }
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
            if(Health > 100){
                Health = 100;
            }
            barraDeVida.CambiarVidaActual(Health);
        }
        else if(Health > 100){
            Health = 100;
            barraDeVida.CambiarVidaActual(Health);
        }
    }
}
