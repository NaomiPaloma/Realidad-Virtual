using UnityEngine;

public class TronoSalirJuego : MonoBehaviour
{
    public string tagJugador = "Player"; // ya coincide con tu jugador
    private bool jugadorEnTrono = false; // para saber si el jugador está tocando el trono

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger detectado con: " + other.name + " - Tag: " + other.tag);

        if (other.CompareTag(tagJugador))
        {
            jugadorEnTrono = true;
            Debug.Log("Jugador entró al trono. Presiona E para salir del juego.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(tagJugador))
        {
            jugadorEnTrono = false;
            Debug.Log("Jugador salió del trono.");
        }
    }

    private void Update()
    {
        if (jugadorEnTrono && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Tecla E detectada, saliendo del juego...");
            SalirDelJuego();
        }
    }

    private void SalirDelJuego()
    {
        Debug.Log("Tocaste el trono y presionaste E. Saliendo del juego...");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
} 