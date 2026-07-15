using UnityEngine;

public class InterruptorPuzzle : MonoBehaviour
{
    [Header("Referencias")]
    public Light luzAsociada; // La lucecita que prende este botón
    public ControladorPuzzle controlador; // El cerebro del puzzle

    private bool encendido = false;
    private bool bloqueado = false;

    void Start()
    {
        Apagar(); // Nos aseguramos que arranquen apagadas
    }

    // El jugador llama a esto con la letra 'E'
    public void Interactuar()
    {
        if (bloqueado || encendido) return; // Si ya está prendido, ignoramos el click

        encendido = true;
        if (luzAsociada != null) luzAsociada.enabled = true;

        // Le avisa al cerebro central que tocaron este botón
        if (controlador != null) controlador.RecibirInputLuz(this);
    }

    public void Apagar()
    {
        encendido = false;
        if (luzAsociada != null) luzAsociada.enabled = false;
    }

    public void BloquearFinal()
    {
        bloqueado = true; // Para que no jueguen con las luces una vez ganado
    }
}