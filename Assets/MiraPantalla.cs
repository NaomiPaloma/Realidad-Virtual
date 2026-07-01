using UnityEngine;

public class MiraPantalla : MonoBehaviour
{
    [SerializeField] Texture2D texturaMira;
    [SerializeField] float tamañoMira = 30f;

    void OnGUI()
    {
        // Calcula el centro exacto de la pantalla
        float xCentro = (Screen.width - tamañoMira) / 10;
        float yCentro = (Screen.height - tamañoMira) / 10;

        // Dibuja la mira en el centro
        if (texturaMira != null)
        {
            GUI.DrawTexture(new Rect(xCentro, yCentro, tamañoMira, tamañoMira), texturaMira);
        }
        else
        {
            // Si no le asignaste ninguna imagen en el Inspector, dibuja un cuadradito por defecto
            GUI.Box(new Rect(xCentro, yCentro, tamañoMira, tamañoMira), "");
        }
    }
} 