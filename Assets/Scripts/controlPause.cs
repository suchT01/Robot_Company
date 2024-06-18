using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlPause : MonoBehaviour
{
    public GameObject pausa;
    public GameObject player;
    private RockyMovement rockyMovement;
    // Start is called before the first frame update
    void Start()
    {
        rockyMovement = player.GetComponent<RockyMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Pausar();
        }
    }

    private void Pausar()
    {
        bool valor = true;
        if (pausa.activeSelf)
        {
            pausa.SetActive(!valor);
            Time.timeScale = 1f;
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
