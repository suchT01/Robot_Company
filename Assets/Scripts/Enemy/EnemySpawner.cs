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
        // Funcion que instancia los audios de las rondas al principio

        SetTimeUntilSpawn();
        audioRonda = FindObjectsOfType<AudioSource>();
        audioPacifista = FindObjectsOfType<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Update se encarga de actualizar el juego cada frame

        // Se busca al jugador principal para inicializar el contador de disparos y verificar requisitos del logro ninja
        GameObject playerObject = GameObject.Find("Rocky");
        RockyMovement rocky = playerObject.GetComponent<RockyMovement>();
        int cDisparo = rocky.contadorDisparos;

        // Se inicializa el contador general
        timer += 1 * Time.deltaTime;
        int tiempoRedondeado = Mathf.FloorToInt(timer);
        textoTimer.text = tiempoRedondeado.ToString();

        // Condicion que verifica la primera etapa del juego
        if(timer <= 120){
            // Funcion que spawnea los enemigos y les asigna un porcentaje de aparicion a cada uno
            // En base a 100%
            generateEnemy(50f,30f,19.8f,0.2f);
        }

        // Segunda etapa del juego
        else if(timer>120 && timer<=300){
            // Condicion que verifica el tiempo de aparicion del logro Ninja
            if(timer>=120 && timer<125 && cDisparo==0){
                if(!audioReproducido2){
                    audioLogro2();
                    audioReproducido2 = true;
                }
                // Funcion que inicia una secuencia para la aparicion del logro en pantalla
                StartCoroutine(activarLogro2());
                if(logroPacifista.transform.position.x <= 500){
                    logroPacifista.transform.position = logroPacifista.transform.position + new Vector3(5,0,0); 
                }
            }

            // Condicion que desaparece el logro de la pantalla
            else if(logroPacifista.transform.position.x >= -723){
                    logroPacifista.transform.position = logroPacifista.transform.position + new Vector3(-6,0,0); 
            }

            // Se cambian los tiempos de spawn enemies para la segunda etapa
            minimumSpawnTime=1;
            maximumSpawnTime=3;
            generateEnemy(25f,45f,27f,3f);

        }

        // Tercera etapa
        else if(timer>300){
            // Condicion para el logro de la etapa final
            if(timer>=300 && timer<305){
                if(!audioReproducido){
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

            // Tiempo de spawn enemies para la tercera ronda
            minimumSpawnTime=0.5f;
            maximumSpawnTime=2.5f;
            generateEnemy(10f,40f,40f,10f);
        }   
    }

    // Funcion que genera enemigos aleatorios en base a un porcentaje sobre 100%
    private void generateEnemy(float peso1, float peso2, float peso3, float peso4)
    {
        // Se declara un peso total en el cual la suma de todos los pesos es igual a 100%
        float pesoTotal = peso1 + peso2 + peso3 + peso4;
        // Se obtiene un valor aleatorio del peso
        float ranEnemy = Random.Range(0, pesoTotal);
        // Se asigna el tiempo antes de que los enemigos spawneen  
        timeUntilSpawn -= Time.deltaTime;
        // Se seleccion un spawn aleatoriamente 
        int randSpawnPoint = Random.Range(0, spawnPoints.Length);

        // En esta condicion se asignan el spawn del enemigo correspondiente a su peso
        // Se verifica que el numero arrojado este dentro del peso del enemigo
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

    // Funcion que inicia una secuencia para mostrar y quitar los logros de la pantalla
    private IEnumerator activarLogro()
    {
        logroRonda.SetActive(true);
        yield return new WaitForSeconds(10); 
        logroRonda.SetActive(false);
    }

    // Funcion que asigna el audio al logro de ultima ronda
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

    // Audio para el logro ninja
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

    // Funcion para aparicion y desaparicion del segundo logro
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
