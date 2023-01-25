using Scripts.Game;
using Scripts.Interfaces;
using Scripts.Tools;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Visuals;

namespace Scripts.Enemies
{
    public class SpawnerManager : ExtendedMonoBehaviour, IHit
    {
        private AudioManager audioManager;
        private readonly string DieTriggerAnimatorName = "Die";
        private readonly string SpawnWaveAnimatorName = "SpawnNextWave";
        private readonly string RestAnimatorStateName = "Rest";

		private Animator animator;
        private SpriteRenderer spriteRenderer;
        private BossSpawningEnemies spawningComponent;

        // Helth for 1 stage,
        [SerializeField] private int StageHealh;
        private int healthLeft;

        private void Awake()
		{
			animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
			spawningComponent = GetComponent<BossSpawningEnemies>();

			healthLeft = StageHealh;

            audioManager = FindObjectOfType<AudioManager>();
		}

        public void ResetHealth() => healthLeft = StageHealh;
   
		public void Hit(GameObject attacker, int damage, IHit.HitWeapon weapon = IHit.HitWeapon.OTHER)
        {
            // Get damage only in rest state
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName(RestAnimatorStateName)) return;

			healthLeft = Mathf.Clamp(healthLeft - damage, 0, StageHealh);
            audioManager.Play("EnemyDamage");
            StartCoroutine(FlashingRedOnHit());
            if (healthLeft == 0)
            {
                if (spawningComponent.WavesLeft == 0)
                {
                    audioManager.Play("DjinnDeath");
                    animator.SetTrigger(DieTriggerAnimatorName);
                } else
                {
                    animator.SetTrigger(SpawnWaveAnimatorName);
                }
            }
        }

        public void Stun(GameObject attacker, float time, float strength = 1, IHit.HitWeapon weapon = IHit.HitWeapon.OTHER)
        {
            // Stund does nothing to this enemy, he get hits only when he is resting
            return;
        }
        public void StunHit(GameObject attacker, int damage, float stunTime, IHit.HitWeapon weapon = IHit.HitWeapon.OTHER)
        {
            // Call only hit because stun does nothing
            Hit(attacker, damage, weapon);
        }
        
        public IEnumerator FlashingRedOnHit()
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = Color.white;
        }
        
    }
}
