using UnityEngine;

public class Cofre : MonoBehaviour
{
    [Header("Partes del Cofre")]
    public Transform tapa; // Arrastrá acá el objeto de la tapa

    [Header("Configuración de Rotación")]
    public float anguloAbierto = -90f; // Ángulo de apertura (puede ser 90 positivo dependiendo de tu modelo 3D)
    public float velocidadApertura = 5f; // Qué tan rápido se abre

    private bool estaAbierto = false;
    private Quaternion rotacionCerrada;
    private Quaternion rotacionAbierta;

    void Start()
    {
        if (tapa != null)
        {
            // Guardamos la rotación inicial como la "cerrada"
            rotacionCerrada = tapa.localRotation;

            // Calculamos la rotación "abierta" modificando solo el eje X (el eje clásico de bisagras)
            rotacionAbierta = Quaternion.Euler(anguloAbierto, tapa.localEulerAngles.y, tapa.localEulerAngles.z);
        }
    }

    void Update()
    {
        if (tapa != null)
        {
            // Elegimos hacia dónde tiene que ir la tapa según su estado
            Quaternion rotacionObjetivo = estaAbierto ? rotacionAbierta : rotacionCerrada;

            // Lerp rota la tapa de a poco hasta llegar al objetivo
            tapa.localRotation = Quaternion.Lerp(tapa.localRotation, rotacionObjetivo, Time.deltaTime * velocidadApertura);
        }
    }

    public void Interactuar()
    {
        estaAbierto = !estaAbierto; // Cambia el estado (si estaba cerrado lo abre, y viceversa)
    }
}
