using UnityEngine;
using System.Collections;
// El loro reacciona cuando el jugador esta dentro de su Box Collider
// (Is Trigger) y presiona una tecla para interactuar.
//
// SETUP EN UNITY:
// 1. Arrastrar este script al prefab del loro.
// 2. El loro necesita un Box Collider con "Is Trigger" tildado.
// 3. El jugador necesita:
//    - Un Collider (cualquier tipo)
//    - Un Rigidbody (puede ser Is Kinematic = true)
//    - El Tag "Player" puesto en el Inspector
// 4. Asignar el AudioClip en "sonidoLoro".
// 5. Escribir la frase en "frase".
// 6. (Opcional) Asignar un Text/TextMeshProUGUI en "textoFrase" para que
//    la frase se vea en pantalla.
// 7. (Opcional - Outline) Crear un objeto hijo del loro que sea una copia
//    de su modelo, un poco mas grande (ej: escala 1.05), con un material
//    de color solido (blanco o amarillo) y SIN sombras/collider.
//    Arrastrar ese objeto hijo al campo "outline". Empieza desactivado
//    y se prende solo cuando el jugador entra en el trigger.
// 8. (Opcional - Prompt) Asignar un Text/TextMeshProUGUI en "textoInteractuar"
//    con el mensaje "Interactuar con la E". Se muestra al entrar al trigger
//    y se oculta al salir o al interactuar.
[RequireComponent(typeof(AudioSource))]
public class ParrotInteraction : MonoBehaviour
{
    [Header("Contenido")]
    public AudioClip sonidoLoro;
    [TextArea]
    public string frase = "Polly quiere una galleta!";

    [Header("Interaccion")]
    public KeyCode teclaInteraccion = KeyCode.E;

    [Header("UI opcional - Frase")]
    [Tooltip("Objeto de texto (Text o TextMeshProUGUI) donde se muestra la frase")]
    public GameObject textoFrase;
    public float duracionFrase = 3f;

    [Header("UI opcional - Prompt de interaccion")]
    [Tooltip("Objeto de texto que dice, por ejemplo, 'Interactuar con la E'")]
    public GameObject textoInteractuar;
    [TextArea]
    public string mensajeInteraccion = "Interactuar con la E";

    [Header("Outline opcional")]
    [Tooltip("Objeto hijo (silueta un poco mas grande) que se prende cuando el jugador esta cerca")]
    public GameObject outline;

    private AudioSource audioSource;
    private bool jugadorDentro = false;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (textoFrase != null)
            textoFrase.SetActive(false);

        if (textoInteractuar != null)
        {
            AsignarTexto(textoInteractuar, mensajeInteraccion);
            textoInteractuar.SetActive(false);
        }

        if (outline != null)
            outline.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        jugadorDentro = true;

        if (outline != null)
            outline.SetActive(true);

        if (textoInteractuar != null)
            textoInteractuar.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        jugadorDentro = false;

        if (outline != null)
            outline.SetActive(false);

        if (textoInteractuar != null)
            textoInteractuar.SetActive(false);
    }

    void Update()
    {
        if (jugadorDentro && Input.GetKeyDown(teclaInteraccion))
        {
            Interactuar();
        }
    }

    private void Interactuar()
    {
        // Sonido
        if (sonidoLoro != null && audioSource != null)
        {
            audioSource.PlayOneShot(sonidoLoro);
        }

        // Frase en consola
        Debug.Log("[Loro dice]: " + frase);

        // Ocultamos el prompt de "Interactuar con la E" mientras habla
        if (textoInteractuar != null)
            textoInteractuar.SetActive(false);

        // Frase en pantalla (si hay UI asignada)
        if (textoFrase != null)
        {
            StopAllCoroutines();
            StartCoroutine(MostrarFrase());
        }
    }

    private IEnumerator MostrarFrase()
    {
        AsignarTexto(textoFrase, frase);
        textoFrase.SetActive(true);
        yield return new WaitForSeconds(duracionFrase);
        textoFrase.SetActive(false);

        // Volvemos a mostrar el prompt si el jugador sigue dentro del trigger
        if (jugadorDentro && textoInteractuar != null)
            textoInteractuar.SetActive(true);
    }

    // Metodo auxiliar para no repetir la logica de TMP vs UI.Text
    private void AsignarTexto(GameObject objetoTexto, string contenido)
    {
        if (objetoTexto == null) return;

        var tmp = objetoTexto.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        if (tmp != null)
        {
            tmp.text = contenido;
            return;
        }

        var textoUI = objetoTexto.GetComponentInChildren<UnityEngine.UI.Text>();
        if (textoUI != null) textoUI.text = contenido;
    }
} 