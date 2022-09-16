﻿using Scripts.Interfaces;
using Scripts.Tools;
using System.Collections;
using UnityEngine;

namespace Scripts.Player
{
	public class Health : ExtendedMonoBehaviour, IHit
	{
		private Manager player;
		private MeleeWeapon meleeWeapon;
		public int HP { get; private set; }

		private void Start()
		{
			player = GetComponentInParent<Manager>();
			meleeWeapon = GetComponentInChildren<MeleeWeapon>();
		}

		public void Hit(GameObject attacker, int damage, IHit.HitWeapon weapon)
		{
			if (IsHit()) {
				Damage(damage);
			}
		}
		public void StunHit(GameObject attacker, int damage, float stunTime, IHit.HitWeapon weapon)
		{
			if (!IsHit()) return;

			Damage(damage);
			Stun(gameObject, stunTime);
		}
		public void Stun(GameObject attacker, float time, float strength = 1, IHit.HitWeapon weapon = IHit.HitWeapon.OTHER)
		{
			if(player.State == Manager.PlayerState.Stun) {
				StopAllCoroutines();
			}

			player.State = Manager.PlayerState.Stun;
			StartCoroutine(WaitAndDo(time, () => {
				player.State = Manager.PlayerState.Walk;
			}));
		}

		private void Damage(int damage)
		{
			if (meleeWeapon.BlockActive) {
				HP -= damage / 2;
			} else {
				HP -= damage;
			}

			HP = HP < 0 ? 0 : HP;
			GameEventSystem.Instance.OnPlayerHPChanged?.Invoke(HP);

			if (HP == 0) {
				//TODO Die
			}
		}
		private bool IsHit()
		{
			if (player.State == Manager.PlayerState.Dash) return false;

			if (player.PowerUpController.HitForceField()) return false;

			return true;
		}
		
	}
}