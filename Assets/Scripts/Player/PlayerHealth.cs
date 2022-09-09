using Scripts.Interfaces;
using System.Collections;
using UnityEngine;

namespace Scripts.Player
{
	public class PlayerHealth : MonoBehaviour, IHit
	{
		private Manager player;
		private MeleeWeapon meleeWeapon;
		public int HP { get; private set; }

		private void Start()
		{
			player = GetComponentInParent<Manager>();
			meleeWeapon = GetComponentInChildren<MeleeWeapon>();
		}

		public void Hit(int damage, IHit.HitWeapon weapon)
		{
			if (player.PowerUpController.HitForceField()) return;

			if (meleeWeapon.BlockActive) {
				HP -= damage / 2;
			} else {
				HP -= damage;
			}

			HP = HP < 0 ? 0 : HP;
			GameEventSystem.Instance.OnPlayerHPChanged?.Invoke(HP);

			if(HP == 0) {
				//TODO Die
			}
		}
		public void StunHit(int damage, float stunTime, IHit.HitWeapon weapon)
		{
			throw new System.NotImplementedException();

		}

		public void Stun(float time, float strength = 1, IHit.HitWeapon weapon = IHit.HitWeapon.OTHER)
		{
			throw new System.NotImplementedException();
		}
	}
}