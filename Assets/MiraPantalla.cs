using UnityEngine;

public class MiraPantalla : MonoBehaviour
{
    [SerializeField] Texture2D texturaMira;
    [SerializeField] float tamañoMira = 30f;

    void OnGUI()
    {
        // Calcula el centro exacto de la pantalla
        float xCentro = (Screen.width - tamañoMira) / 2;
        float yCentro = (Screen.height - tamañoMira) / 2;

        // Dibuja la mira en el centro
        if (texturaMira != null)
        {
            GUI.DrawTexture(new Rect(xCentro, yCentro, tamañoMira, tamañoMira), texturaMira);
        }
        else
        {
            GUI.Box(new Rect(xCentro, yCentro, tamañoMira, tamañoMira), "");
        }
    }
} 