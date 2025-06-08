using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	[Header("Movement Settings")]
	public float _moveSpeed = 50f;
	public float _turnSpeed = 120f; // Degrees per second
	public float _maxSpeed = 10f;
	public float _acceleration = 2f;
	public float _friction = 0.98f;

	[Header("Leaning Settings")]
	public float _maxLeanAngle = 30f;
	public float _leanSmoothSpeed = 8f;

	[Header("Turning Inertia")]
	public float _minTurnSpeed = 120f; // Degrees/sec at max speed (harder to turn)
	public float _maxTurnSpeed = 350f; // Degrees/sec at min speed (easier to turn)


	private Rigidbody2D _rb;
	private float _currentSpeed;
	private float _moveX;
	private Vector2 _moveDirection = Vector2.down; // Start moving downward

	private InputAction _moveAction;

	void Awake()
	{
		_moveAction = new InputAction(type: InputActionType.Value, binding: "<Gamepad>/leftStick/x");
		_moveAction.AddCompositeBinding("1DAxis")
			.With("Negative", "<Keyboard>/a")
			.With("Negative", "<Keyboard>/leftArrow")
			.With("Positive", "<Keyboard>/d")
			.With("Positive", "<Keyboard>/rightArrow");
		_moveAction.Enable();
	}

	void Start()
	{
		_rb = GetComponent<Rigidbody2D>();
		_currentSpeed = _moveSpeed;
	}

	void Update()
	{
		_moveX = _moveAction.ReadValue<float>();

		// Calculate turn speed based on current speed (inertia effect)
		float speedT = (_currentSpeed - _moveSpeed) / (_maxSpeed - _moveSpeed);
		float _turnSpeedThisFrame = Mathf.Lerp(_maxTurnSpeed, _minTurnSpeed, speedT);

		// Curved Movement
		float _turnAmount = -_moveX * _turnSpeedThisFrame * Time.deltaTime;
		_moveDirection = Quaternion.Euler(0, 0, _turnAmount) * _moveDirection;
		_moveDirection.Normalize();

		// Accelerate forward
		_currentSpeed += _acceleration * Time.deltaTime;
		_currentSpeed = Mathf.Clamp(_currentSpeed, _moveSpeed, _maxSpeed);

		Vector2 _newPosition = _rb.position + _currentSpeed * Time.deltaTime * _moveDirection;

		_currentSpeed *= _friction;

		_rb.MovePosition(_newPosition);

		// Leaning/Curving Effect
		float _targetAngle = Mathf.Atan2(_moveDirection.y, _moveDirection.x) * Mathf.Rad2Deg + 90f;
		float _leanAngle = -_moveX * _maxLeanAngle;
		transform.rotation = Quaternion.Euler(0f, 0f, _targetAngle + _leanAngle);
	}


	void OnDestroy()
	{
		_moveAction.Disable();
		_moveAction.Dispose();
	}
}
