using UnityEngine;
using System.Collections;

// Script para hacer que un loro reaccione al interactuar:
// reproduce un sonido y muestra/dice una frase.
//
// SETUP EN UNITY:
// 1. Arrastrar este script al prefab del loro.
// 2. Asegurarse de que el loro tenga un Collider (marcarlo como
//    "Is Trigger" si se va a usar el modo de cercania + tecla).
// 3. Asignar un AudioClip en el campo "sonidoLoro".
// 4. Escribir la frase deseada en el campo "frase".
// 5. Elegir el modo de interaccion en "modoInteraccion" (Click o CercaniaConTecla).
// 6. (Opcional) Asignar un TextMeshPro / Text en "globoDialogo" si se quiere
//    que la frase se muestre en un globo de dialogo en vez de solo en consola.

[RequireComponent(typeof(AudioSource))]
public class ParrotInteraction : MonoBehaviour
{
    public enum ModoInteraccion { Click, CercaniaConTecla }

    [Header("Contenido")]
    [Tooltip("Sonido que hace el loro (graznido, etc.)")]
    public AudioClip sonidoLoro;

    [Tooltip("Frase que dice el loro")]
    [TextArea]
    public string frase = "Polly quiere una galleta!";

    [Header("Configuracion de interaccion")]
    public ModoInteraccion modoInteraccion = ModoInteraccion.Click;

    [Tooltip("Solo si el modo es CercaniaConTecla: tecla para interactuar")]
    public KeyCode teclaInteraccion = KeyCode.E;

    [Header("UI opcional")]
    [Tooltip("Objeto con un Text o TextMeshProUGUI para mostrar la frase (opcional)")]
    public GameObject globoDialogo;
    public float duracionGlobo = 3f;

    [Header("Animacion opcional")]
    [Tooltip("Nombre del trigger en el Animator para animar al loro al hablar (opcional)")]
    public string triggerAnimacion = "Hablar";

    private AudioSource audioSource;
    private Animator animator;
    private bool jugadorCerca = false;
    private bool interactuando = false;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>(); // puede ser null, esta OK

        if (globoDialogo != null)
            globoDialogo.SetActive(false);
    }

    void Update()
    {
        if (modoInteraccion == ModoInteraccion.CercaniaConTecla &&
            jugadorCerca &&
            Input.GetKeyDown(teclaInteraccion) &&
            !interactuando)
        {
            Interactuar();
        }
    }

    // Se usa si el modo es Click y el loro tiene un Collider (no trigger)
    void OnMouseDown()
    {
        if (modoInteraccion == ModoInteraccion.Click && !interactuando)
        {
            Interactuar();
        }
    }

    // Se usa si el modo es CercaniaConTecla y el Collider esta marcado como Trigger
    void OnTriggerEnter(Collider other)
    {
        if (modoInteraccion == ModoInteraccion.CercaniaConTecla && other.CompareTag("Player"))
        {
            jugadorCerca = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (modoInteraccion == ModoInteraccion.CercaniaConTecla && other.CompareTag("Player"))
        {
            jugadorCerca = false;
        }
    }

    private void Interactuar()
    {
        StartCoroutine(SecuenciaInteraccion());
    }

    private IEnumerator SecuenciaInteraccion()
    {
        interactuando = true;

        // Sonido
        if (sonidoLoro != null && audioSource != null)
        {
            audioSource.PlayOneShot(sonidoLoro);
        }

        // Animacion (si existe Animator y trigger configurado)
        if (animator != null && !string.IsNullOrEmpty(triggerAnimacion))
        {
            animator.SetTrigger(triggerAnimacion);
        }

        // Frase por consola (siempre)
        Debug.Log("[Loro dice]: " + frase);

        // Frase en UI (si hay globo asignado)
        if (globoDialogo != null)
        {
            var texto = globoDialogo.GetComponentInChildren<TMPro.TextMeshProUGUI>();
            if (texto != null)
            {
                texto.text = frase;
            }
            else
            {
                var textoUI = globoDialogo.GetComponentInChildren<UnityEngine.UI.Text>();
                if (textoUI != null) textoUI.text = frase;
            }

            globoDialogo.SetActive(true);
            yield return new WaitForSeconds(duracionGlobo);
            globoDialogo.SetActive(false);
        }

        interactuando = false;
    }
} 