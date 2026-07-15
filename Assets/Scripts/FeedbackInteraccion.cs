using UnityEngine;
using TMPro;

public class FeedbackInteraccion : MonoBehaviour
{
    [Header("Configuración")]
    public float distanciaInteraccion = 3f;

    [Header("Referencias de UI")]
    public TextMeshProUGUI textoInteraccionUI;

    [Header("Textos a Mostrar")]
    public string textoAgarrar = "[E] Interactuar";
    public string textoSoltar = "[E] Soltar";

    private Outline objetoMiradoActual;

    [HideInInspector] public bool estaSosteniendoObjeto = false;

    void Update()
    {
        if (estaSosteniendoObjeto)
        {
            if (textoInteraccionUI != null)
            {
                textoInteraccionUI.text = textoSoltar;
                textoInteraccionUI.gameObject.SetActive(true);
            }

            if (objetoMiradoActual != null)
            {
                OcultarOutline(objetoMiradoActual);
                objetoMiradoActual = null;
            }
            return;
        }

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, distanciaInteraccion))
        {
            Outline outlineEncontrado = hit.collider.GetComponent<Outline>();

            if (outlineEncontrado != null)
            {
                if (objetoMiradoActual != null && objetoMiradoActual != outlineEncontrado)
                {
                    OcultarOutline(objetoMiradoActual);
                }

                MostrarOutline(outlineEncontrado);
                objetoMiradoActual = outlineEncontrado;

                if (textoInteraccionUI != null)
                {
                    textoInteraccionUI.text = textoAgarrar;
                    textoInteraccionUI.gameObject.SetActive(true);
                }

                // --- SISTEMA UNIFICADO DE INTERACCIÓN ---
                if (Input.GetKeyDown(KeyCode.E))
                {
                    // 1. Preguntamos si miramos un Interruptor del Puzzle
                    InterruptorPuzzle interruptor = hit.collider.GetComponent<InterruptorPuzzle>();
                    if (interruptor != null)
                    {
                        interruptor.Interactuar();
                    }

                    // 2. Preguntamos si miramos un Cuervo
                    CuervoSonido cuervoEncontrado = hit.collider.GetComponent<CuervoSonido>();
                    if (cuervoEncontrado != null)
                    {
                        cuervoEncontrado.HacerRuido();
                    }
                }
                // ----------------------------------------

                return;
            }
        }

        ApagarInteraccion();
    }

    void ApagarInteraccion()
    {
        if (objetoMiradoActual != null)
        {
            OcultarOutline(objetoMiradoActual);
            objetoMiradoActual = null;
        }

        if (textoInteraccionUI != null)
        {
            textoInteraccionUI.gameObject.SetActive(false);
        }
    }

    void MostrarOutline(Outline outl)
    {
        Color c = outl.OutlineColor;
        c.a = 1f;
        outl.OutlineColor = c;
    }

    void OcultarOutline(Outline outl)
    {
        Color c = outl.OutlineColor;
        c.a = 0f;
        outl.OutlineColor = c;
    }
}