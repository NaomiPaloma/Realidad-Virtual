using UnityEngine;

/// <summary>
/// Colocar este script en cada cuadro/pintura de la escena.
/// Guarda el título y la historia que se mostrará cuando el jugador lo mire.
/// Requiere que el objeto tenga un Collider (puede ser "Is Trigger" o no,
/// ya que se detecta por Raycast, no por colisión física).
/// </summary>
[DisallowMultipleComponent]
public class PaintingInfo : MonoBehaviour
{
    [Header("Datos del cuadro")]
    [Tooltip("Título del cuadro (se muestra en negrita/arriba del panel)")]
    public string titulo;

    [Tooltip("Texto con la historia/descripción del cuadro")]
    [TextArea(4, 12)]
    public string historia;
}
