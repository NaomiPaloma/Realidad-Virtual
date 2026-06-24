using UnityEngine;

public class SistemaAgarre : MonoBehaviour
{
    public Camera camara;
    public Transform holdPoint;
    public float distancia = 3f;

    Agarrable objetoActual = null;
    FirstPlayerCamera camaraScript;

    void Start()
    {
        camaraScript = camara.GetComponent<FirstPlayerCamera>();
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
            Agarrable obj = hit.collider.GetComponent<Agarrable>();
            if (obj != null)
            {
                objetoActual = obj;
                objetoActual.Agarrar(holdPoint);
                camaraScript?.SetSosteniendoObjeto(true); // avisa que agarro algo
            }
        }
    }

    void Soltar()
    {
        Vector3 posicionSuelta = camara.transform.position + camara.transform.forward * 0.8f;
        objetoActual.Soltar(posicionSuelta);
        objetoActual = null;
        camaraScript?.SetSosteniendoObjeto(false); // avisa que solto
    }
}