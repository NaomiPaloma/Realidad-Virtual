using UnityEngine;

public class Cofre : MonoBehaviour
{
    [Header("Partes del Cofre")]
    public Transform tapa;
    public float anguloAbierto = -90f;
    public float velocidadApertura = 5f;

    [Header("Contenido del Cofre")]
    [Tooltip("Arrastrá acá el cubo que está adentro del cofre")]
    public Agarrable cuboAdentro;

    private bool estaAbierto = false;
    private Quaternion rotacionCerrada;
    private Quaternion rotacionAbierta;

    void Start()
    {
        if (tapa != null)
        {
            rotacionCerrada = tapa.localRotation;
            rotacionAbierta = Quaternion.Euler(anguloAbierto, tapa.localEulerAngles.y, tapa.localEulerAngles.z);
        }

        // Bloqueamos el cubo al iniciar para que no se pueda interactuar
        if (cuboAdentro != null)
        {
            cuboAdentro.BloquearInteraccion(true);
        }
    }

    void Update()
    {
        if (tapa != null)
        {
            Quaternion rotacionObjetivo = estaAbierto ? rotacionAbierta : rotacionCerrada;
            tapa.localRotation = Quaternion.Lerp(tapa.localRotation, rotacionObjetivo, Time.deltaTime * velocidadApertura);
        }
    }

    // Esta función ya no la llama el jugador con la 'E', la llama el Puzzle
    public void AbrirCofre()
    {
        estaAbierto = true;

        // Habilitamos el cubo para que vuelva a ser interactivo
        if (cuboAdentro != null)
        {
            cuboAdentro.BloquearInteraccion(false);
        }
    }
}