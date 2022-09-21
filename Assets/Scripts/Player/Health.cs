using Scripts.Game;
using Scripts.Interfaces;
using Scripts.Tools;
using System.Collections;
using UnityEngine;

namespace Scripts.Player
{
	public class Health : ExtendedMonoBehaviour, IHit
	{
		private PlayerManager player;
		private BladePlayerWeapon meleeWeapon;
		[SerializeField] private int hp;
		public int HP { get => hp; private set => hp = value; }

		private void Start()
		{
			player = GetComponentInParent<PlayerManager>();
			meleeWeapon = GetComponentInChildren<BladePlayerWeapon>();
		}

		public void Hit(GameObject attacker, int damage, IHit.HitWeapon weapon)
		{
			if (IsHit(attacker)) {
				Damage(damage);
			}
		}
		public void StunHit(GameObject attacker, int damage, float stunTime, IHit.HitWeapon weapon)
		{
			if (!IsHit(attacker)) return;

			Damage(damage);
			Stun(gameObject, stunTime);
		}
		public void Stun(GameObject attacker, float time, float strength = 1, IHit.HitWeapon weapon = IHit.HitWeapon.OTHER)
		{
			if(player.State == PlayerManager.PlayerState.Stun) {
				StopAllCoroutines();
			}

			player.State = PlayerManager.PlayerState.Stun;
			StartCoroutine(WaitAndDo(time, () => {
				player.State = PlayerManager.PlayerState.Walk;
			}));
		}

		private void Damage(int damage)
		{
			//Debug.Log($"Player damaged by {damage}; HP left: {HP}");
			if (meleeWeapon.IsBlockActive) {
				Debug.Log("Attack blocked");
				HP -= damage / 2;
			} else {
				HP -= damage;
			}

			HP = HP < 0 ? 0 : HP;
			GameEventSystem.Instance.OnPlayerHPChanged?.Invoke(HP);

			if (HP == 0) {
				player.AnimationController.Die();
			}
		}
		private bool IsHit(GameObject attacker)
		{
			if (player.State == PlayerManager.PlayerState.Dash) return false;

			if (player.PowerUpController.HitForceField()) return false;

			if (meleeWeapon.IsRiposteActive) {
				attacker.GetComponent<IRiposte>().Riposte(gameObject);
				return false;
			}

			return true;
		}
		
	}
}