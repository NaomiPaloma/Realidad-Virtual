using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PickableObject : MonoBehaviour
{
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public Collider coll;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
    }
    

    public void OnPickedUp()
    {
        rb.isKinematic = true; 
        coll.enabled = false;   
    }

    public void OnDropped()
    {
        rb.isKinematic = false; 
        coll.enabled = true;    
    }
}

