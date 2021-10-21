using UnityEngine;

[RequireComponent(typeof(Player)), RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _stopSpeed;
    [SerializeField] private Camera _mainCamera;

    private Player _player;
    private Rigidbody2D _rigidbody2D;
    private PlayerInput _input;

    private Vector2 minBorder;
    private Vector2 maxBorder;

    private void Awake()
    {
        _input = new PlayerInput();
    }

    private void OnEnable()
    {
        _input.Enable();

        Cursor.visible = false;

        maxBorder = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)); // ѕри изменении разрешени€ перезапускать.
        minBorder = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
    }

    private void OnDisable()
    {
        _input.Disable();

        Cursor.visible = true;
    }

    private void Start()
    {
        _player = GetComponent<Player>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {

        Vector2 mousePos = _input.Movement.MouseMove.ReadValue<Vector2>();
        Vector2 moveToMouse = Camera.main.ScreenToWorldPoint(mousePos);

        MoveMouse(moveToMouse);
    }

    private void MoveMouse(Vector2 movement)
    {
        var currentPosition = _player.transform.position;

        if (currentPosition.x >= maxBorder.x || currentPosition.y >= maxBorder.y || currentPosition.x <= minBorder.x || currentPosition.y <= minBorder.y)
            return;
        else
            _player.transform.position = Vector2.MoveTowards(_player.transform.position, movement, _speed * Time.fixedDeltaTime);     
    }

    private void MoveKeyboard(Vector2 movement)
    {
        _rigidbody2D.AddForce(movement * _speed);

        Vector2 currentVelocity = _rigidbody2D.velocity;

        if (currentVelocity.magnitude > _maxSpeed)
        {
            _rigidbody2D.velocity = new Vector2(
                (currentVelocity.x / currentVelocity.magnitude) * _maxSpeed,
                    (currentVelocity.y / currentVelocity.magnitude) * _maxSpeed);
        }

        if (movement.magnitude < 0.01f && currentVelocity.magnitude > 0.001f)
        {
            _rigidbody2D.velocity = new Vector2(currentVelocity.x / _stopSpeed, currentVelocity.y / _stopSpeed);
        }
    }
}
