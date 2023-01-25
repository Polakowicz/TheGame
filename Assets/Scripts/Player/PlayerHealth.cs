using Scripts.Game;
using Scripts.Interfaces;
using Scripts.Tools;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Player
{
	public class PlayerHealth : ExtendedMonoBehaviour, IHit
	{
		// [whereistheguru]
		private AudioManager audioManager;
		// It's a "Filled" Image Type, so full health's value equals 1, while e.g. 65% is 0.65
		public Image healthbarFill;
		public Image heartIcon;
		private float hp_bar;
		// UI elements to disable/re-enable when Player dies/revives.
		public GameObject healthbar;
		public GameObject lowHealthPanel;
		
		private PlayerManager player;
		private BladePlayerWeapon meleeWeapon;

		[SerializeField] private int maxHp;
		private int hp;
		public int HP { get => hp; private set => hp = value; }

		private void Awake()
		{
			player = GetComponentInParent<PlayerManager>();
			meleeWeapon = GetComponentInChildren<BladePlayerWeapon>();

			hp = maxHp;
			// [whereistheguru]
			healthbarFill.fillAmount = 1;
			heartIcon.color = new Color32(255, 255, 255, 255);
			healthbar.SetActive(true);
			lowHealthPanel.SetActive(false);
			audioManager = FindObjectOfType<AudioManager>();
		}

		public void RestoreHP()
		{
			hp = maxHp;
			// [whereistheguru]
			healthbarFill.fillAmount = 1;
			heartIcon.color = new Color32(255, 255, 255, 255);
			lowHealthPanel.SetActive(false);
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
			if (player.State == PlayerManager.PlayerState.Cutscene ||
				player.State == PlayerManager.PlayerState.Dead) return;

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
			if (meleeWeapon.IsBlockActive) {
				HP -= damage / 2;
				// [whereistheguru]
				hp_bar = HP;
				healthbarFill.fillAmount = hp_bar / 100;
			} else {
				HP -= damage;
				audioManager.Play("PlayerDamage");
				// [whereistheguru]
				hp_bar = HP;
				healthbarFill.fillAmount = hp_bar / 100;
			}

			HP = HP < 0 ? 0 : HP;
			GameEventSystem.Instance.OnPlayerHPChanged?.Invoke(HP);

			// [whereistheguru]
			if (HP < 30){
				lowHealthPanel.SetActive(true);
				heartIcon.color = new Color32(75, 0, 0, 255);
			}

			if (HP == 0) {
				player.AnimationController.Die();
				// [whereistheguru]
				audioManager.Play("PlayerDeath");
				healthbar.SetActive(false);
				PlayerMovement.playerControlsEnabled = false;
                
			}
		}

		public void PlayerDied() {
            GameEventSystem.Instance.RestartGame();
        }


		private bool IsHit(GameObject attacker)
		{
			// Player is immune to attack during dash
			if (player.State == PlayerManager.PlayerState.Dash) return false;

			if (meleeWeapon.IsRiposteActive)
			{
				attacker.GetComponent<IRiposte>()?.Riposte(gameObject);
				return false;
			}

			if (player.PowerUpController.HitForceField()) return false;

			// Player got hit
			return true;
		}
		
	}
}