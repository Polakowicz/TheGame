using UnityEngine;

public class BulletHitEnemy : MonoBehaviour
{
    [SerializeField] int damage = 5;
    [SerializeField] bool piercing;

	void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.gameObject.layer != LayerMask.NameToLayer("Enemy")) {
            return;
        }

        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        enemy.Hit(damage);
		if (!piercing) {
            Destroy(gameObject);
        }
    }
}
