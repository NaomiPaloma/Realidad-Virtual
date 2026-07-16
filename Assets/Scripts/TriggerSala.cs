using UnityEngine;

public class TriggerSala : MonoBehaviour
{
    public Light[] luces;
    public ObjetoFlotante[] objetos;
    public Renderer farol;
    public AudioSource audioSource; // El mismo de antes

    public void EncenderEfectos()
    {
        // Lógica para prender luces, emisivos y objetos
        foreach (var luz in luces) luz.enabled = false;
        foreach (var obj in objetos) obj.CambiarEstadoFlotacion(false);
        if (farol) farol.material.SetColor("_EmissionColor", Color.white);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Apagar todo al salir
            foreach (var luz in luces) luz.enabled = true;
            foreach (var obj in objetos) obj.CambiarEstadoFlotacion(true);
            if (farol) farol.material.SetColor("_EmissionColor", Color.black);

            // Detener el sonido
            if (audioSource) audioSource.Stop();
        }
    }
}