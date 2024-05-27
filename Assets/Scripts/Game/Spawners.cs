using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawners : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // string spawnerName = gameObject.name; // Obtener el nombre único del spawner
        Vector3 spawnPosition = transform.position; // Obtener la posición del spawner
        WaveManager.Instance.SetSpawnPosition(spawnPosition); // Pasar el nombre único y la posición al WaveManager
        WaveManager.Instance.startRound();
    }

    // Update is called once per frame
    void Update()
    {
        // Vector3 spawnPosition = transform.position;
        // WaveManager.Instance.SetSpawnPosition(spawnPosition);
    }
}
