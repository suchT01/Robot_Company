using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class escudoDrop : MonoBehaviour
{
    private Rigidbody2D itemRb;
    private BoxCollider2D boxCd;
    private SpriteRenderer cuadro;
    private CapsuleCollider2D baseEscudo;
    public float dropForce = 5;
    [SerializeField] private int tiempoEscudo;
    private GameObject escudo;
    private GameObject player;
    private RockyMovement rocky;
    // public controladorEscudo shield;
    
    void Start()
    {
        itemRb = GetComponent<Rigidbody2D>();
        boxCd = GetComponent<BoxCollider2D>();
        cuadro = GetComponent<SpriteRenderer>();
        baseEscudo = GetComponent<CapsuleCollider2D>();
        itemRb.AddForce(Vector2.up * dropForce, ForceMode2D.Impulse);
        player = GameObject.FindGameObjectWithTag("Player");
        escudo = GameObject.FindGameObjectWithTag("dropEscudo");
        if(escudo != null){
            Debug.Log("objeto encontrado");
        }
        else{
            Debug.Log("objeto no encontrado");
        }
        
        // GameObject shieldObject = GameObject.FindGameObjectWithTag("dropEscudo");
        // if (shieldObject != null)
        // {
        //     // Obtener una referencia al componente controladorEscudo en el GameObject encontrado
        //     shield = shieldObject.GetComponent<controladorEscudo>();
        // }
        // else
        // {
        //     Debug.LogError("No se encontró ningún GameObject con la etiqueta 'Shield'");
        // }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void desactivaEscudo()
    {
        escudo.SetActive(false);
    }

    private IEnumerator activarEscudoTemporal()
    {
        
        escudo.SetActive(true);
        yield return new WaitForSeconds(tiempoEscudo); 
        desactivaEscudo();
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        RockyMovement rocky = collider.gameObject.GetComponent<RockyMovement>();
        // controladorEscudo shield;
        // GameObject shieldObject = GameObject.FindGameObjectWithTag("dropEscudo");
        // shield = shieldObject.GetComponent<controladorEscudo>();
        controladorEscudo shield = GameObject.Find("Capsule").GetComponent<controladorEscudo>();
        if (shield != null) {
            Debug.Log("exito");
        }

        if (rocky != null)
        {
            boxCd.enabled = false;
            cuadro.enabled = false;
            baseEscudo.enabled = false;
            StartCoroutine(shield.activarEscudoTemporal());
            // Destroy(gameObject);
            
        }
        else if (collider.gameObject.CompareTag("Wall"))
        {
            // Amen
        }
    }
}
