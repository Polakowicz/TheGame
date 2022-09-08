using Scripts.Interfaces;
using UnityEngine;

namespace Scripts.Bullets
{
    public class BulletHitEnemy : MonoBehaviour
    {
        [SerializeField] private int damage = 5;
        [SerializeField] private bool piercing;

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Player")) {
                return;
            }

            if (collision.gameObject.layer == LayerMask.NameToLayer("Map")) {
                Destroy(gameObject);
			}

            if (collision.TryGetComponent(out IHit hit)) {
                hit.Hit(damage);
				if (!piercing) {
                    Destroy(gameObject);
				}
            }
        }

    }
}
