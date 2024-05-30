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
    private AudioSource[] audioRonda;
    private AudioSource[] audioPacifista;
    private bool audioReproducido = false;
    private bool audioReproducido2 = false;
    public float timer = 0;
    private float timeUntilSpawn;


    void Start()
    {
        SetTimeUntilSpawn();
        audioRonda = FindObjectsOfType<AudioSource>();
        audioPacifista = FindObjectsOfType<AudioSource>();
        // timer += 1 * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject playerObject = GameObject.Find("Rocky");
        RockyMovement rocky = playerObject.GetComponent<RockyMovement>();
        int cDisparo = rocky.contadorDisparos;

        timer += 1 * Time.deltaTime;
        int tiempoRedondeado = Mathf.FloorToInt(timer);
        textoTimer.text = tiempoRedondeado.ToString();

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
            if(timer>=120 && timer<125 && cDisparo==0){
                if(!audioReproducido2){
                    audioLogro2();
                    audioReproducido2 = true;
                }

                StartCoroutine(activarLogro2());
                if(logroPacifista.transform.position.x <= 500){
                    logroPacifista.transform.position = logroPacifista.transform.position + new Vector3(5,0,0); 
                }
            }

            else if(logroPacifista.transform.position.x >= -723){
                    logroPacifista.transform.position = logroPacifista.transform.position + new Vector3(-6,0,0); 
            }

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
                if(!audioReproducido){
                    // Debug.Log("segundo 301");
                    audioLogro();
                    audioReproducido = true;
                }
                 // Establecer la bandera a true para que no se reproduzca nuevamente
                      
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

    private void audioLogro(){
        // Debug.Log("reproducir audio");
        List<AudioSource> rondaAudio = new List<AudioSource>();
            foreach (AudioSource audioSource in audioRonda)
            {
                if (audioSource.gameObject.CompareTag("aRonda"))
                {
                    rondaAudio.Add(audioSource);
                }
            }
        // int escogeAudio = Random.Range(0, bomboclatAudios.Count);
        rondaAudio[0].Play();
    }

    private void audioLogro2(){
        // Debug.Log("reproducir audio");
        List<AudioSource> pacifistaAudio = new List<AudioSource>();
            foreach (AudioSource audioSource in audioPacifista)
            {
                if (audioSource.gameObject.CompareTag("aPaz"))
                {
                    pacifistaAudio.Add(audioSource);
                }
            }
        // int escogeAudio = Random.Range(0, bomboclatAudios.Count);
        pacifistaAudio[0].Play();
    }

    private IEnumerator activarLogro2()
    {
        logroPacifista.SetActive(true);
        yield return new WaitForSeconds(10); 
        logroPacifista.SetActive(false);
    }

    private void desactivarLogro(){
        logroRonda.SetActive(false); 
    }

    private void desactivarLogro2(){
        logroPacifista.SetActive(false); 
    }

    private void SetTimeUntilSpawn(){
        timeUntilSpawn = Random.Range(minimumSpawnTime, maximumSpawnTime);
    }
}
