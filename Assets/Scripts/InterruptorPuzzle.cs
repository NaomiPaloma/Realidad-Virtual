using UnityEngine;

public class InterruptorPuzzle : MonoBehaviour
{
    public Light luzAsociada;
    public ControladorPuzzle controlador;
    private bool encendido = false;
    private bool bloqueado = false;

    public void Interactuar()
    {
        if (bloqueado) return;

        // Alternar estado SIEMPRE
        encendido = !encendido;
        if (luzAsociada != null) luzAsociada.enabled = encendido;

        // Avisar al controlador
        if (controlador != null) controlador.RecibirInputLuz(this, encendido);
    }

    public void Apagar()
    {
        encendido = false;
        if (luzAsociada != null) luzAsociada.enabled = false;
    }

    public void BloquearFinal()
    {
        bloqueado = true;
    }
}