using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta : MonoBehaviour
{
    Vector3 posicionCerrada;
    Vector3 posicionAbierta;
    bool abierta = false;
    float velocidad = 2f;

    void Start()
    {
        posicionCerrada = transform.position;
        posicionAbierta = transform.position + new Vector3(0, 3, 0); // sube 3 unidades
    }

    void Update()
    {
        if (abierta)
            transform.position = Vector3.Lerp(transform.position, posicionAbierta, velocidad * Time.deltaTime);
        else
            transform.position = Vector3.Lerp(transform.position, posicionCerrada, velocidad * Time.deltaTime);
    }

    public void AbrirPuerta()
    {
        abierta = true;
    }

    public void CerrarPuerta()
    {
        abierta = false;
    }
}

