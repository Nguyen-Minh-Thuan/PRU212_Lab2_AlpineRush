using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	[Header("Movement Settings")]
	public float moveSpeed = 5f;
	public float laneWidth = 2f;
	public float turnSpeed = 7f;
	public float maxSpeed = 10f;
	public float acceleration = 2f;
	public float friction = 0.98f;

	private Rigidbody2D rb;
	private float currentSpeed;
	private float moveX;

	// InputAction for horizontal movement
	private InputAction moveAction;

	void Awake()
	{
		//  Simple action for horizontal movement, smth like that, idk how to properly making an input system:D
		moveAction = new InputAction(type: InputActionType.Value, binding: "<Gamepad>/leftStick/x");
		moveAction.AddCompositeBinding("1DAxis")
			.With("Negative", "<Keyboard>/a")
			.With("Negative", "<Keyboard>/leftArrow")
			.With("Positive", "<Keyboard>/d")
			.With("Positive", "<Keyboard>/rightArrow");
		moveAction.Enable();
	}

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		currentSpeed = moveSpeed;
	}

	void Update()
	{
		// Read value from the Input System
		moveX = moveAction.ReadValue<float>();

		Vector2 newPosition = rb.position + moveX * Time.deltaTime * turnSpeed * Vector2.right;

		currentSpeed += acceleration * Time.deltaTime;
		currentSpeed = Mathf.Clamp(currentSpeed, moveSpeed, maxSpeed);

		newPosition += currentSpeed * Time.deltaTime * Vector2.down;

		currentSpeed *= friction;

		rb.MovePosition(newPosition);
	}

	void OnDestroy()
	{
		moveAction.Disable();
		moveAction.Dispose();
	}
}
