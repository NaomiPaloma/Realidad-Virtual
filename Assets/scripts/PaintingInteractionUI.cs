using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Colocar este script en la cámara del jugador (o en un objeto vacío
/// que sea hijo de la cámara, como el "punto de mira").
///
/// Lanza un Raycast hacia adelante. Si el rayo golpea un objeto con
/// componente PaintingInfo dentro de la distancia máxima, muestra un
/// panel en la parte inferior de la pantalla con el título y la historia
/// del cuadro. Si el jugador se aleja o deja de mirarlo, el panel se oculta.
/// </summary>
public class PaintingInteractionUI : MonoBehaviour
{
    [Header("Referencias")]
    [Tooltip("Cámara del jugador. Si se deja vacío, usa Camera.main")]
    public Camera playerCamera;

    [Tooltip("Panel (GameObject) que contiene el fondo + textos, ubicado abajo de la pantalla")]
    public GameObject panelInfo;

    [Tooltip("Texto UI para el título del cuadro")]
    public Text textoTitulo;

    [Tooltip("Texto UI para la historia del cuadro")]
    public Text textoHistoria;

    [Header("Configuración de detección")]
    [Tooltip("Distancia máxima a la que el jugador puede activar el cuadro")]
    public float distanciaMaxima = 4f;

    [Tooltip("Capa(s) en la que están los cuadros (opcional pero recomendado)")]
    public LayerMask capaCuadros = ~0; // por defecto detecta todas las capas

    [Header("Animación (opcional)")]
    [Tooltip("Velocidad de aparición/desaparición del panel (0 = instantáneo)")]
    public float velocidadFade = 6f;

    private CanvasGroup canvasGroup;
    private PaintingInfo cuadroActual;
    private bool debeMostrarse;

    void Awake()
    {
        if (playerCamera == null)
            playerCamera = Camera.main;

        if (panelInfo != null)
        {
            canvasGroup = panelInfo.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
                canvasGroup = panelInfo.AddComponent<CanvasGroup>();

            canvasGroup.alpha = 0f;
            panelInfo.SetActive(true); // lo dejamos activo y controlamos con alpha
        }
    }

    void Update()
    {
        DetectarCuadro();
        ActualizarFade();
    }

    void DetectarCuadro()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, distanciaMaxima, capaCuadros))
        {
            PaintingInfo info = hit.collider.GetComponentInParent<PaintingInfo>();

            if (info != null)
            {
                debeMostrarse = true;

                if (info != cuadroActual)
                {
                    cuadroActual = info;
                    ActualizarTextos(info);
                }
                return;
            }
        }

        debeMostrarse = false;
        cuadroActual = null;
    }

    void ActualizarTextos(PaintingInfo info)
    {
        if (textoTitulo != null) textoTitulo.text = info.titulo;
        if (textoHistoria != null) textoHistoria.text = info.historia;
    }

    void ActualizarFade()
    {
        if (canvasGroup == null) return;

        float objetivo = debeMostrarse ? 1f : 0f;

        if (velocidadFade <= 0f)
        {
            canvasGroup.alpha = objetivo;
        }
        else
        {
            canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, objetivo, velocidadFade * Time.deltaTime);
        }

        // Desactiva la interacción del panel cuando está invisible (opcional)
        canvasGroup.interactable = debeMostrarse;
        canvasGroup.blocksRaycasts = debeMostrarse;
    }

    // Dibuja el rayo en el editor para debug
    void OnDrawGizmosSelected()
    {
        if (playerCamera == null) return;
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(playerCamera.transform.position,
            playerCamera.transform.position + playerCamera.transform.forward * distanciaMaxima);
    }
}
