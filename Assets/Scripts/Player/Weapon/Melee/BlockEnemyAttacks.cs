using UnityEngine;

namespace Scripts.Player {

	public class BlockEnemyAttacks : MonoBehaviour
	{
		private MeleeWeapon weapon;

		private void Start()
		{
			weapon = GetComponentInParent<MeleeWeapon>();
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (!weapon.BlockActive) return;

			if (weapon.BlockLayerMask !=
				(weapon.BlockLayerMask | (1 << collision.gameObject.layer)))
				return;
			
			Destroy(collision.gameObject);//TODO is this class even required???
		}
	}
}
