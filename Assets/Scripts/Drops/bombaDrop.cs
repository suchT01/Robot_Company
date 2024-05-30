using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class bombaDrop : MonoBehaviour
{
    private Rigidbody2D itemRb;
    public float dropForce = 5;
    private GameObject[] enemiesObject;
    [SerializeField] private GameObject efectoMuerte;
    private AudioSource[] bomboclat;
    
    // Start is called before the first frame update
    void Start()
    {
        itemRb = GetComponent<Rigidbody2D>();
        itemRb.AddForce(Vector2.up * dropForce, ForceMode2D.Impulse);
        enemiesObject = GameObject.FindGameObjectsWithTag("Enemy");
        bomboclat = FindObjectsOfType<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        enemiesObject = GameObject.FindGameObjectsWithTag("Enemy");
        // if (playerObject != null)
        // {
        //     player = playerObject.transform;
        // }
        // else
        // {
        //     Debug.LogError("No se encontró al jugador en la escena.");
        // }
    }

    public void eliminaEnemigos()
    {
        if(enemiesObject != null){
            foreach (GameObject enemy in enemiesObject)
            {
                Instantiate(efectoMuerte, enemy.transform.position + new Vector3(0, 0.7f, 0), Quaternion.identity);
                Destroy(enemy); // Destruye cada enemigo en el array
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        RockyMovement rocky = collider.gameObject.GetComponent<RockyMovement>();

        if (rocky != null)
        {
            // List<AudioSource> bomboclatAudios = new List<AudioSource>();
            // foreach (AudioSource audioSource in bomboclat)
            // {
            //     if (audioSource.gameObject.CompareTag("bomboclat"))
            //     {
            //         bomboclatAudios.Add(audioSource);
            //     }
            // }
            // int escogeAudio = Random.Range(0, bomboclatAudios.Count);
            // bomboclatAudios[escogeAudio].Play();
            eliminaEnemigos();
            Destroy(gameObject);
        }
        else if (collider.gameObject.CompareTag("Wall"))
        {
            // Amen
        }
    }

}
