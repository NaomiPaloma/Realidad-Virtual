using UnityEngine;

public class ObjetoFlotante : MonoBehaviour
{
    [Header("Configuración de Vuelo")]
    public float velocidadFlotacion = 2f;
    public float alturaFlotacion = 0.5f;

    [Header("Efectos")]
    public AudioClip sonidoMagico;
    private AudioSource audioSource;

    private Vector3 posicionInicial;
    private bool estaFlotando = false;

    // Nuestro propio cronómetro interno
    private float tiempoActual = 0f;

    void Start()
    {
        posicionInicial = transform.position;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;

        if (sonidoMagico != null)
        {
            audioSource.clip = sonidoMagico;
            audioSource.loop = true;
        }
    }

    void Update()
    {
        if (estaFlotando)
        {
            // El cronómetro avanza solo cuando está flotando
            tiempoActual += Time.deltaTime;

            // Usamos nuestro cronómetro en vez del reloj global
            float nuevaY = posicionInicial.y + Mathf.Sin(tiempoActual * velocidadFlotacion) * alturaFlotacion;
            transform.position = new Vector3(transform.position.x, nuevaY, transform.position.z);
        }
    }

    public void CambiarEstadoFlotacion(bool encender)
    {
        estaFlotando = encender;

        if (encender)
        {
            // ¡CLAVE! Reseteamos el cronómetro a 0.
            // Esto garantiza que el movimiento arranque al instante y sin demoras.
            tiempoActual = 0f;

            if (audioSource.clip != null)
            {
                audioSource.Play();
            }
        }
        else
        {
            transform.position = posicionInicial;
            audioSource.Stop();
        }
    }
}