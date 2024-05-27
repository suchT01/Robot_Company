using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    private float _minimumSpawnTime;

    [SerializeField]
    private float _maximumSpawnTime;

    private float _timeUntilSpawn;

    public int currentRound = 1;
    public int enemiesRemaining = 0;

    public int enemiesPerRound = 2;
    public int contador = 0;
    public bool isWaitingForNextWave;
    public bool waveFinished;
    public TextMeshProUGUI counterText;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI enemiesText;
    public float counterToNextWave = 10;
    public float timeForNextWave = 10;

    public void checkCounterForNextWave(){
        if(isWaitingForNextWave && !waveFinished){
            counterToNextWave -= 1 * Time.deltaTime;
            // counterText.text = counterToNextWave.ToString("00");
            
            if(counterToNextWave <= 0){
                //Debug.Log("Empieza la siguiente ronda");
                isWaitingForNextWave = false;
                currentRound ++;
                enemiesPerRound += 2;
                contador = 0;
                startRound();
            }
        }

    }

    void Awake(){
        startRound();
        //SetTimeUntilSpawn();
    }

    void startRound(){
        // waveText.text = currentRound.ToString("00");
        counterToNextWave = 10;
        enemiesRemaining = enemiesPerRound;

        // Si la ronda actual es la 10, cambia a la nueva escena
        if (currentRound == 10)
        {
            SceneManager.LoadScene("Start");
        }
        else
        {
            spawnEnemies();
        }
    }

    void spawnEnemies()
    {
        StartCoroutine(SpawnEnemiesCoroutine());
    }

    IEnumerator SpawnEnemiesCoroutine()
    {
        isWaitingForNextWave = false;
        for (int i = 0; i < enemiesPerRound; i++)
        {
            Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(_minimumSpawnTime, _maximumSpawnTime));
            contador++;
        }
    }

    void Update(){
            if(enemiesRemaining >= 0){
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                // enemiesRemaining = enemiesPerRound - contador;

                // enemiesText.text = (enemies.Length).ToString("00");

                if (enemies.Length == 0 && contador>=enemiesPerRound){
                    isWaitingForNextWave = true;
                    checkCounterForNextWave();
                    //currentRound ++;
                    //enemiesPerRound += 5;
                    //startRound();
                }
            }
        }
    
    public void enemyDestroyed(){
        enemiesRemaining --;
    }
    
    void SetTimeUntilSpawn(){
        _timeUntilSpawn = Random.Range(_minimumSpawnTime, _maximumSpawnTime);
    }

}
