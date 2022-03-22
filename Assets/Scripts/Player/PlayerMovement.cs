using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
	//Components
	Rigidbody2D rb;
	[SerializeField] PlayerInput playerInput;

	//Input actions
	InputAction moveAction;

	//Parameters
	[SerializeField] float speed = 5f;

	//Internal variables
	Vector2 input;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();

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
