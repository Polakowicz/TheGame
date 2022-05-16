using System.Collections;
using UnityEngine;

public class KickPlayer : MonoBehaviour
{
	[SerializeField] private int damage;
	[SerializeField] private float speed;
	[SerializeField] private float distance;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer != LayerMask.NameToLayer("Player")) return;

		collision.GetComponent<PlayerEventSystem>().Kick(speed, distance, damage);
	}
}
