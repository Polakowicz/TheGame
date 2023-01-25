using Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Enemies.Mechanics
{
    public class Tentacle : MonoBehaviour
    {
        [SerializeField] int damage = 10;
        [SerializeField] private Transform player;
        private Collider2D collider;
        private SpriteRenderer sprite;
        private Animator animator;
        private ContactFilter2D filter;

        private void Awake()
        {
            sprite = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            collider = GetComponent<Collider2D>();
            filter = new ContactFilter2D { 
                useTriggers = true,
                useLayerMask = true,
                layerMask = LayerMask.GetMask("Player")
            };
        }

        private void Update()
        {
            sprite.flipX = player.position.x > transform.position.x;
        }
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                animator.SetTrigger("Attack");
                
            }
        }

        public void Attack()
        {
            var hits = new List<Collider2D>();
            collider.OverlapCollider(filter, hits);
            foreach (var hit in hits)
            {
                hit.GetComponent<IHit>()?.Hit(gameObject, damage);
            }
        }
    }
}