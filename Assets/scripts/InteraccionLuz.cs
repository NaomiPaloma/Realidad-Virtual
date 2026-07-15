using UnityEngine;

public class InteraccionLuz : MonoBehaviour
{
    [Header("Luces del cuarto")]
    public Light[] luces = new Light[6]; //[cite: 8]

    [Header("Objetos Mágicos")]
    public ObjetoFlotante[] objetosQueFlotan; // Lista de los objetos que van a levitar

    // Ahora es public para que el SistemaAgarre lo pueda ejecutar al mirarlo[cite: 8]
    public void ToggleTodasLasLuces()
    {
        bool algunaEncendida = false; //[cite: 8]

        foreach (Light luz in luces) //[cite: 8]
        {
            if (luz != null && luz.enabled) //[cite: 8]
            {
                algunaEncendida = true; //[cite: 8]
                break; //[cite: 8]
            }
        }

        // Determinamos el nuevo estado (si había alguna prendida, las apagamos. Si no, las prendemos)
        bool nuevoEstado = !algunaEncendida;

        foreach (Light luz in luces) //[cite: 8]
        {
            if (luz != null) //[cite: 8]
                luz.enabled = nuevoEstado; //[cite: 8]
        }

        // Le avisamos a todos los objetos mágicos que empiecen a flotar (o dejen de hacerlo)
        foreach (ObjetoFlotante obj in objetosQueFlotan)
        {
            if (obj != null)
            {
                obj.CambiarEstadoFlotacion(nuevoEstado);
            }
        }
    }
}