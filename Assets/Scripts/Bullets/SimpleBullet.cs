using UnityEngine;

public class SimpleBullet : MonoBehaviour
{
    //Components
    Rigidbody2D rb;

    //Parameters
    [SerializeField] float speed = 10f;
    [SerializeField] int damage = 5;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2 (-Mathf.Sin(Mathf.Deg2Rad * rb.rotation), Mathf.Cos(Mathf.Deg2Rad * rb.rotation)) * speed;
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.gameObject.layer != 16) return; //16 = enemy

        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        enemy.GetHit(damage);
        Destroy(gameObject);
	}
}
