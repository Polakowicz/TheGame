using System.Collections;
using UnityEngine;


public class EnemyMeleeAttack : MonoBehaviour
{
	private Enemy enemy;

	[SerializeField] private int damage;
	[SerializeField] private float range;
	[SerializeField] private float cooldown;
	[SerializeField] private float animationDelay;

	private float cooldownLeft;

	private void Start()
	{
		enemy = GetComponent<Enemy>();

	}
	private void Update()
	{
		if(cooldownLeft > 0) {
			cooldownLeft -= Time.deltaTime;
			return;
		}

		if (!CanAttack()) return;
		if (enemy.Data.DistanceToPlayer > range) return;

		enemy.OnMeleeAttackStart?.Invoke();
		StartCoroutine(WaitForAttack());
	}

	private IEnumerator WaitForAttack()
	{
		yield return new WaitForSeconds(animationDelay);

		if (!CanAttack()) yield break;
		if(enemy.Data.DistanceToPlayer < range) {
			enemy.Data.Player.GetComponent<PlayerEventSystem>().GiveDamage(damage);
		}
	}
	private bool CanAttack()
	{
		return !(enemy.Data.Stunned || enemy.Data.Pulled || enemy.Data.Player == null);
	}
}
