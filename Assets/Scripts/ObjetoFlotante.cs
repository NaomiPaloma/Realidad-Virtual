using UnityEngine;

public class ObjetoFlotante : MonoBehaviour
{
    [Header("Configuración de Vuelo")]
    public float velocidadFlotacion = 2f; // Qué tan rápido sube y baja
    public float alturaFlotacion = 0.5f;  // Qué tan alto llega

    [Header("Efectos")]
    public AudioClip sonidoMagico;
    private AudioSource audioSource;

    private Vector3 posicionInicial;
    private bool estaFlotando = false;

    void Start()
    {
        // Guardamos dónde estaba el objeto originalmente para que vuelva ahí al apagar la luz
        posicionInicial = transform.position;

        // Configuramos el AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        if (estaFlotando)
        {
            // Calcula la nueva posición en Y usando un movimiento de onda suave
            float nuevaY = posicionInicial.y + Mathf.Sin(Time.time * velocidadFlotacion) * alturaFlotacion;
            transform.position = new Vector3(transform.position.x, nuevaY, transform.position.z);
        }
    }

    public void CambiarEstadoFlotacion(bool encender)
    {
        estaFlotando = encender;

        if (encender)
        {
            // Reproduce el sonido mágico al empezar a flotar
            if (sonidoMagico != null)
            {
                audioSource.PlayOneShot(sonidoMagico);
            }
        }
        else
        {
            // Lo devuelve suavemente a su posición original al apagar la luz
            transform.position = posicionInicial;
        }
    }
}