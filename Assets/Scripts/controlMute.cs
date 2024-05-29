using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlMute : MonoBehaviour
{
    public AudioSource musica;
    void Start()
    {
        musica = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void muteaMusica(bool valor){
        Debug.Log("mute");
        if(valor && musica.volume>0){
            musica.volume = 0;
        }
        else{
            musica.volume = 0.1f;
        }
    }
}
