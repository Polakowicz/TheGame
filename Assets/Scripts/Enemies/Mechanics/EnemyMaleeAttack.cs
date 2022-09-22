using Scripts.Interfaces;
using UnityEngine;

namespace Scripts.Enemies
{
	public class EnemyMaleeAttack : MonoBehaviour
	{
		private EnemyManager manager;

		[SerializeField] private int range;
		[SerializeField] private int damage;

		private void Awake()
		{
			manager = GetComponentInParent<EnemyManager>();
		}

		public void Attack()
		{
			var distance = Vector2.Distance(manager.Player.transform.position, gameObject.transform.position);
			if (distance > range) return;

			manager.Player.GetComponent<IHit>().Hit(gameObject, damage);
		}
	}
}