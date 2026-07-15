using UnityEngine;

public class Agarrable : MonoBehaviour
{
    [Header("Identificación")]
    public string colorPeso = "Rojo"; // debe escribirse igual que en la baldosa correspondiente

    Rigidbody rb;
    Collider col;
    Transform holdPoint;
    bool agarrado = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    void Update()
    {
        if (agarrado && holdPoint != null)
        {
            transform.position = holdPoint.position;
            transform.rotation = holdPoint.rotation;
        }
    }

    public bool EstaAgarrado()
    {
        return agarrado;
    }

    public void Agarrar(Transform punto)
    {
        holdPoint = punto;
        agarrado = true;

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;

        Physics.IgnoreLayerCollision(
            LayerMask.NameToLayer("Objetos"),
            LayerMask.NameToLayer("Player"),
            true
        );

        Physics.IgnoreLayerCollision(
            LayerMask.NameToLayer("Objetos"),
            LayerMask.NameToLayer("Baldosas"),
            true
        );
    }

    public void Soltar(Vector3 posicionSuelta)
    {
        agarrado = false;
        holdPoint = null;

        transform.position = posicionSuelta;

        rb.isKinematic = false;

        Physics.IgnoreLayerCollision(
            LayerMask.NameToLayer("Objetos"),
            LayerMask.NameToLayer("Player"),
            false
        );

        Physics.IgnoreLayerCollision(
            LayerMask.NameToLayer("Objetos"),
            LayerMask.NameToLayer("Baldosas"),
            false
        );
    }

    // NUEVO: Para bloquearlo dentro del cofre
    // NUEVO: Para bloquearlo dentro del cofre
    public void BloquearInteraccion(bool bloquear)
    {
        if (col == null) col = GetComponent<Collider>();
        if (rb == null) rb = GetComponent<Rigidbody>(); // Buscamos las físicas

        if (col != null) col.enabled = !bloquear; // Apaga o prende las colisiones

        // ¡LA CLAVE! Si está bloqueado, activamos isKinematic para que no se caiga por el piso
        if (rb != null) rb.isKinematic = bloquear;

        // Apaga el Outline si lo tiene para que no brille a través del cofre
        Outline outline = GetComponent<Outline>();
        if (outline != null) outline.enabled = !bloquear;
    }
}