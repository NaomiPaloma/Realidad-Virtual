using UnityEngine;

public class InteraccionTrono : MonoBehaviour
{
    public void TerminarJuego()
    {
        Debug.Log("¡El jugador interactuó con el trono! Cerrando el juego...");

        // Esta línea cierra el juego en la Build final (.exe) que le des a los profes
        Application.Quit();

        // Esta instrucción especial frena el modo "Play" solo adentro del Editor de Unity
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}