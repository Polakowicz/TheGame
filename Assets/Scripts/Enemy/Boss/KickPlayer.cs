using System.Collections;
using UnityEngine;

public class KickPlayer : MonoBehaviour
{
	private Goblin main;

	[SerializeField] private int damage;
	[SerializeField] private float speed;
	[SerializeField] private float distance;

	private void Start()
	{
		main = GetComponentInParent<Goblin>();
		main.OnKickPlayer += Kick;
	}
	private void OnDestroy()
	{
		main.OnKickPlayer -= Kick;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer != LayerMask.NameToLayer("Player")) return;

		main.OnPlayerInKickRange?.Invoke();
	}

	private void Kick()
	{
		//main.Player.GetComponent<PlayerEventSystem>().Kick(speed, distance, damage);
	}
}
