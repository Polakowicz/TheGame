using Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Bullets
{
	public class BlockDirectionChange : MonoBehaviour, IRiposte
	{
		private Rigidbody2D rb;

		public void Riposte(GameObject sender)
		{
			Debug.Log("Bullet riposetd");
			//gameObject.transform.Rotate(0, 0, 180);
			if (rb == null) {
				rb = GetComponent<Rigidbody2D>();
			}
			//rb.velocity = new Vector2(-rb.velocity.x, -rb.velocity.y);
			rb.velocity = rb.position - (Vector2)sender.transform.position;
			Debug.Log(rb.velocity);
		}
	}
}