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
        //transform.position = new Vector3(0,0,5);
        //transform.position += new Vector3(0, 0, 5);


        //Posicion += dirección * velocidad * fixeo de tiempo entre dispositivos

        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.forward * velocidad * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.back * velocidad * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * velocidad * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * velocidad * Time.deltaTime;
        }

       
    }
}
