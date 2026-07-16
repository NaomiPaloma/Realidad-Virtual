using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PaintingInteractionUI : MonoBehaviour
{
    public static PaintingInteractionUI Instancia;

    [Header("Modo Texto (Mona Lisa / Flamenco)")]
    public GameObject panelInfo;
    public TMP_Text textoTitulo;
    public TMP_Text textoHistoria;

    [Header("Modo Imagen (Cartas y Pergaminos)")]
    public GameObject panelImagen;
    public Image componenteImagen;

    [Header("Sistema de Audio")]
    [Tooltip("El reproductor de sonido de la UI")]
    public AudioSource audioSourceUI;

    private PaintingInfo cuadroActual;

    void Awake()
    {
        Instancia = this;
        if (panelInfo != null) panelInfo.SetActive(false);
        if (panelImagen != null) panelImagen.SetActive(false);
    }

    public void AlternarCuadro(PaintingInfo info)
    {
        if (EstaLeyendo() && cuadroActual == info)
        {
            CerrarPanel();
        }
        else
        {
            CerrarPanel();
            cuadroActual = info;

            // REPRODUCIR SONIDO UNA VEZ
            if (info.sonidoAlAbrir != null && audioSourceUI != null)
            {
                audioSourceUI.PlayOneShot(info.sonidoAlAbrir);
            }

            // Modo Imagen
            if (info.imagenDocumento != null)
            {
                if (componenteImagen != null) componenteImagen.sprite = info.imagenDocumento;
                if (panelImagen != null) panelImagen.SetActive(true);
            }
            // Modo Texto
            else
            {
                if (textoTitulo != null) textoTitulo.text = info.titulo;
                if (textoHistoria != null) textoHistoria.text = info.historia;
                if (panelInfo != null) panelInfo.SetActive(true);
            }
        }
    }

    public void CerrarPanel()
    {
        if (panelInfo != null) panelInfo.SetActive(false);
        if (panelImagen != null) panelImagen.SetActive(false);
        cuadroActual = null;
    }

    public bool EstaLeyendo()
    {
        bool textoAbierto = panelInfo != null && panelInfo.activeSelf;
        bool imagenAbierta = panelImagen != null && panelImagen.activeSelf;
        return textoAbierto || imagenAbierta;
    }
}