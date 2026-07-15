using UnityEngine;

public class CuervoSonido : MonoBehaviour
{
    [Tooltip("Sonido que hará el cuervo al presionar E")]
    public AudioClip sonidoCuervo;

    [Tooltip("Distancia máxima para poder interactuar con el cuervo")]
    public float distanciaInteraccion = 3f;

    private Transform jugador;
    private AudioSource audioSource;

    private void Start()
    {
        // Busca automáticamente al jugador por su Tag
        GameObject jugadorObj = GameObject.FindGameObjectWithTag("Player");
        if (jugadorObj != null)
        {
            jugador = jugadorObj.transform;
        }

        // Obtiene o agrega un AudioSource al cuervo
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Update()
    {
        if (jugador == null) return;

        float distancia = Vector3.Distance(transform.position, jugador.position);

        if (distancia <= distanciaInteraccion && Input.GetKeyDown(KeyCode.E))
        {
            HacerRuido();
        }
    }

    private void HacerRuido()
    {
        if (sonidoCuervo != null)
        {
            audioSource.PlayOneShot(sonidoCuervo);
            Debug.Log("¡El cuervo hizo ruido!");
        }
        else
        {
            Debug.LogWarning("No asignaste un clip de audio al cuervo.");
        }
    }
}