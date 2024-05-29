using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemySpawner : MonoBehaviour
{   
    public Transform[] spawnPoints;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject enemyPrefab2;
    [SerializeField] private GameObject enemyPrefab3;
    [SerializeField] private GameObject enemyPrefab4;
    [SerializeField] private float minimumSpawnTime;
    [SerializeField] private float maximumSpawnTime;
    [SerializeField] private TextMeshProUGUI textoTimer; 
    [SerializeField] private GameObject logroPacifista;
    [SerializeField] private GameObject logroRonda;  
    public float timer = 0;
    private float timeUntilSpawn;


    void Start()
    {
        SetTimeUntilSpawn();
        // timer += 1 * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer += 1 * Time.deltaTime;
        textoTimer.text = "" + timer.ToString("f0");

        if(timer <= 120){
            float peso1 = 50;
            float peso2 = 30;
            float peso3 = 19.8f;
            float peso4 = 0.2f;
            float pesoTotal = peso1 + peso2 + peso3 + peso4;
            // float pesoAcumulado = 0;
            float ranEnemy = Random.Range(0, pesoTotal);  
            timeUntilSpawn -= Time.deltaTime;
            int randSpawnPoint = Random.Range(0, spawnPoints.Length);


            if(timeUntilSpawn <= 0){
                if(0f <= ranEnemy && ranEnemy <= peso1){
                    Instantiate(enemyPrefab, spawnPoints[randSpawnPoint].position, Quaternion.identity);
                    SetTimeUntilSpawn();
                }

                else if(ranEnemy >= peso1 && ranEnemy <= peso1+peso2){
                    Instantiate(enemyPrefab2, spawnPoints[randSpawnPoint].position, Quaternion.identity);
                    SetTimeUntilSpawn();
                }

                else if(ranEnemy >= peso1+peso2 && ranEnemy <= peso1+peso2+peso3){
                    Instantiate(enemyPrefab3, spawnPoints[randSpawnPoint].position, Quaternion.identity);
                    SetTimeUntilSpawn();
                }

                else if(ranEnemy >= peso1+peso2+peso3 && ranEnemy <= peso1+peso2+peso3+peso4){
                    Instantiate(enemyPrefab4, spawnPoints[randSpawnPoint].position, Quaternion.identity);
                    SetTimeUntilSpawn();
                }
            
            }
        }

        else if(timer>120 && timer<=300){
            float peso1 = 25;
            float peso2 = 45;
            float peso3 = 27;
            float peso4 = 3;
            minimumSpawnTime=1;
            maximumSpawnTime=3;
            float pesoTotal = peso1 + peso2 + peso3 + peso4;
            // float pesoAcumulado = 0;
            float ranEnemy = Random.Range(0, pesoTotal);  
            timeUntilSpawn -= Time.deltaTime;
            int randSpawnPoint = Random.Range(0, spawnPoints.Length);


            if(timeUntilSpawn <= 0){
                if(0f <= ranEnemy && ranEnemy <= peso1){
                    Instantiate(enemyPrefab, spawnPoints[randSpawnPoint].position, Quaternion.identity);
                    SetTimeUntilSpawn();
                }

                else if(ranEnemy >= peso1 && ranEnemy <= peso1+peso2){
                    Instantiate(enemyPrefab2, spawnPoints[randSpawnPoint].position, Quaternion.identity);
                    SetTimeUntilSpawn();
                }

                else if(ranEnemy >= peso1+peso2 && ranEnemy <= peso1+peso2+peso3){
                    Instantiate(enemyPrefab3, spawnPoints[randSpawnPoint].position, Quaternion.identity);
                    SetTimeUntilSpawn();
                }

                else if(ranEnemy >= peso1+peso2+peso3 && ranEnemy <= peso1+peso2+peso3+peso4){
                    Instantiate(enemyPrefab4, spawnPoints[randSpawnPoint].position, Quaternion.identity);
                    SetTimeUntilSpawn();
                }
            
            }
        }

        else if(timer>300){
            if(timer>=300 && timer<305){
                StartCoroutine(activarLogro());
                if(logroRonda.transform.position.x <= 500){
                    logroRonda.transform.position = logroRonda.transform.position + new Vector3(5,0,0); 
                }
            }

            else if(logroRonda.transform.position.x >= -723){
                    logroRonda.transform.position = logroRonda.transform.position + new Vector3(-6,0,0); 
            }
            // else{
            //     desactivarLogro();
            // }
            float peso1 = 10;
            float peso2 = 40;
            float peso3 = 40;
            float peso4 = 10;
            minimumSpawnTime=0.5f;
            maximumSpawnTime=2.5f;
            float pesoTotal = peso1 + peso2 + peso3 + peso4;
            // float pesoAcumulado = 0;
            float ranEnemy = Random.Range(0, pesoTotal);  
            timeUntilSpawn -= Time.deltaTime;
            int randSpawnPoint = Random.Range(0, spawnPoints.Length);

            

            if(timeUntilSpawn <= 0){
                if(0f <= ranEnemy && ranEnemy <= peso1){
                    Instantiate(enemyPrefab, spawnPoints[randSpawnPoint].position, Quaternion.identity);
                    SetTimeUntilSpawn();
                }

                else if(ranEnemy >= peso1 && ranEnemy <= peso1+peso2){
                    Instantiate(enemyPrefab2, spawnPoints[randSpawnPoint].position, Quaternion.identity);
                    SetTimeUntilSpawn();
                }

                else if(ranEnemy >= peso1+peso2 && ranEnemy <= peso1+peso2+peso3){
                    Instantiate(enemyPrefab3, spawnPoints[randSpawnPoint].position, Quaternion.identity);
                    SetTimeUntilSpawn();
                }

                else if(ranEnemy >= peso1+peso2+peso3 && ranEnemy <= peso1+peso2+peso3+peso4){
                    Instantiate(enemyPrefab4, spawnPoints[randSpawnPoint].position, Quaternion.identity);
                    SetTimeUntilSpawn();
                }
            
            }
        }
        
    }

    private IEnumerator activarLogro()
    {
        logroRonda.SetActive(true);
        yield return new WaitForSeconds(10); 
        logroRonda.SetActive(false);
    }

    private void desactivarLogro(){
        logroRonda.SetActive(false); 
    }

    private void SetTimeUntilSpawn(){
        timeUntilSpawn = Random.Range(minimumSpawnTime, maximumSpawnTime);
    }
}
