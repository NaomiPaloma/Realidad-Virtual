using UnityEngine;
using TMPro;

public class PaintingInteractionUI : MonoBehaviour
{
    // Esto crea una referencia global para que FeedbackInteraccion lo encuentre al instante
    public static PaintingInteractionUI Instancia;

    [Header("Referencias UI")]
    [Tooltip("El objeto Padre que contiene el fondo y los textos")]
    public GameObject panelInfo;

    [Tooltip("Texto TMP para el título del cuadro")]
    public TMP_Text textoTitulo;

    [Tooltip("Texto TMP para la historia del cuadro")]
    public TMP_Text textoHistoria;

    private PaintingInfo cuadroActual;

    void Awake()
    {
        Instancia = this; // Se auto-asigna al iniciar

        // Nos aseguramos de que arranque apagado
        if (panelInfo != null) panelInfo.SetActive(false);
    }

    public void AlternarCuadro(PaintingInfo info)
    {
        // Si tocamos la E y el panel ya estaba abierto con ESTE cuadro, lo cerramos
        if (panelInfo != null && panelInfo.activeSelf && cuadroActual == info)
        {
            CerrarPanel();
        }
        else
        {
            // Si estaba cerrado, lo rellenamos con la info y lo abrimos
            cuadroActual = info;
            if (textoTitulo != null) textoTitulo.text = info.titulo;
            if (textoHistoria != null) textoHistoria.text = info.historia;

            if (panelInfo != null) panelInfo.SetActive(true);
        }
    }

    public void CerrarPanel()
    {
        // Solo lo apagamos si está prendido
        if (panelInfo != null && panelInfo.activeSelf)
        {
            panelInfo.SetActive(false);
            cuadroActual = null;
        }
    }
}