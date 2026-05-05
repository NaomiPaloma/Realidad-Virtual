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

    // Estado interno
    private PickableObject currentlyHeldObject;

    private void Update()
    {
        // Esta línea dibujará el rayo en la ventana de Escena (Scene) para que puedas verlo
        Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * grabDistance, Color.red);
        // Detectar Input (Input System clásico para blocking)
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
        // Lanzamos un rayo desde el centro de la vista de la cámara
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        // Si el rayo impacta algo dentro de la distancia y en la capa correcta
        if (Physics.Raycast(ray, out hit, grabDistance, interactableLayer))
        {
            // Intentamos obtener el componente PickableObject
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

        // Llamamos a la lógica interna del objeto (apagar fisicas)
        currentlyHeldObject.OnPickedUp();

        // Lógica de emparentado (Parenting)
        // 1. Movemos el objeto a la posición de la mano
        currentlyHeldObject.transform.position = holdPoint.position;
        currentlyHeldObject.transform.rotation = holdPoint.rotation;

        // 2. Lo hacemos hijo de holdPoint para que se mueva con él
        currentlyHeldObject.transform.SetParent(holdPoint);

        Debug.Log($"Objeto agarrado: {currentlyHeldObject.name}");
    }

    private void DropObject()
    {
        Debug.Log($"Objeto soltado: {currentlyHeldObject.name}");

        // Lógica de desemparentado
        currentlyHeldObject.transform.SetParent(null); // Ya no tiene padre

        // Llamamos a la lógica interna del objeto (encender fisicas)
        currentlyHeldObject.OnDropped();

        // Limpiamos la referencia
        currentlyHeldObject = null;
    }
}

