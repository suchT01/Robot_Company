using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Asimov : MonoBehaviour
{
    public int Health;
    private AudioSource dañoBot;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float timeBetweenShoots;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float speed;
    [SerializeField] private float minDistance;
    private Transform player;
    private bool isFacingRight = false;
    private float lastShootTime;
    private GameManager gameManager;
    [SerializeField] private GameObject efectoMuerte;
    [SerializeField] private GameObject itemDrops;
    [SerializeField] private GameObject dropBomba;
    [SerializeField] private GameObject dropEscudo;

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("No se encontró al jugador en la escena.");
        }
        
        gameManager = FindObjectOfType<GameManager>();
        dañoBot =  GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            Follow();
            bool isPlayerRight = transform.position.x < player.position.x;
            Flip(isPlayerRight);
        }
        else
        {
            Debug.LogWarning("El objeto del jugador ha sido destruido.");
        }
    }

    private void Follow()
    {
        if (player != null && Vector2.Distance(transform.position, player.position) > minDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            
            CancelInvoke("AttackRepeatedly");
        }
        else if (player != null)
        {
            if (!IsInvoking("AttackRepeatedly"))
            {
                if (Time.time - lastShootTime > timeBetweenShoots)
                {
                    Attack();
                    lastShootTime = Time.time; // Actualizar el tiempo del último disparo
                }
            }

            if (Vector2.Distance(transform.position, player.position) < (minDistance - 0.1f))
            {
                Vector2 direction = (Vector2)transform.position - (Vector2)player.position;
                transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + direction, speed * Time.deltaTime);
                
            }
        }
    }

    private void Attack()  
    {
        // Invocar el método AttackRepeatedly() con un intervalo de tiempo específico
        InvokeRepeating("AttackRepeatedly", 0, timeBetweenShoots); 
    }

    private void AttackRepeatedly()
    {
        if (player != null) // Verificación adicional antes de disparar
        {
            Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
        }
        else
        {
            CancelInvoke("AttackRepeatedly");
        }
    }

    private void Flip(bool isPlayerRight)
    {
        if((isFacingRight && !isPlayerRight) || (!isFacingRight && isPlayerRight)){
            isFacingRight = !isFacingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    public void Hit(int Damage)
    {
        Health = Health - Damage;
        // dañoBot.Play();
        if (Health <= 0)
        {
            // dañoBot.Play();
            Vector3 newPosition = transform.position + new Vector3(0f, 0.5f, 0f);
            Instantiate(efectoMuerte, newPosition, Quaternion.identity);
            Destroy(gameObject);
            ItemDrop();
            WaveManager.Instance.enemyDestroyed();
            
        }
    }

    private void ItemDrop(){
        float pesoBomba = 2;
        float pesoVida = 5;
        float pesoEscudo = 3;
        float pesoNada = 90;
        float pesoTotal = pesoBomba+pesoVida+pesoEscudo+pesoNada;
        float spawnRate = Random.Range(0, pesoTotal);

        if(spawnRate >= 90 && spawnRate < 95){
            Instantiate(itemDrops, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        }
        else if(spawnRate >= 95 && spawnRate < 98){
            Instantiate(dropEscudo, transform.position + new Vector3(0,1,0), Quaternion.identity);
        }
        else if(spawnRate >= 98 && spawnRate < 100){
            Instantiate(dropBomba, transform.position + new Vector3(0,1,0), Quaternion.identity);
        }
    }
}
