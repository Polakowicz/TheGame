using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
	//Components
	Rigidbody2D rb;
	[SerializeField] PlayerInput playerInput;

	//Input actions
	InputAction moveAction;
	InputAction dashAction;

	//Parameters
	[SerializeField] float basicSpeed = 5f;
	[SerializeField] float dashSpeed = 30f;
	[SerializeField] float dashTime = 0.1f;

	//Internal variables
	Vector2 input;
	bool isInDash;
	float speed;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();

		moveAction = playerInput.actions["Move"];
		dashAction = playerInput.actions["Dash"];

		dashAction.performed += PerformDash;

		speed = basicSpeed;
	}

	void OnDestroy()
	{
		dashAction.performed -= PerformDash;
	}

	void Update()
	{
		if (!isInDash) {
			input = moveAction.ReadValue<Vector2>();
		}
	}

	void FixedUpdate()
	{
		if (isInDash) {
			rb.velocity = input.normalized * speed;
		} else {
			rb.velocity = input * speed;
		}
	}

	void PerformDash(InputAction.CallbackContext context)
	{
		isInDash = true;
		speed = dashSpeed;
		StartCoroutine(DashDelay(dashTime));
	}

	IEnumerator DashDelay(float delay)
	{
		yield return new WaitForSeconds(delay);
		speed = basicSpeed;
		isInDash = false;
	}
}
