using DefaultNamespace;
using UnityEngine;

[RequireComponent(typeof(Player)), RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _stopSpeed;

    private Player          _player;
    private Rigidbody2D     _rigidbody2D;
    private PlayerInput     _input;

    private Vector2 _minBorder;
    private Vector2 _maxBorder;
    
    Camera _camera;

    private void Awake()
    {
        _camera         = Camera.main;
        _input          = new PlayerInput();
        _player         = GetComponent<Player>();
        _rigidbody2D    = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _input.Enable();

        Cursor.visible = false;

        _maxBorder  = _camera.ViewportToWorldPoint(new Vector2(1, 1)); // ѕри изменении разрешени€ перезапускать.
        _minBorder  = _camera.ViewportToWorldPoint(new Vector2(0, 0));

        //Debug.Log( "Border: " + _minBorder + " : " + _maxBorder );
    }

    private void OnDisable()
    {
        _input.Disable();

        Cursor.visible = true;
    }

    private void Update()
    {
        Vector2 mousePos        = _input.Movement.MouseMove.ReadValue<Vector2>();
        Vector2 worldPos        = _camera.ScreenToWorldPoint(mousePos);

        MoveMouse(worldPos);
    }

    private void MoveMouse(Vector2 to)
    {
        Vector2 currentPos      = _player.transform.position;
        Vector2 direction       = (to - currentPos).normalized;
        float magnitude         = (to - currentPos).magnitude;

        if(magnitude <= Constant.Epsilon)
        {
            _rigidbody2D.velocity = Vector2.zero;
            return;
        }

        _rigidbody2D.velocity   = direction * _speed * magnitude * Time.timeScale;
    }

    private void MoveKeyboard(Vector2 to)
    {
        _rigidbody2D.AddForce(to * _speed);

        Vector2 currentVelocity = _rigidbody2D.velocity;

        if (currentVelocity.magnitude > _maxSpeed)
        {
            _rigidbody2D.velocity = new Vector2(
                (currentVelocity.x / currentVelocity.magnitude) * _maxSpeed,
                    (currentVelocity.y / currentVelocity.magnitude) * _maxSpeed);
        }

        if (to.magnitude < 0.01f && currentVelocity.magnitude > 0.001f)
        {
            _rigidbody2D.velocity = new Vector2(currentVelocity.x / _stopSpeed, currentVelocity.y / _stopSpeed);
        }
    }
}
