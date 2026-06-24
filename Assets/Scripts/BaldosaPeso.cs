using UnityEngine;

public class BaldosaPeso : MonoBehaviour
{
    public Puerta puerta;
    bool jugadorAdentro = false;
    bool objetoAdentro = false;

    void Start()
    {

    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorAdentro = true;
            puerta.AbrirPuerta();
        }

        if (other.CompareTag("Peso"))
        {
            // solo activa si el objeto no esta siendo sostenido
            Agarrable agarrable = other.GetComponent<Agarrable>();
            if (agarrable != null && !agarrable.EstaAgarrado())
            {
                objetoAdentro = true;
                puerta.AbrirPuerta();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorAdentro = false;

            // solo cierra si el objeto tampoco está
            if (!objetoAdentro)
                puerta.CerrarPuerta();
        }

        if (other.CompareTag("Peso"))
        {
            objetoAdentro = false;

            // solo cierra si el jugador tampoco está
            if (!jugadorAdentro)
                puerta.CerrarPuerta();
        }
    }
}