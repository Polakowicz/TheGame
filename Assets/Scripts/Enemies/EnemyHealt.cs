using Scripts.Interfaces;
using System.Collections;
using UnityEngine;

namespace Scripts.Enemies
{
	public class EnemyHealt : MonoBehaviour, IHit
	{
		[SerializeField] private int hp;

		public void Hit(int damage, IHit.HitWeapon weapon)
		{
			Debug.Log($"Enemy hp: {hp},  hit by {damage} dmg,  left hp {hp - damage}");
			hp -= damage;

			if (hp <= 0) {
				//TODO
				Destroy(gameObject);
			}
		}

		public void Hit(GameObject attacker, int damage, IHit.HitWeapon weapon = IHit.HitWeapon.OTHER)
		{
			throw new System.NotImplementedException();
		}

		public void Stun(GameObject attacker, float time, float strength = 1, IHit.HitWeapon weapon = IHit.HitWeapon.OTHER)
		{
			throw new System.NotImplementedException();
		}

		public void StunHit(GameObject attacker, int damage, float stunTime, IHit.HitWeapon weapon = IHit.HitWeapon.OTHER)
		{
			throw new System.NotImplementedException();
		}
	}
}