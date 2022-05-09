using System.Collections;
using UnityEngine;


public class EnemyMelleAttack : MonoBehaviour
{
	Enemy enemy;
	EnemySharedData data;

	[SerializeField] int damage;
	[SerializeField] float range;
	[SerializeField] float cooldown;
	[SerializeField] float animationDelay;

	float cooldownLeft;

	void Start()
	{
		enemy = GetComponent<Enemy>();
		data = enemy.SharedData;
	}

	void Update()
	{
		if(cooldownLeft > 0) {
			cooldownLeft -= Time.deltaTime;
			return;
		}

		if (!CanAttack()) return;
		if (data.DistanceToPlayer > range) return;

		enemy.OnMeleeAttackAnimationStarts?.Invoke();
		StartCoroutine(WaitForAttack());
	}

	IEnumerator WaitForAttack()
	{
		yield return new WaitForSeconds(animationDelay);

		if (!CanAttack()) yield break;
		if(data.DistanceToPlayer < range) {
			data.Player.GetComponent<PlayerEventSystem>().GiveDamage(damage);
		}
	}

	bool CanAttack()
	{
		return !(data.Stunned || data.Pulled || data.Player == null);
	}
}
