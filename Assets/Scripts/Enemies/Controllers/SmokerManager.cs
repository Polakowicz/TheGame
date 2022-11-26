using Assets.Scripts.Enemies.Mechanics;
using Scripts.Interfaces;
using System.Collections;
using UnityEngine;

namespace Scripts.Enemies
{
    public class SmokerManager : MonoBehaviour, IHit
    {
        private static readonly string DieAnimationTrigger = "Die";

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

        [Header("Stage times")]
        [SerializeField] private float attackingTime;
        [SerializeField] private float smoakingTime;
        [Space(20)]

        private Shooting shootingComponent;
        private RotateTowards rotateTowardsComponent;

        [SerializeField] private Transform head;
        [SerializeField] private int maxHealth;
        private int health;
        private SState state;

        private void Awake()
        {
            shootingComponent = GetComponentInChildren<Shooting>();
            rotateTowardsComponent = GetComponentInChildren<RotateTowards>();

            health = maxHealth;
            state = SState.Waiting;
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        public void Hit(GameObject attacker, int damage, IHit.HitWeapon weapon = IHit.HitWeapon.OTHER)
        {
            if (state != SState.Smoking) return;

            health = Mathf.Clamp(health - damage, 0, maxHealth);

            if(health == 0)
            {
                StopAllCoroutines();
                shootingComponent.DeactivateAutoFire();
                rotateTowardsComponent.Active = false;

                bodyAnimator.SetTrigger(DieAnimationTrigger);
                headAnimator.SetTrigger(DieAnimationTrigger);
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

        public void Activate()
        {
            if(state != SState.Waiting) return;
            StartCoroutine(Attacking());
        }

        private IEnumerator Attacking()
        {
            state = SState.Attacking;
            shootingComponent.ActivateAutoFire();
            rotateTowardsComponent.Active = true;
            yield return new WaitForSeconds(attackingTime);
            StartCoroutine(Smoking());
        }

        private IEnumerator Smoking()
        {
            state = SState.Smoking;
            shootingComponent.DeactivateAutoFire();
            rotateTowardsComponent.Active = false;
            head.rotation = Quaternion.Euler(0, 0, 180);
            yield return new WaitForSeconds(smoakingTime);
            StartCoroutine(Attacking());
        }
    }
}