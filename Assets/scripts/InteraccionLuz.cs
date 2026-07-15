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

    [Header("Animación de Perilla")]
    public Transform perillaInterruptor; 
    public Vector3 rotacionApagado = new Vector3(-105f, 0f, 0f); 
    public Vector3 rotacionEncendido = new Vector3(-75f, 0f, 0f); 

    [Header("Emisivo del Farol")]
    public Renderer farolRenderer; // Arrastrá el modelo 3D del farol acá
    public int indiceMaterial = 0; // Dejalo en 0 si el farol tiene un solo material
    
    // [ColorUsage(true, true)] permite elegir colores HDR (que tienen intensidad de brillo)
    [ColorUsage(true, true)] public Color colorEmisionPrendido = Color.white; 
    [ColorUsage(true, true)] public Color colorEmisionApagado = Color.black; 

    AudioSource audioSource;
    Material materialFarol;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (perillaInterruptor != null)
        {
            perillaInterruptor.localEulerAngles = rotacionApagado;
        }

        // Clonamos el material del farol para poder modificarlo sin afectar al resto del juego
        if (farolRenderer != null)
        {
            materialFarol = farolRenderer.materials[indiceMaterial];
            materialFarol.EnableKeyword("_EMISSION"); // Nos aseguramos de que la emisión esté activada
            materialFarol.SetColor("_EmissionColor", colorEmisionApagado); // Arranca apagado
        }
    }

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

        bool nuevoEstado = !algunaEncendida;
        foreach (Light luz in luces)
        {
            if (luz != null)
                luz.enabled = nuevoEstado;
        }

        foreach (ObjetoFlotante obj in objetosQueFlotan)
        {
            if (obj != null)
            {
                obj.CambiarEstadoFlotacion(nuevoEstado);
            }
        }

        ReproducirSonido(nuevoEstado);

        if (perillaInterruptor != null)
        {
            perillaInterruptor.localEulerAngles = nuevoEstado ? rotacionEncendido : rotacionApagado;
        }

        // --- NUEVO: Cambiar el emisivo del farol ---
        if (materialFarol != null)
        {
            Color colorDestino = nuevoEstado ? colorEmisionPrendido : colorEmisionApagado;
            materialFarol.SetColor("_EmissionColor", colorDestino);
        }
    }

    void ReproducirSonido(bool encendiendo)
    {
        AudioClip clip = encendiendo ? sonidoEncendido : sonidoApagado;

        if (audioSource != null && clip != null)
            audioSource.PlayOneShot(clip);
    }
}