using Assets.Scripts.GameController;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	[Header("Movement Settings")]
	public float _moveSpeed = 37f;
	public float _turnSpeed = 120f; // Degrees per second
	public float _maxSpeed;
    public float _acceleration = 2f;
	public float _friction = 0.98f;
    public float _stageSpeed = 100f;
    public float _flightSpeed = 2f;
    private float _originalMoveSpeed;

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
    private bool _isVulnerable = true; // Player vulnerability state
    private float _playerPoints = 0f; // Player points, can be used for scoring or other purposes
    private GameConfiguration _gameConfiguration;

    private InputAction _moveAction;
    private InputAction _accelerate;

	void Awake()
	{
		_moveAction = new InputAction(type: InputActionType.Value, binding: "<Gamepad>/leftStick/x");
        _moveAction.AddCompositeBinding("1DAxis")
            .With("Negative", "<Keyboard>/a")
            .With("Negative", "<Keyboard>/leftArrow")
            .With("Positive", "<Keyboard>/d")
            .With("Positive", "<Keyboard>/rightArrow");
		_moveAction.Enable();
        _accelerate = new InputAction(type: InputActionType.Button, binding: "<Keyboard>/space");
        _accelerate.Enable();
    }

	void Start()
	{
		_rb = GetComponent<Rigidbody2D>();
        _gameConfiguration = GetComponent<GameConfiguration>();
		_currentSpeed = _moveSpeed;
        _gameConfiguration._maxStageSpeed = 30f;
    }

    void Update()
    {
        _stageSpeed = _gameConfiguration._maxStageSpeed * _gameConfiguration._stageMutiplier; // Set stage speed based on game configuration
        _maxSpeed = _stageSpeed*0.8f; // Set max speed from game configuration

        var getAccelerate = _accelerate.ReadValue<float>();
        if (getAccelerate == 1f && _currentSpeed < _stageSpeed)
            _moveSpeed += 0.1f;
        else if(_currentSpeed > _maxSpeed)
            _moveSpeed -= 0.1f;

        _moveX = _moveAction.ReadValue<float>();

        // Calculate turn speed based on current speed (inertia effect)
        float speedT = (_currentSpeed - _moveSpeed) / (_maxSpeed - _moveSpeed);
        float _turnSpeedThisFrame = Mathf.Lerp(_maxTurnSpeed, _minTurnSpeed, speedT);

        float maxTurnAngle = 60f; // Maximum angle from downward (in degrees)
        float currentAngle = Vector2.SignedAngle(Vector2.down, _moveDirection);

        float _turnAmount = _moveX * _turnSpeedThisFrame * Time.deltaTime;
        float newAngle = Mathf.Clamp(currentAngle + _turnAmount, -maxTurnAngle, maxTurnAngle);

        // If no input, smoothly return to downward direction
        if (Mathf.Approximately(_moveX, 0f))
        {
            // Smoothly rotate back to Vector2.down
            float returnSpeed = _turnSpeedThisFrame * Time.deltaTime;
            _moveDirection = Vector3.RotateTowards(_moveDirection, Vector2.down, returnSpeed * Mathf.Deg2Rad, 0f);
            _moveDirection = ((Vector2)_moveDirection).normalized;
            // Clamp angle to maxTurnAngle
            float angleAfterReturn = Vector2.SignedAngle(Vector2.down, _moveDirection);
            angleAfterReturn = Mathf.Clamp(angleAfterReturn, -maxTurnAngle, maxTurnAngle);
            _moveDirection = Quaternion.Euler(0, 0, angleAfterReturn) * Vector2.down;
            _moveDirection.Normalize();
        }
        else
        {
            // Set new move direction based on clamped angle
            _moveDirection = Quaternion.Euler(0, 0, newAngle) * Vector2.down;
            _moveDirection.Normalize();
        }

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
        // Debugging information
    }


    void OnDestroy()
	{
		_moveAction.Disable();
		_moveAction.Dispose();
	}

    public void SlowDown(float slowPercent)
    {
        _moveSpeed *= (1f - slowPercent);
    }

    public void TakeFlight()
    {
        _isVulnerable = false;
        _originalMoveSpeed = _moveSpeed; // Store the original speed
        _moveSpeed *= _flightSpeed; // Increase speed for flight
    }

    public void Land()
    {
        _moveSpeed = _originalMoveSpeed; // Restore the original speed
        _isVulnerable = true;
    }

    public float GetPlayerPoints()
    {
        return _playerPoints;
    }

    public void AddPoints(int v)
    {
        _playerPoints += v;
    }

    public bool IsVulnerable()
    {
        return _isVulnerable;
    }

    public void PlayerLose()
    {
        _isVulnerable = false;
        _moveSpeed = 0f; // Stop the player
        // Additional logic for player losing can be added here (e.g., game over screen)
    }




}
