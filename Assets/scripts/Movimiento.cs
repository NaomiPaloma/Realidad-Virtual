using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    //public MeshRenderer myMesh;
    //public Transform myTransform;
    //public Movimiento yoMismo;

    public float velocidad = 5;
    // Start is called before the first frame update
    void Start()
    {
        //MovimientoDelPj();
    }

    // Update is called once per frame
    void Update()
    {
        MovimientoDelPj();
    }

    void MovimientoDelPj()
    {
        transform.position += (Input.GetAxisRaw("Vertical") * transform.forward + Input.GetAxisRaw("Horizontal") * transform.right).normalized * velocidad * Time.deltaTime;
    }
}
