using Scripts.Interfaces;
using System.Collections;
using UnityEngine;


public class EnemyMeleeAttack : MonoBehaviour
{
	private OldEnemy main;

	[SerializeField] private int damage;
	[SerializeField] private float range;
	[SerializeField] private float cooldown;
	[SerializeField] private float animationDelay;

	private float cooldownLeft;

	private void Start()
	{
		main = GetComponent<OldEnemy>();

	}
	private void Update()
	{
		if(cooldownLeft > 0) {
			cooldownLeft -= Time.deltaTime;
			return;
		}

		if (!CanAttack()) return;
		if (main.Data.DistanceToPlayer > range) return;

		main.OnMeleeAttackStart?.Invoke();
		StartCoroutine(WaitForAttack());
		cooldownLeft = cooldown;
	}

	private IEnumerator WaitForAttack()
	{
		yield return new WaitForSeconds(animationDelay);

		if (!CanAttack()) yield break;
		if(main.Data.DistanceToPlayer < range) {
			main.Data.Player.GetComponent<IHit>().Hit(gameObject, damage, IHit.HitWeapon.OTHER);
		}
	}
	private bool CanAttack()
	{
		return !(main.Data.Stunned || main.Data.Pulled || main.Data.Player == null);
	}
}
