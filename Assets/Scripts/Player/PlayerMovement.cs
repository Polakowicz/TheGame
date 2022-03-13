using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
	//Components
	Rigidbody2D rb;
	PlayerInput playerInput;

	Vector2 input;

	[SerializeField] float speed = 5f;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		playerInput = GetComponent<PlayerInput>();
	}

	void Update()
	{
		input = playerInput.actions["Movement"].ReadValue<Vector2>();
	}

	void FixedUpdate()
	{
		//Vector2 direction = input;
		//if(input.magnitude > 1) {
		//	direction = input.normalized;
		//}
		rb.velocity = input * speed;
	}
}
