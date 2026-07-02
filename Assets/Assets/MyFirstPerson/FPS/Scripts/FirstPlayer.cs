using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPlayer : MonoBehaviour
{
    [SerializeField] Transform _headPosition;

    [SerializeField] float _movementSpeed;
    [SerializeField] float _mouseSensitivity;

    Rigidbody _rgbd;
    FirstPlayerCamera _myCam;

    float _mouseX;
    float _inputMouseX, _inputMouseY, _inputVertical, _inputHorizontal;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (_rgbd == null) _rgbd = GetComponent<Rigidbody>();

        // ignora colision entre objetos y baldosas
        Physics.IgnoreLayerCollision(
            LayerMask.NameToLayer("Objetos"),
            LayerMask.NameToLayer("Baldosas"),
            true
        );

        // ignora colision entre player y objetos
        Physics.IgnoreLayerCollision(
            LayerMask.NameToLayer("Player"),
            LayerMask.NameToLayer("Objetos"),
            true
        );
    }

    private void Start()
    {
        if (_myCam == null) _myCam = Camera.main.GetComponent<FirstPlayerCamera>();

        if (_myCam != null)
        {
            _myCam.SetPlayersHead(_headPosition);
        }
    }

    void Update()
    {
        _inputMouseX = Input.GetAxisRaw("Mouse X");
        _inputMouseY = Input.GetAxisRaw("Mouse Y");

        _inputVertical = Input.GetAxis("Vertical");
        _inputHorizontal = Input.GetAxis("Horizontal");

        if (_inputMouseX != 0 || _inputMouseY != 0)
        {
            Rotation(_inputMouseX, _inputMouseY);
        }
    }

    private void FixedUpdate()
    {
        if (_inputVertical != 0 || _inputHorizontal != 0)
        {
            Movement(_inputHorizontal, _inputVertical);
        }
    }

    public void Rotation(float rotX, float rotY)
    {
        // 1. Calculamos el giro con tu sensibilidad y Time.deltaTime intactos
        float movimientoX = rotX * _mouseSensitivity * Time.deltaTime;
        float movimientoY = rotY * _mouseSensitivity * Time.deltaTime;

        // 2. FILTRO ANTI-LAG: Limitamos el giro máximo permitido en un solo frame (ej. 15 grados).
        // Si el juego da un tirón y el cálculo explota a 500, esto lo frena en seco a 15.
        movimientoX = Mathf.Clamp(movimientoX, -15f, 15f);
        movimientoY = Mathf.Clamp(movimientoY, -15f, 15f);

        _mouseX += movimientoX;

        if (_mouseX >= 360)
        {
            _mouseX -= 360;
        }
        else if (_mouseX <= -360)
        {
            _mouseX += 360;
        }

        transform.rotation = Quaternion.Euler(0, _mouseX, 0);

        _myCam?.Rotate(_mouseX, movimientoY);
    }

    public void Movement(float horizontalAxi, float verticalAxi)
    {
        Vector3 direction = transform.forward * verticalAxi + transform.right * horizontalAxi;

        if (direction.sqrMagnitude > 1)
        {
            direction.Normalize();
        }

        _rgbd.MovePosition(_rgbd.position + direction * (_movementSpeed * Time.fixedDeltaTime));
    }
}