using Scripts.Interfaces;
using Scripts.Tools;
using System.Collections;
using UnityEngine;

namespace Scripts.Bullets
{
	public class Bullet : MonoBehaviour
	{
		protected ObjectPool bulletPool;

		[SerializeField] protected int damage;

		// Piercing bullet dont disapear after hiting enemy
		[SerializeField] protected bool isBulletPiercing;
		[SerializeField] private float speed;
		[SerializeField] protected LayerMask hitLayerMask;
		public float Speed { get => speed; }

		private void Awake()
		{
			bulletPool = GetComponentInParent<ObjectPool>();
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if(collision.gameObject.layer == LayerMask.NameToLayer("Map"))
			{
				bulletPool.DisposeObject(gameObject);
				return;
			}

			if(hitLayerMask == (hitLayerMask | (1 << collision.gameObject.layer))){
				Damage(collision);
				if (!isBulletPiercing)
				{
					bulletPool.DisposeObject(gameObject);
				}
			}
		}

		protected virtual void Damage(Collider2D collision)
		{
			if (collision.TryGetComponent(out IHit hit))
			{
				hit.Hit(gameObject, damage);
			}
		}
	}
}