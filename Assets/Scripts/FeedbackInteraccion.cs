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
                    // Chequeamos si el panel del cuadro está abierto en este momento
                    bool leyendoCuadro = PaintingInteractionUI.Instancia != null && PaintingInteractionUI.Instancia.EstaLeyendo();

                    if (leyendoCuadro)
                    {
                        // Si estamos leyendo, apagamos el cartel de [E] Interactuar para que no moleste
                        textoInteraccionUI.gameObject.SetActive(false);
                    }
                    else
                    {
                        // Si el panel está cerrado, mostramos el [E] Interactuar normal
                        textoInteraccionUI.text = textoAgarrar;
                        textoInteraccionUI.gameObject.SetActive(true);
                    }
                }

                // --- SISTEMA UNIFICADO DE INTERACCIÓN ---
                if (Input.GetKeyDown(KeyCode.E))
                {
                    // 1. Preguntamos si miramos un Interruptor del Puzzle
                    InterruptorPuzzle interruptor = hit.collider.GetComponent<InterruptorPuzzle>();
                    if (interruptor != null) interruptor.Interactuar();

                    // 2. Preguntamos si miramos un Cuervo
                    CuervoSonido cuervo = hit.collider.GetComponent<CuervoSonido>();
                    if (cuervo != null) cuervo.HacerRuido();

                    // 3. Preguntamos si miramos un Cuadro (NUEVO)
                    PaintingInfo cuadro = hit.collider.GetComponentInParent<PaintingInfo>();
                    if (cuadro != null && PaintingInteractionUI.Instancia != null)
                    {
                        PaintingInteractionUI.Instancia.AlternarCuadro(cuadro);
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

        // Si dejamos de mirar cualquier cosa interactuable, cerramos el panel del cuadro (NUEVO)
        if (PaintingInteractionUI.Instancia != null)
        {
            PaintingInteractionUI.Instancia.CerrarPanel();
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