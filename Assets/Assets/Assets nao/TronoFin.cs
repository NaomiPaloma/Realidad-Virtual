using UnityEngine;

public class TronoSalirJuego : MonoBehaviour
{
    public string tagJugador = "Player"; // ya coincide con tu jugador

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagJugador))
        {
            SalirDelJuego();
        }
    }

    private void SalirDelJuego()
    {
        Debug.Log("Tocaste el trono. Saliendo del juego...");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
} 