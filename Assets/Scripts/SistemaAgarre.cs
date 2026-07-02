using UnityEngine;

public class SistemaAgarre : MonoBehaviour
{
    public Camera camara;
    public Transform holdPoint;
    public float distancia = 3f;

    Agarrable objetoActual = null;
    FirstPlayerCamera camaraScript;
    FeedbackInteraccion feedbackScript;

    void Start()
    {
        camaraScript = camara.GetComponent<FirstPlayerCamera>();
        feedbackScript = camara.GetComponent<FeedbackInteraccion>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (objetoActual == null)
                IntentarAgarrar();
            else
                Soltar();
        }
    }

    void IntentarAgarrar()
    {
        RaycastHit hit;
        Ray rayo = camara.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if (Physics.Raycast(rayo, out hit, distancia))
        {
            // 1. Primero nos fijamos si es un Cubo (Agarrable)
            Agarrable obj = hit.collider.GetComponent<Agarrable>();
            if (obj != null)
            {
                objetoActual = obj;
                objetoActual.Agarrar(holdPoint);
                camaraScript?.SetSosteniendoObjeto(true); // avisa a la cámara

                // Le avisamos a la UI que ya tenemos el objeto en la mano
                if (feedbackScript != null) feedbackScript.estaSosteniendoObjeto = true;

                return; // Cortamos acá así no busca el interruptor
            }

            // 2. Si no era un cubo, nos fijamos si es tu interruptor de luces
            InteraccionLuz interruptor = hit.collider.GetComponent<InteraccionLuz>();
            if (interruptor != null)
            {
                interruptor.ToggleTodasLasLuces();
                // Acá no cambiamos la variable "estaSosteniendoObjeto" porque solo tocamos un botón
            }
        }
    }

    void Soltar()
    {
        Vector3 posicionSuelta = camara.transform.position + camara.transform.forward * 0.8f;
        objetoActual.Soltar(posicionSuelta);
        objetoActual = null;
        camaraScript?.SetSosteniendoObjeto(false); // avisa a la cámara que soltó

        // Le avisamos a la UI que soltamos el objeto
        if (feedbackScript != null) feedbackScript.estaSosteniendoObjeto = false;
    }
}