using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CuervoSonido : MonoBehaviour
{
    [Tooltip("Sonido que hará el cuervo al presionar E")]
    public AudioClip sonidoCuervo;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Esta función es 'public' para que FeedbackInteraccion pueda llamarla
    public void HacerRuido()
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