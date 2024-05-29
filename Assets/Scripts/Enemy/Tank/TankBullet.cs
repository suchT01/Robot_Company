using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBullet : MonoBehaviour
{
    [SerializeField] private float speed;
    public int Damage;
    private Transform player;
    private Rigidbody2D rb;

    void Start()
    {
        player = FindObjectOfType<RockyMovement>()?.transform; // Usamos el operador de null conditional '?'
        rb = GetComponent<Rigidbody2D>();

        if (player != null) // Verificamos si el jugador se ha encontrado correctamente
        {
            LauchProjectile(); // Lanzamos el proyectil solo si el jugador se ha encontrado
        }
        else
        {
            Debug.LogWarning("No se encontró el objeto del jugador.");
        }
    }

    public void SetDamage(int Damage)
    {
        this.Damage = Damage;
    }

    private void LauchProjectile()
    {
        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg; // Calcula el ángulo entre el proyectil y el jugador
        rb.velocity = directionToPlayer * speed; // Establece la velocidad del proyectil en esa dirección
        // Corrección para invertir la rotación
        angle += 180f;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); // Rota el proyectil para que apunte hacia el jugador
        StartCoroutine(DestroyProjectile());
    }

    IEnumerator DestroyProjectile(){
        float destroyTime = 5f;
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            RockyMovement rocky = other.GetComponent<RockyMovement>();
            if (rocky != null)
            {
                rocky.Hit(Damage);
            }

            Destroy(gameObject);
        }  
        else if (other.CompareTag("Wall"))
        {
            Destroy(gameObject); // Destruir la bala al colisionar con un muro
        } 
        else if (other.CompareTag("dropEscudo"))
        {
            Destroy(gameObject); // Destruir la bala al colisionar con el escudo
        }  
    }

    void Update()
    {
        // Aquí puedes agregar cualquier lógica de actualización adicional si es necesario
    }
}
