using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
	//Components
	Rigidbody2D rb;
	PlayerInput playerInput;

	//Input actions
	InputAction moveAction;

	//Parameters
	[SerializeField] float speed = 5f;

	//Internal varaibles
	Vector2 input;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		playerInput = GetComponent<PlayerInput>();

		moveAction = playerInput.actions["Move"];
	}

	void Update()
	{
		input = moveAction.ReadValue<Vector2>();
	}

	void FixedUpdate()
	{
		rb.velocity = input * speed;
	}
}
