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
			if (rb == null) {
				rb = GetComponent<Rigidbody2D>();
			}
			rb.velocity = (rb.position - (Vector2)sender.transform.position).normalized * rb.velocity.magnitude;
			var newAngle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg - 90;
			transform.rotation = Quaternion.AngleAxis(newAngle, Vector3.forward);
		}
	}
}