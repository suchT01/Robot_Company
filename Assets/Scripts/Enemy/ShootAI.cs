using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAI : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float timeBetweenShoots;
    [SerializeField] private Transform shootPoint;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot(){
        while(true){
            yield return new WaitForSeconds(timeBetweenShoots);
            Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
        }
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
