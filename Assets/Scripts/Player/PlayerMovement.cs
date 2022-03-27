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
	[SerializeField] float normalSpeed = 5f;
	[SerializeField] float dashSpeed = 30f;
	[SerializeField] float dashTime = 0.1f;

	//Internal variables
	Vector2 input;
	bool isInDash;
	float realSpeed;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();

		moveAction = playerInput.actions["Move"];
		dashAction = playerInput.actions["Dash"];

		dashAction.performed += PerformDash;

		realSpeed = normalSpeed;
	}

	private void OnDestroy()
	{
		dashAction.performed -= PerformDash;
	}

	void Update()
	{
		if(!isInDash)
			input = moveAction.ReadValue<Vector2>();
	}

	void FixedUpdate()
	{
		rb.velocity = input * realSpeed;
	}

	void PerformDash(InputAction.CallbackContext context)
	{
		StartCoroutine(DashDelay(dashTime));
	}

	IEnumerator DashDelay(float delay)
	{
		isInDash = true;
		realSpeed = dashSpeed;
		yield return new WaitForSeconds(delay);
		realSpeed = normalSpeed;
		isInDash = false;
	}
}
