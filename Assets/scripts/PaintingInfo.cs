using UnityEngine;

public class PaintingInfo : MonoBehaviour
{
    [Header("Modo Texto (Cuadros y Flamenco)")]
    public string titulo;
    [TextArea(4, 12)] public string historia;

    [Header("Modo Imagen (Cartas y Notas)")]
    [Tooltip("Si pones un Sprite acá, el juego ignorará los textos y mostrará esta imagen a pantalla completa.")]
    public Sprite imagenDocumento;
    

    [Header("Audio Opcional")]
    [Tooltip("Sonido que se reproduce una vez al abrir este panel/imagen.")]
    public AudioClip sonidoAlAbrir;
}