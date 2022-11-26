using Assets.Scripts.Enemies.Mechanics;
using Scripts.Interfaces;
using System.Collections;
using UnityEngine;

namespace Scripts.Enemies
{
    public class SmokerManager : MonoBehaviour, IHit
    {
        private enum SState
        {
            Waiting,
            Smoking,
            Attacking
        }

        [Header("Animators")]
        [SerializeField] private Animator bodyAnimator;
        [SerializeField] private Animator headAnimator;
        [Space(20)]


        private Shooting shootingComponent;

        [SerializeField] private int maxHealth;
        private int health;
        private SState state;



        private void Awake()
        {
            shootingComponent = GetComponentInChildren<Shooting>();

            health = maxHealth;
            state = SState.Attacking;
        }

        public void OnDestroy()
        {
            Destroy(gameObject);
        }

        public void Hit(GameObject attacker, int damage, IHit.HitWeapon weapon = IHit.HitWeapon.OTHER)
        {
            if (state != SState.Smoking) return;

            health = Mathf.Clamp(health - damage, maxHealth, 0);

            if(health == 0)
            {
                shootingComponent.DeactivateAutoFire();
            }
        }

        public void StunHit(GameObject attacker, int damage, float stunTime, IHit.HitWeapon weapon = IHit.HitWeapon.OTHER)
        {
            Hit(attacker, damage, weapon);
        }

        public void Stun(GameObject attacker, float time, float strength = 1, IHit.HitWeapon weapon = IHit.HitWeapon.OTHER)
        {
            // Stun does nothing to this enemy
            return;
        }
    }
}