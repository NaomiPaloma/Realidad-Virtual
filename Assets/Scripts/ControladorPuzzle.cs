using UnityEngine;

public class ControladorPuzzle : MonoBehaviour
{
    public InterruptorPuzzle[] ordenCorrecto;
    public Cofre cofreAsignado;
    private int indiceActual = 0;

    public void RecibirInputLuz(InterruptorPuzzle interruptorTocado, bool estaEncendiendo)
    {
        // Si el jugador apaga una luz, no hacemos nada, dejamos que el interruptor la apague
        if (!estaEncendiendo) return;

        // Si está encendiendo, verificamos el orden
        if (ordenCorrecto[indiceActual] == interruptorTocado)
        {
            indiceActual++;
            if (indiceActual >= ordenCorrecto.Length)
            {
                if (cofreAsignado != null) cofreAsignado.AbrirCofre();
                foreach (var luz in ordenCorrecto) luz.BloquearFinal();
            }
        }
        else
        {
            // Opcional: si querés que se resetee al fallar, descomentá esto:
            // indiceActual = 0; 
            // foreach (var luz in ordenCorrecto) luz.Apagar();
        }
    }
}