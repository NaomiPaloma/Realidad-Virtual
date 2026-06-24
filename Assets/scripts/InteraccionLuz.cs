using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteraccionLuz : MonoBehaviour
{
    [Header("Luces del cuarto")]
    public Light[] luces = new Light[6];

    bool enRangoLuz = false;

    void Start()
    {

    }

    void Update()
    {
        if (enRangoLuz && Input.GetKeyDown(KeyCode.E))
        {
            ToggleTodasLasLuces();
        }
    }

    void ToggleTodasLasLuces()
    {
        bool algunaEncendida = false;

        foreach (Light luz in luces)
        {
            if (luz != null && luz.enabled)
            {
                algunaEncendida = true;
                break;
            }
        }

        foreach (Light luz in luces)
        {
            if (luz != null)
                luz.enabled = !algunaEncendida;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            enRangoLuz = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            enRangoLuz = false;
    }
}
