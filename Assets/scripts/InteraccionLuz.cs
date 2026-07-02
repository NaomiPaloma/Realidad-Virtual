using UnityEngine;

public class InteraccionLuz : MonoBehaviour
{
    [Header("Luces del cuarto")]
    public Light[] luces = new Light[6];

    // Ahora es public para que el SistemaAgarre lo pueda ejecutar al mirarlo
    public void ToggleTodasLasLuces()
    {
        bool algunaEncendida = false;

        foreach (Light luz in luces)
        {
            if (luz != null && luz.enabled)
            {
                algunaEncendida = true;
                break;
            }
        }

        foreach (Light luz in luces)
        {
            if (luz != null)
                luz.enabled = !algunaEncendida;
        }
    }
}