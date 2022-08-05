using Scripts.Interfaces;
using UnityEngine;


public class BulletHitEnemy : MonoBehaviour
{
    [SerializeField] int damage = 5;
    [SerializeField] bool piercing;

	void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.Damage(damage);
            if (!piercing) {
                Destroy(gameObject);
            }
        } else if (collision.gameObject.layer == LayerMask.NameToLayer("Boss")){
            collision.gameObject.GetComponent<IHit>()?.Hit(damage, IHit.HitWeapon.Bullet);
            if (!piercing) {
                Destroy(gameObject);
            }
        }

       
		
    }
}
