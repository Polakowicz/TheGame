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

        PlayerEventSystem player = collision.gameObject.GetComponent<PlayerEventSystem>();
        player.GiveDamage(damage);
        Destroy(gameObject);
        
    }
}