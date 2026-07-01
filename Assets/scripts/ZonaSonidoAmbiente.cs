using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ZonaSonidoAmbiente : MonoBehaviour
{
    [Header("Configuración de sonido")]
    public AudioClip sonidoAmbiente;
    public float volumenMaximo = 0.8f;
    public float duracionFade = 1.5f; // segundos que tarda el fade in/out

    private AudioSource audioSource;
    private Coroutine fadeCoroutine;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = sonidoAmbiente;
        audioSource.loop = true;      // clave: que se mantenga sonando
        audioSource.volume = 0f;      // arranca en silencio, el fade lo sube
        audioSource.playOnAwake = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!audioSource.isPlaying)
                audioSource.Play();

            IniciarFade(volumenMaximo);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IniciarFade(0f);
        }
    }

    void IniciarFade(float volumenObjetivo)
    {
        // si ya hay un fade corriendo, lo cortamos para arrancar el nuevo
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(FadeVolumen(volumenObjetivo));
    }

    IEnumerator FadeVolumen(float volumenObjetivo)
    {
        float volumenInicial = audioSource.volume;
        float tiempoTranscurrido = 0f;

        while (tiempoTranscurrido < duracionFade)
        {
            tiempoTranscurrido += Time.deltaTime;
            float t = tiempoTranscurrido / duracionFade;
            audioSource.volume = Mathf.Lerp(volumenInicial, volumenObjetivo, t);
            yield return null;
        }

        audioSource.volume = volumenObjetivo;

        // si terminó bajando a 0, pausamos para no gastar recursos de más
        if (volumenObjetivo == 0f)
            audioSource.Stop();
    }
}
