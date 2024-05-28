using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ItemDrop : MonoBehaviour
{
    private Rigidbody2D itemRb;
    public float dropForce = 5;
    public int Cura = 10;
    // Start is called before the first frame update
    void Start()
    {
        itemRb = GetComponent<Rigidbody2D>();
        itemRb.AddForce(Vector2.up * dropForce, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCura(int Cura)
    {
        this.Cura = Cura;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        RockyMovement rocky = collision.gameObject.GetComponent<RockyMovement>();

        if (rocky != null)
        {
            rocky.Curar(Cura);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            // Amen
        }
}

}
