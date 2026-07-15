using UnityEngine;

public class ControladorPuzzle : MonoBehaviour
{
    [Header("Configuración")]
    [Tooltip("Arrastrá acá los 4 interruptores EN EL ORDEN EXACTO en el que hay que prenderlos")]
    public InterruptorPuzzle[] ordenCorrecto;

    [Tooltip("El cofre que se va a abrir al ganar")]
    public Cofre cofreAsignado;

    private int indiceActual = 0;

    public void RecibirInputLuz(InterruptorPuzzle interruptorTocado)
    {
        // Revisamos si el botón que tocó es el que seguía en el orden
        if (ordenCorrecto[indiceActual] == interruptorTocado)
        {
            indiceActual++;

            // ¿Llegamos al final de la secuencia? ¡Ganó!
            if (indiceActual >= ordenCorrecto.Length)
            {
                if (cofreAsignado != null) cofreAsignado.AbrirCofre();

                foreach (var luz in ordenCorrecto)
                {
                    luz.BloquearFinal();
                }
            }
        }
        else
        {
            // Se equivocó de orden. Reseteamos todo a cero.
            indiceActual = 0;
            foreach (var luz in ordenCorrecto)
            {
                luz.Apagar();
            }
        }
    }
}