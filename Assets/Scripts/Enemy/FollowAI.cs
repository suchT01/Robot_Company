using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowAI : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float timeBetweenShoots;
    [SerializeField] private Transform shootPoint;

    [SerializeField] private float speed;
    [SerializeField] private float minDistance;
    private Transform player;

    private bool isFacingRight = false;
    private float lastShootTime;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (player == null)
        {
            Debug.LogError("No se encontró al jugador en la escena.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Follow();

        bool isPlayerRight = transform.position.x < player.transform.position.x;
        Flip(isPlayerRight);
    }

    private void Follow()
    {
        if(Vector2.Distance(transform.position, player.position) > minDistance)
        {
            // StopCoroutine(Shoot());
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            CancelInvoke("AttackRepeatedly");

        }
        // else if(Vector2.Distance(transform.position, player.position) < (minDistance - 0.3))
        // {
        //     Vector2 direction = (Vector2)transform.position - (Vector2)player.position;
        //     transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + direction, speed * Time.deltaTime);

        //     // if (Time.time - lastShootTime > timeBetweenShoots)
        //     // {
        //     //     Attack();
        //     //     lastShootTime = Time.time; // Actualizar el tiempo del último disparo
        //     // }
        //     // Attack();
        //     // StartCoroutine(Shoot());
        //     // transform.position = Vector2.MoveTowards(transform, (transform.position + direction), speed * Time.deltaTime);
        // }
        else
        {
            if (!IsInvoking("AttackRepeatedly")){
                if (Time.time - lastShootTime > timeBetweenShoots)
                {
                    Attack();
                    lastShootTime = Time.time; // Actualizar el tiempo del último disparo
                }   
            }
            

            if(Vector2.Distance(transform.position, player.position) < (minDistance - 0.1)){
                Vector2 direction = (Vector2)transform.position - (Vector2)player.position;
                transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + direction, speed * Time.deltaTime);
            }
            // Attack();
            // StartCoroutine(Shoot());
        }
        

    }

    private void Attack()  
    {
        // Invocar el método AttackRepeatedly() con un intervalo de tiempo específico
        InvokeRepeating("AttackRepeatedly", 0, timeBetweenShoots); 
    }

    private void AttackRepeatedly()
    {
        Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
    }

    // IEnumerator Shoot(){
    //     while(true){
    //         yield return new WaitForSeconds(timeBetweenShoots);
    //         Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
    //     }
    // }

    private void Flip(bool isPlayerRight)
    {
        if((isFacingRight && !isPlayerRight) || (!isFacingRight && isPlayerRight)){
            isFacingRight = !isFacingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
}
