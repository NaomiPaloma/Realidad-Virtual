using System.Collections;
using UnityEngine;

public class CofreInteractuable : MonoBehaviour
{
    [Header("Rotación de la tapa")]
    public float anguloAbierto = -100f; // grados sobre el eje X (ajustá signo/eje según tu bisagra)
    public float duracionAnimacion = 0.8f;

    [Header("Curva de animación")]
    public AnimationCurve curvaSuavizado = AnimationCurve.EaseInOut(0, 0, 1, 1);

    bool abierto = false;
    Quaternion rotacionCerrada;
    Quaternion rotacionAbierta;
    Coroutine animacionActual;

    void Start()
    {
        rotacionCerrada = transform.localRotation;
        rotacionAbierta = Quaternion.Euler(anguloAbierto, 0f, 0f) * rotacionCerrada;
    }

    // Método público para que tu sistema de interacción (el mismo que usa SistemaAgarre/InteraccionLuz) lo ejecute
    public void ToggleCofre()
    {
        abierto = !abierto;

        if (animacionActual != null)
            StopCoroutine(animacionActual);

        Quaternion objetivo = abierto ? rotacionAbierta : rotacionCerrada;
        animacionActual = StartCoroutine(RotarTapa(objetivo));
    }

    IEnumerator RotarTapa(Quaternion rotacionObjetivo)
    {
        Quaternion rotacionInicial = transform.localRotation;
        float tiempoTranscurrido = 0f;

        while (tiempoTranscurrido < duracionAnimacion)
        {
            tiempoTranscurrido += Time.deltaTime;
            float t = tiempoTranscurrido / duracionAnimacion;
            float tSuavizado = curvaSuavizado.Evaluate(t);

            transform.localRotation = Quaternion.Slerp(rotacionInicial, rotacionObjetivo, tSuavizado);
            yield return null;
        }

        transform.localRotation = rotacionObjetivo;
    }
}
