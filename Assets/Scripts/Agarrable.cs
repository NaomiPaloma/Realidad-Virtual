using UnityEngine;

public class Agarrable : MonoBehaviour
{
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

        // ignora colision con baldosas mientras sostenes el objeto
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

        // reactiva colision con baldosas al soltar
        Physics.IgnoreLayerCollision(
            LayerMask.NameToLayer("Objetos"),
            LayerMask.NameToLayer("Baldosas"),
            false
        );
    }
}
