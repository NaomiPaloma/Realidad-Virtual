using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta : MonoBehaviour
{
    [Header("Sonido de la Puerta")]
    [Tooltip("Arrastrá acá el archivo de audio (MP3/WAV) de la puerta abriéndose")]
    public AudioClip sonidoAbrir;

    Vector3 posicionCerrada;
    Vector3 posicionAbierta;
    bool abierta = false;
    float velocidad = 2f;

    void Start()
    {
        posicionCerrada = transform.position;
        posicionAbierta = transform.position + new Vector3(0, 3, 0);
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
        // Verificamos que esté cerrada antes de abrirla para que el sonido no se repita como loco
        if (!abierta)
        {
            // Reproduce el sonido si le asignaste uno en el Inspector
            if (sonidoAbrir != null)
            {
                AudioSource.PlayClipAtPoint(sonidoAbrir, transform.position);
            }
            abierta = true;
        }
    }

    public void CerrarPuerta()
    {
        abierta = false;
    }
}
