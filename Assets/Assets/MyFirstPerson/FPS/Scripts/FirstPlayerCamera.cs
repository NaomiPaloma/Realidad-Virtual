using UnityEngine;

public class FirstPlayerCamera : MonoBehaviour
{
    [SerializeField] float _yMaxRotation, _yMinRotation;
    float _yMinRotacionConObjeto = -20f; // limite cuando sostiene algo

    [Header("Suavizado de Cámara")]
    [Tooltip("Qué tan rápido se acomoda la cámara al agarrar un objeto")]
    public float velocidadSuavizado = 8f;

    float _mouseY;
    float _limiteActual; // Guarda el límite que se está moviendo en tiempo real

    Transform _playersHead;
    bool sosteniendoObjeto = false;

    private void Start()
    {
        // Arrancamos con el límite normal
        _limiteActual = _yMinRotation;
    }

    private void LateUpdate()
    {
        Movement();
        SuavizarBloqueo(); // Llama a la nueva función de suavizado
    }

    public void SetPlayersHead(Transform playersHead)
    {
        _playersHead = playersHead;
    }

    void Movement()
    {
        if (_playersHead != null)
        {
            transform.position = _playersHead.position;
        }
    }

    public void Rotate(float xAxis, float yAxis)
    {
        _mouseY += yAxis;

        // Ahora clampamos usando el límite dinámico en vez del estático
        _mouseY = Mathf.Clamp(_mouseY, _limiteActual, _yMaxRotation);

        transform.rotation = Quaternion.Euler(-_mouseY, xAxis, 0);
    }

    public void SetSosteniendoObjeto(bool valor)
    {
        sosteniendoObjeto = valor;
    }

    // --- NUEVA FUNCIÓN ---
    void SuavizarBloqueo()
    {
        // 1. Elegimos a qué límite queremos ir
        float limiteObjetivo = sosteniendoObjeto ? _yMinRotacionConObjeto : _yMinRotation;

        // 2. Movemos el límite actual hacia el objetivo suavemente con Lerp
        _limiteActual = Mathf.Lerp(_limiteActual, limiteObjetivo, Time.deltaTime * velocidadSuavizado);

        // 3. Si la cámara quedó por debajo del nuevo límite, la empujamos hacia arriba suavemente
        // Esto hace que se acomode sola incluso si el jugador no mueve el mouse al agarrar el cubo
        if (_mouseY < _limiteActual)
        {
            _mouseY = _limiteActual;
            transform.rotation = Quaternion.Euler(-_mouseY, transform.eulerAngles.y, 0);
        }
    }
}
