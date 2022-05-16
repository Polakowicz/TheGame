using System.Collections;
using UnityEngine;

public class PushableRock : MonoBehaviour
{
	private Rigidbody2D rb;

	[SerializeField] private float speed;

	bool pushed;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	public void Push(Vector2 direction)
	{
		pushed = true;
		rb.velocity = direction.normalized * speed;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (!pushed) return;

		if(collision.gameObject.layer == LayerMask.NameToLayer("Boss")) {
			Debug.Log("Damage boss with rock");
		}

		Destroy(gameObject);
	}
}
