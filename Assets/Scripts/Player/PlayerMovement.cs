using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
	//Components
	Rigidbody2D rb;

	Vector2 input;

	[SerializeField] float speed = 5f;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
	}

	void FixedUpdate()
	{
		Vector2 direction = input;
		if(input.magnitude > 1) {
			direction = input.normalized;
		}
		rb.velocity = direction * speed;
	}
}
