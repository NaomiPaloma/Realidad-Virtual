using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Requerimos Rigidbody porque vamos a manipular sus físicas
[RequireComponent(typeof(Rigidbody))]
public class PickableObject : MonoBehaviour
{
    // Referencias cacheadas para rendimiento
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public Collider coll;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        // Asumimos que tiene al menos un collider
        coll = GetComponent<Collider>();
    }

    // Método profesional: preparamos el objeto para ser cargado
    public void OnPickedUp()
    {
        rb.isKinematic = true;  // Desactivamos físicas para que no caiga
        coll.enabled = false;   // Desactivamos colisiones para que no empuje al jugador
    }

    // Método profesional: preparamos el objeto para volver al mundo
    public void OnDropped()
    {
        rb.isKinematic = false; // Reactivamos físicas
        coll.enabled = true;    // Reactivamos colisiones
    }
}

