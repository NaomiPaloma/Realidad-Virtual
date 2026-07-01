using UnityEngine;

public class BaldosaColor : MonoBehaviour
{
    [Header("Referencias")]
    public Puerta puerta;

    [Header("Color que activa esta baldosa")]
    public string colorBaldosa = "Rojo"; // debe coincidir con el colorPeso del cubo

    bool jugadorAdentro = false;
    bool objetoAdentro = false;

    void OnTriggerEnter(Collider other)
    {
        // el personaje siempre activa la baldosa, sin importar el color
        if (other.CompareTag("Player"))
        {
            jugadorAdentro = true;
            puerta.AbrirPuerta();
            return;
        }

        // un cubo solo activa si coincide el color y no está siendo sostenido
        if (other.CompareTag("Peso"))
        {
            Agarrable agarrable = other.GetComponent<Agarrable>();
            if (agarrable != null && !agarrable.EstaAgarrado() && ColorCoincide(agarrable.colorPeso))
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

            if (!objetoAdentro)
                puerta.CerrarPuerta();

            return;
        }

        if (other.CompareTag("Peso"))
        {
            Agarrable agarrable = other.GetComponent<Agarrable>();
            if (agarrable != null && ColorCoincide(agarrable.colorPeso))
            {
                objetoAdentro = false;

                if (!jugadorAdentro)
                    puerta.CerrarPuerta();
            }
        }
    }

    // compara ignorando mayúsculas/minúsculas y espacios de más, para evitar errores de tipeo tontos
    bool ColorCoincide(string colorCubo)
    {
        return string.Equals(colorBaldosa.Trim(), colorCubo.Trim(), System.StringComparison.OrdinalIgnoreCase);
    }
}