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
//    la frase se vea en pantalla. Si no se asigna nada, la frase igual
//    aparece en la consola (Window > General > Console).

[RequireComponent(typeof(AudioSource))]
public class ParrotInteraction : MonoBehaviour
{
    [Header("Contenido")]
    public AudioClip sonidoLoro;

    [TextArea]
    public string frase = "Polly quiere una galleta!";

    [Header("Interaccion")]
    public KeyCode teclaInteraccion = KeyCode.E;

    [Header("UI opcional")]
    [Tooltip("Objeto de texto (Text o TextMeshProUGUI) donde se muestra la frase")]
    public GameObject textoFrase;
    public float duracionFrase = 3f;

    private AudioSource audioSource;
    private bool jugadorDentro = false;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (textoFrase != null)
            textoFrase.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            jugadorDentro = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            jugadorDentro = false;
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

        // Frase en pantalla (si hay UI asignada)
        if (textoFrase != null)
        {
            StopAllCoroutines();
            StartCoroutine(MostrarFrase());
        }
    }

    private IEnumerator MostrarFrase()
    {
        var tmp = textoFrase.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        if (tmp != null)
        {
            tmp.text = frase;
        }
        else
        {
            var textoUI = textoFrase.GetComponentInChildren<UnityEngine.UI.Text>();
            if (textoUI != null) textoUI.text = frase;
        }

        textoFrase.SetActive(true);
        yield return new WaitForSeconds(duracionFrase);
        textoFrase.SetActive(false);
    }
} 