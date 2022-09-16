using Scripts.Interfaces;
using System.Collections;
using UnityEngine;

namespace Scripts.Bullets
{
    public class BulletHitPlayer : MonoBehaviour
    {
        [SerializeField] private int damage = 5;

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Map")) {
                Destroy(gameObject);
            }

            if (collision.isTrigger) return;
            if (collision.gameObject.layer != LayerMask.NameToLayer("Player")) return;
  
            IHit player = collision.gameObject.GetComponent<IHit>();
            player.Hit(gameObject, damage, IHit.HitWeapon.OTHER);
            Destroy(gameObject);
        }
    }
}