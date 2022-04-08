using UnityEngine;

public class BulletHitEnemy : MonoBehaviour
{
    [SerializeField] int damage = 5;

	void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.gameObject.layer != 16) {//16 = enemy
            return;
        }

        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        enemy.GetHit(damage);
        Destroy(gameObject);
	}
}
