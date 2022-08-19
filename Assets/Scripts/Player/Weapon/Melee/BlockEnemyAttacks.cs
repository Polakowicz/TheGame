using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Player.Weapon {

	public class BlockEnemyAttacks : MonoBehaviour
	{
		private MeleeWeapon weapon;

		void Start()
		{
			weapon = GetComponentInParent<MeleeWeapon>();
		}

		void OnTriggerEnter2D(Collider2D collision)
		{
			if (!weapon.BlockActive) return;

			if (weapon.BlockLayerMask !=
				(weapon.BlockLayerMask | (1 << collision.gameObject.layer)))
				return;
			
			Destroy(collision.gameObject);//TODO
		}
	}
}
