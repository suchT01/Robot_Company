using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controladorEscudo : MonoBehaviour
{
    private SpriteRenderer sR;
    private CapsuleCollider2D cC;
    private GameObject dropEscudo;
    void Start()
    {
        sR = GetComponent<SpriteRenderer>();
        cC = GetComponent<CapsuleCollider2D>();
        dropEscudo = GameObject.FindGameObjectWithTag("escudito");
        // activaEscudo();
        // StartCoroutine(activarEscudoTemporal());
    }

    void Update(){
        
    }
    public void activaEscudo(){
        sR.enabled = true;
        cC.enabled = true;
    }

    public void desactivaEscudo()
    {
        sR.enabled = false;
        cC.enabled = false;
    }

    public IEnumerator activarEscudoTemporal()
    {
        if(sR.enabled!=true && cC.enabled!=true){
            Debug.Log("Activar escudo");
            sR.enabled = true;
            cC.enabled = true;
            yield return new WaitForSeconds(10);
            Debug.Log("desactivar escudo"); 
            sR.enabled = false;
            cC.enabled = false;

            Destroy(dropEscudo);

            yield break;
        }        
    }
}
