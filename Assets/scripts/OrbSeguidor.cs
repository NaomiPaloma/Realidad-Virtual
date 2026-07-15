using UnityEngine;

/// <summary>
/// Colocar este script en el GameObject del orbe.
///
/// El orbe se queda quieto (en reposo) hasta que el jugador presiona
/// la tecla de activación (E por defecto) estando cerca de algo
/// interactuable. Recién ahí el orbe empieza a seguirlo, y cambia a
/// un material con emisión (brillo). Si el jugador sale de la
/// habitación asignada, el orbe deja de seguirlo, vuelve a su punto
/// de reposo y recupera el material normal (hace falta volver a
/// presionar la tecla para reactivarlo).
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

    [Header("Tecla de activación")]
    [Tooltip("Tecla que el jugador presiona para activar el seguimiento del orbe")]
    public KeyCode teclaActivar = KeyCode.E;

    private Renderer orbRenderer;
    private bool siguiendoActualmente;
    private bool activadoPorJugador;
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

        // Se activa al presionar la tecla mientras hay algo interactuable enfrente
        if (Input.GetKeyDown(teclaActivar) && hayAlgoInteractuable && dentroDeLaHabitacion)
        {
            activadoPorJugador = true;
        }

        // Si el jugador sale de la habitación, se reinicia (necesita volver a presionar la tecla)
        if (!dentroDeLaHabitacion)
        {
            activadoPorJugador = false;
        }

        bool debeSeguir = activadoPorJugador && dentroDeLaHabitacion;

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