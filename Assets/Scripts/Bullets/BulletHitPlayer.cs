using Scripts.Interfaces;
using System.Collections;
using UnityEngine;

public class BulletHitPlayer : MonoBehaviour
{
	[SerializeField] int damage = 5;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Player")) {
            return;
        }

        if (collision.isTrigger) {
            return;
        }

        IHit player = collision.gameObject.GetComponent<IHit>();
        player.Hit(damage, IHit.HitWeapon.OTHER);
        Destroy(gameObject);
        
    }
}