using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteraccionLuz : MonoBehaviour
{

    public Light luz;
    bool enRangoLuz = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (enRangoLuz && Input.GetKeyDown(KeyCode.F))
        {
            luz.enabled = !luz.enabled;
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
