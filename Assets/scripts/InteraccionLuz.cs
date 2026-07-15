using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class InteraccionLuz : MonoBehaviour
{
    [Header("Luces del cuarto")]
    public Light[] luces = new Light[6];

    [Header("Objetos Mágicos")]
    public ObjetoFlotante[] objetosQueFlotan;

    [Header("Sonidos del interruptor")]
    public AudioClip sonidoEncendido;
    public AudioClip sonidoApagado;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Ahora es public para que el SistemaAgarre lo pueda ejecutar al mirarlo
    public void ToggleTodasLasLuces()
    {
        bool algunaEncendida = false;
        foreach (Light luz in luces)
        {
            if (luz != null && luz.enabled)
            {
                algunaEncendida = true;
                break;
            }
        }

        // Determinamos el nuevo estado (si había alguna prendida, las apagamos. Si no, las prendemos)
        bool nuevoEstado = !algunaEncendida;
        foreach (Light luz in luces)
        {
            if (luz != null)
                luz.enabled = nuevoEstado;
        }

        // Le avisamos a todos los objetos mágicos que empiecen a flotar (o dejen de hacerlo)
        foreach (ObjetoFlotante obj in objetosQueFlotan)
        {
            if (obj != null)
            {
                obj.CambiarEstadoFlotacion(nuevoEstado);
            }
        }

        // Reproducimos el sonido correspondiente según el nuevo estado
        ReproducirSonido(nuevoEstado);
    }

    void ReproducirSonido(bool encendiendo)
    {
        AudioClip clip = encendiendo ? sonidoEncendido : sonidoApagado;

        if (audioSource != null && clip != null)
            audioSource.PlayOneShot(clip);
    }
}