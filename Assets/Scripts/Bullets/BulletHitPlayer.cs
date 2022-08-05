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

        PlayerManager player = collision.gameObject.GetComponent<PlayerManager>();
        player.GiveDamage(damage);
        Destroy(gameObject);
        
    }
}