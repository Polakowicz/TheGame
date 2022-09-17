using Scripts.Interfaces;
using System.Collections;
using UnityEngine;

namespace Scripts.Enemies
{
	public class Health : MonoBehaviour, IHit, IRiposte
	{
		private Enemy manager;
		[SerializeField] private int hp;

		private void Start()
		{
			manager = GetComponent<Enemy>();
		}

		public void Hit(GameObject attacker, int damage, IHit.HitWeapon weapon = IHit.HitWeapon.OTHER)
		{
			Debug.Log($"Enemy hp: {hp},  hit by {damage} dmg,  left hp {hp - damage}");
			hp -= damage;

			if (hp <= 0) {
				manager.Animator.SetTrigger("Die");
			}
		}

		public void Stun(GameObject attacker, float time, float strength = 1, IHit.HitWeapon weapon = IHit.HitWeapon.OTHER)
		{
			throw new System.NotImplementedException();
		}

		public void StunHit(GameObject attacker, int damage, float stunTime, IHit.HitWeapon weapon = IHit.HitWeapon.OTHER)
		{
			throw new System.NotImplementedException();
		}

		public void Riposte()
		{
			Debug.Log("Enemy riposeted");
		}
	}
}