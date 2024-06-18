using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlPause : MonoBehaviour
{
    // Se inicializa el canvas del menu
    public GameObject pausa;
    // Se hace referencia al objeto del jugador, se pueden implementar mas cosas como objetos de enemigos etc
    public GameObject player;
    private RockyMovement rockyMovement;
    // Start is called before the first frame update
    void Start()
    {
        // Se obtiene el script del player al iniciar el script
        rockyMovement = player.GetComponent<RockyMovement>();
    }

    // Update is called once per frame
    void Update()
    {   
        // Funcion que detecta la tecla para el menu de Pausa
        if (Input.GetKeyDown(KeyCode.P))
        {
            Pausar();
        }
    }

    // Funcion para pausar el juego
    private void Pausar()
    {
        bool valor = true;
        if (pausa.activeSelf)
        {
            pausa.SetActive(!valor);
            Time.timeScale = 1f;
            // Condicion que reactiva el script del jugador
            rockyMovement.enabled = true;  // Reactivar el script de movimiento del jugador
        }
        else
        {
            pausa.SetActive(valor);
            Time.timeScale = 0f;//
            rockyMovement.enabled = false;  // Desactivar el script de movimiento del jugador
        }
    }
}
