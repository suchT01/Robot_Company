using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject enemyPrefab2;
    [SerializeField] private GameObject enemyPrefab3;
    [SerializeField] private GameObject enemyPrefab4;
    [SerializeField] private float minimumSpawnTime;
    [SerializeField] private float maximumSpawnTime;
    private float timeUntilSpawn;


    void Start()
    {
        SetTimeUntilSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        float peso1 = 50;
        float peso2 = 30;
        float peso3 = 18;
        float peso4 = 2;
        float pesoTotal = peso1 + peso2 + peso3 + peso4;
        // float pesoAcumulado = 0;
        float ranEnemy = Random.Range(0, pesoTotal);  
        timeUntilSpawn -= Time.deltaTime;

        if(timeUntilSpawn <= 0){
            if(0f <= ranEnemy && ranEnemy <= peso1){
                Instantiate(enemyPrefab, transform.position, Quaternion.identity);
                SetTimeUntilSpawn();
            }

            else if(ranEnemy >= peso1 && ranEnemy <= peso1+peso2){
                Instantiate(enemyPrefab2, transform.position, Quaternion.identity);
                SetTimeUntilSpawn();
            }

            else if(ranEnemy >= peso1+peso2 && ranEnemy <= peso1+peso2+peso3){
                Instantiate(enemyPrefab3, transform.position, Quaternion.identity);
                SetTimeUntilSpawn();
            }

            else if(ranEnemy >= peso1+peso2+peso3 && ranEnemy <= peso1+peso2+peso3+peso4){
                Instantiate(enemyPrefab4, transform.position, Quaternion.identity);
                SetTimeUntilSpawn();
            }
            
        }
    }

    private void SetTimeUntilSpawn(){
        timeUntilSpawn = Random.Range(minimumSpawnTime, maximumSpawnTime);
    }
}
