using UnityEngine;

public class FirstPlayerCamera : MonoBehaviour
{
    [SerializeField] float _yMaxRotation, _yMinRotation;
    float _yMinRotacionConObjeto = -20f; // limite cuando sostiene algo

    float _mouseY;

    Transform _playersHead;
    bool sosteniendoObjeto = false;

    private void LateUpdate()
    {
        Movement();
    }

    public void SetPlayersHead(Transform playersHead)
    {
        _playersHead = playersHead;
    }

    void Movement()
    {
        transform.position = _playersHead.position;
    }

    public void Rotate(float xAxis, float yAxis)
    {
        _mouseY += yAxis;

        // cambia el limite segun si sostiene o no un objeto
        float limiteAbajo = sosteniendoObjeto ? _yMinRotacionConObjeto : _yMinRotation;

        _mouseY = Mathf.Clamp(_mouseY, limiteAbajo, _yMaxRotation);

        transform.rotation = Quaternion.Euler(-_mouseY, xAxis, 0);
    }

    public void SetSosteniendoObjeto(bool valor)
    {
        sosteniendoObjeto = valor;
    }
}
