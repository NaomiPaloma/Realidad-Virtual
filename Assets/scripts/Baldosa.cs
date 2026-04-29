using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaldosaObjetos : MonoBehaviour
{
    public Puerta puerta; // Arrastrás la puerta acá en el Inspector

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

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            puerta.CerrarPuerta();
        }
    }
}
