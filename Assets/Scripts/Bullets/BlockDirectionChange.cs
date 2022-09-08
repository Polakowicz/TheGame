using Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDirectionChange : MonoBehaviour, IRiposte
{
	private Rigidbody2D rb;

	public void Riposte()
	{
		gameObject.transform.Rotate(0, 0, 180);
		if (rb == null) {
			rb = GetComponent<Rigidbody2D>();
		}
		rb.velocity = new Vector2(-rb.velocity.x, -rb.velocity.y);
	}
}
