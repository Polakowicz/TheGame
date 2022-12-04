using Scripts.Game;
using Scripts.Interfaces;
using Scripts.Tools;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Scripts.Enemies
{
    public class SpawnerManager : ExtendedMonoBehaviour, IHit
    {
        private readonly string DieTriggerAnimatorName = "Die";
        private readonly string SpawnWaveAnimatorName = "SpawnNextWave";
        private readonly string RestAnimatorStateName = "Rest";

		private Animator animator;
        private BossSpawningEnemies spawningComponent;

        // Helth for 1 stage,
        [SerializeField] private int StageHealh;
        private int healthLeft;

		private void Awake()
		{
			animator = GetComponent<Animator>();
			spawningComponent = GetComponent<BossSpawningEnemies>();

			healthLeft = StageHealh;
		}

        public void ResetHealth() => healthLeft = StageHealh;
   
		public void Hit(GameObject attacker, int damage, IHit.HitWeapon weapon = IHit.HitWeapon.OTHER)
        {
            // Get damage only in rest state
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName(RestAnimatorStateName)) return;

			healthLeft = Mathf.Clamp(healthLeft - damage, 0, StageHealh);
            if(healthLeft == 0)
            {
                if (spawningComponent.WavesLeft == 0)
                {
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

    
    }
}
