using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public GameObject Rocky;


    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;
        position.x = Rocky.transform.position.x;
        transform.position = position;
    }
}
