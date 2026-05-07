using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrabSystem : MonoBehaviour
{
    [Header("Configuración de Interacción")]
    [Tooltip("Punto en la mano del personaje donde se sostendrá el objeto")]
    public Transform holdPoint;

    [Tooltip("Cámara principal para lanzar el rayo de interacción")]
    public Camera playerCamera;

    [Tooltip("Distancia máxima de agarre")]
    public float grabDistance = 3f;

    [Tooltip("Capa (Layer) donde están los objetos interactuables")]
    public LayerMask interactableLayer;

    private PickableObject currentlyHeldObject;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentlyHeldObject == null)
            {
                TryPickUpObject();
            }
            else
            {
                DropObject();
            }
        }
    }

    private void TryPickUpObject()
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, grabDistance, interactableLayer))
        {
            PickableObject pickable = hit.collider.GetComponent<PickableObject>();

            if (pickable != null)
            {
                Grab(pickable);
            }
        }
    }

    private void Grab(PickableObject objectToGrab)
    {
        currentlyHeldObject = objectToGrab;

        currentlyHeldObject.OnPickedUp();

        currentlyHeldObject.transform.position = holdPoint.position;
        currentlyHeldObject.transform.rotation = holdPoint.rotation;

        currentlyHeldObject.transform.SetParent(holdPoint);

        Debug.Log($"Objeto agarrado: {currentlyHeldObject.name}");
    }

    private void DropObject()
    {
        Debug.Log($"Objeto soltado: {currentlyHeldObject.name}");

        currentlyHeldObject.transform.SetParent(null); 

        currentlyHeldObject.OnDropped();

        currentlyHeldObject = null;
    }
}

