using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaldosaObjetos : MonoBehaviour
{
    public Puerta puerta; 

    void Start()
    {

    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            puerta.AbrirPuerta();
        }
    }

}
