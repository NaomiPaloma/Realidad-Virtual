using UnityEngine;

/// <summary>
/// Reproduce un sonido de paso al azar cada vez que el personaje camina.
/// Adaptado para funcionar con el sistema de movimiento por Transform (Input clásico).
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class PlayerFootsteps : MonoBehaviour
{
    [Header("Clips de pasos")]
    [Tooltip("Lista de sonidos de pasos. Se elige uno al azar cada vez.")]
    public AudioClip[] clipsDePasos = new AudioClip[6];

    [Header("Ritmo de pasos")]
    [Tooltip("Segundos entre paso y paso cuando el jugador camina")]
    public float intervaloCaminar = 0.45f;

    [Tooltip("Segundos entre paso y paso cuando el jugador corre. (Manten presionado LeftShift)")]
    public float intervaloCorrer = 0.3f;

    [Header("Variación aleatoria")]
    [Tooltip("Variación de volumen al azar, +/- este valor")]
    [Range(0f, 0.5f)] public float variacionVolumen = 0.1f;

    [Tooltip("Variación de pitch al azar, +/- este valor (evita que suene repetitivo)")]
    [Range(0f, 0.5f)] public float variacionPitch = 0.1f;

    [Tooltip("Volumen base de los pasos")]
    [Range(0f, 1f)] public float volumenBase = 0.7f;

    [Header("Referencias")]
    [Tooltip("Si lo dejás vacío, se usa el AudioSource de este mismo GameObject")]
    public AudioSource audioSource;

    private float timerPasos;
    private int ultimoClipIndex = -1;

    void Awake()
    {
        if (audioSource == null) audioSource = GetComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.loop = false;
    }

    void Update()
    {
        bool estaCaminando = EstaCaminando();

        if (!estaCaminando)
        {
            timerPasos = 0f;
            return;
        }

        timerPasos -= Time.deltaTime;
        if (timerPasos <= 0f)
        {
            ReproducirPasoAleatorio();
            timerPasos = ObtenerIntervaloActual();
        }
    }

    bool EstaCaminando()
    {
        // Detectamos si hay intención de movimiento usando los mismos ejes que tu script Movimiento.cs
        float inputVertical = Input.GetAxisRaw("Vertical");
        float inputHorizontal = Input.GetAxisRaw("Horizontal");

        // Si cualquiera de los dos ejes no es cero, el jugador se está moviendo.
        return Mathf.Abs(inputVertical) > 0.1f || Mathf.Abs(inputHorizontal) > 0.1f;
    }

    float ObtenerIntervaloActual()
    {
        // Si tienes pensado agregar la función de correr, por defecto dejé que se active con Left Shift.
        bool corriendo = Input.GetKey(KeyCode.LeftShift);
        return corriendo ? intervaloCorrer : intervaloCaminar;
    }

    void ReproducirPasoAleatorio()
    {
        if (clipsDePasos == null || clipsDePasos.Length == 0) return;

        int index = ElegirIndiceSinRepetir();
        AudioClip clip = clipsDePasos[index];
        if (clip == null) return;

        audioSource.pitch = 1f + Random.Range(-variacionPitch, variacionPitch);
        float volumen = Mathf.Clamp01(volumenBase + Random.Range(-variacionVolumen, variacionVolumen));

        audioSource.PlayOneShot(clip, volumen);

        ultimoClipIndex = index;
    }

    int ElegirIndiceSinRepetir()
    {
        // Si solo hay un clip, devolvemos el índice 0 para evitar loops infinitos
        if (clipsDePasos.Length <= 1) return 0;

        int index;
        do
        {
            index = Random.Range(0, clipsDePasos.Length);
        }
        while (index == ultimoClipIndex);

        return index;
    }
}