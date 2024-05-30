using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ItemDrop : MonoBehaviour
{
    private Rigidbody2D itemRb;
    public float dropForce = 5;
    public int Cura = 10;
    private AudioSource[] heal;
    // Start is called before the first frame update
    void Start()
    {
        itemRb = GetComponent<Rigidbody2D>();
        itemRb.AddForce(Vector2.up * dropForce, ForceMode2D.Impulse);
        heal = FindObjectsOfType<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCura(int Cura)
    {
        this.Cura = Cura;
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        RockyMovement rocky = collider.gameObject.GetComponent<RockyMovement>();

        if (rocky != null)
        {
            // List<AudioSource> healAudios = new List<AudioSource>();
            // foreach (AudioSource audioSource in heal)
            // {
            //     if (audioSource.gameObject.CompareTag("heal"))
            //     {
            //         healAudios.Add(audioSource);
            //     }
            // }
            // // int escogeAudio = Random.Range(0, healAudios.Count);
            // healAudios[0].Play();

            rocky.Curar(Cura);
            Destroy(gameObject);
        }
        else if (collider.gameObject.CompareTag("Wall"))
        {
            // Amen
        }
}

}
