using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class escudoDrop : MonoBehaviour
{
    private Rigidbody2D itemRb;
    private BoxCollider2D boxCd;
    private SpriteRenderer cuadro;
    private CapsuleCollider2D baseEscudo;
    public float dropForce = 5;
    [SerializeField] private int tiempoEscudo;
    [SerializeField] private GameObject escudo;
    
    void Start()
    {
        itemRb = GetComponent<Rigidbody2D>();
        boxCd = GetComponent<BoxCollider2D>();
        cuadro = GetComponent<SpriteRenderer>();
        baseEscudo = GetComponent<CapsuleCollider2D>();
        itemRb.AddForce(Vector2.up * dropForce, ForceMode2D.Impulse);
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
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

        if (rocky != null)
        {
            boxCd.enabled = false;
            cuadro.enabled = false;
            baseEscudo.enabled = false;
            // StartCoroutine(activarEscudoTemporal());
            
            StartCoroutine(activarEscudoTemporal());
            // Destroy(gameObject);
            
        }
        else if (collider.gameObject.CompareTag("Wall"))
        {
            // Amen
        }
    }
}
