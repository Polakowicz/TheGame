using System.Collections;
using UnityEngine;


public class EnemyMeleeAttack : MonoBehaviour
{
	private Enemy enemy;
	private EnemySharedData data;

	[SerializeField] private int damage;
	[SerializeField] private float range;
	[SerializeField] private float cooldown;
	[SerializeField] private float animationDelay;

	private float cooldownLeft;

	private void Start()
	{
		enemy = GetComponent<Enemy>();
		data = enemy.SharedData;
	}
	private void Update()
	{
		if(cooldownLeft > 0) {
			cooldownLeft -= Time.deltaTime;
			return;
		}

		if (!CanAttack()) return;
		if (data.DistanceToPlayer > range) return;

		enemy.OnMeleeAttackStart?.Invoke();
		StartCoroutine(WaitForAttack());
	}

	private IEnumerator WaitForAttack()
	{
		yield return new WaitForSeconds(animationDelay);

		if (!CanAttack()) yield break;
		if(data.DistanceToPlayer < range) {
			data.Player.GetComponent<PlayerEventSystem>().GiveDamage(damage);
		}
	}
	private bool CanAttack()
	{
		return !(data.Stunned || data.Pulled || data.Player == null);
	}
}
