using UnityEngine;

/// <summary>
/// Colocar este script en el GameObject del orbe.
///
/// El orbe detecta por su cuenta (con Raycast, igual que el jugador)
/// si hay algo interactuable cerca. Cuando eso pasa Y el jugador está
/// dentro de la zona de la habitación asignada, el orbe lo empieza a
/// seguir y cambia a un material con emisión (brillo). Si el jugador
/// sale de la habitación, o deja de haber algo para interactuar,
/// el orbe deja de seguirlo y vuelve a su material normal.
///
/// Es completamente independiente de cualquier otro sistema (cuadros, etc).
/// Solo necesita que los objetos interactuables tengan un Collider y
/// estén en la capa (Layer) que le indiques en "Capa Interactuable".
/// </summary>
[RequireComponent(typeof(Renderer))]
public class OrbSeguidor : MonoBehaviour
{
    [Header("Referencias")]
    [Tooltip("Transform del jugador")]
    public Transform jugador;

    [Tooltip("Cámara del jugador, usada para detectar si hay algo interactuable enfrente")]
    public Camera camaraJugador;

    [Tooltip("Collider (marcado como Trigger) que define los límites de la habitación donde el orbe puede moverse")]
    public Collider zonaHabitacion;

    [Header("Detección de interacción")]
    [Tooltip("Distancia máxima para detectar un objeto interactuable")]
    public float distanciaInteraccion = 4f;

    [Tooltip("Capa(s) de los objetos interactuables")]
    public LayerMask capaInteractuable = ~0;

    [Header("Movimiento del orbe")]
    [Tooltip("Velocidad con la que el orbe se mueve hacia el jugador")]
    public float velocidadSeguimiento = 4f;

    [Tooltip("Distancia mínima que el orbe mantiene respecto al jugador")]
    public float distanciaMinima = 1.2f;

    [Tooltip("Altura extra sobre la posición del jugador (para que flote arriba)")]
    public float alturaFlote = 1.6f;

    [Tooltip("Suaviza el giro del orbe hacia la dirección de movimiento (0 = sin rotar)")]
    public float velocidadRotacion = 2f;

    [Header("Materiales")]
    [Tooltip("Material normal del orbe (sin brillo)")]
    public Material materialNormal;

    [Tooltip("Material con emisión, usado cuando el jugador puede interactuar")]
    public Material materialEmision;

    private Renderer orbRenderer;
    private bool siguiendoActualmente;
    private Vector3 puntoDeReposo;

    void Awake()
    {
        orbRenderer = GetComponent<Renderer>();
        puntoDeReposo = transform.position;

        if (materialNormal != null)
            orbRenderer.material = materialNormal;
    }

    void Update()
    {
        bool dentroDeLaHabitacion = zonaHabitacion != null &&
            zonaHabitacion.bounds.Contains(jugador.position);

        bool hayAlgoInteractuable = DetectarInteractuable();

        bool debeSeguir = dentroDeLaHabitacion && hayAlgoInteractuable;

        MoverOrbe(debeSeguir);
        ActualizarMaterial(debeSeguir);
    }

    bool DetectarInteractuable()
    {
        if (camaraJugador == null) return false;

        Ray ray = new Ray(camaraJugador.transform.position, camaraJugador.transform.forward);
        return Physics.Raycast(ray, distanciaInteraccion, capaInteractuable);
    }

    void MoverOrbe(bool debeSeguir)
    {
        Vector3 destino;

        if (debeSeguir)
        {
            // Posición objetivo: cerca del jugador, a cierta altura, sin pegarse demasiado
            Vector3 haciaOrbe = (transform.position - jugador.position);
            haciaOrbe.y = 0f;

            if (haciaOrbe.magnitude < 0.01f)
                haciaOrbe = jugador.forward; // evita división por vector cero

            haciaOrbe = haciaOrbe.normalized * distanciaMinima;

            destino = jugador.position + haciaOrbe + Vector3.up * alturaFlote;
            siguiendoActualmente = true;
        }
        else
        {
            // Vuelve a su punto de reposo dentro de la habitación
            destino = puntoDeReposo;
            siguiendoActualmente = false;
        }

        transform.position = Vector3.Lerp(transform.position, destino, velocidadSeguimiento * Time.deltaTime);

        if (velocidadRotacion > 0f && siguiendoActualmente)
        {
            Vector3 direccion = jugador.position - transform.position;
            if (direccion.sqrMagnitude > 0.001f)
            {
                Quaternion rotacionObjetivo = Quaternion.LookRotation(direccion);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, velocidadRotacion * Time.deltaTime);
            }
        }
    }

    void ActualizarMaterial(bool debeSeguir)
    {
        if (orbRenderer == null) return;

        Material materialDeseado = debeSeguir ? materialEmision : materialNormal;

        if (materialDeseado != null && orbRenderer.sharedMaterial != materialDeseado)
        {
            orbRenderer.material = materialDeseado;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(puntoDeReposo == Vector3.zero ? transform.position : puntoDeReposo, 0.2f);
    }
}
