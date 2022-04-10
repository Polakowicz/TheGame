using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrustAttackDealDamage : MonoBehaviour
{
	[SerializeField] PlayerEventSystem eventSystem;
	[SerializeField] LayerMask hitLayerMask;

	Collider2D hitCollider;
	ContactFilter2D contactFilter;

	bool attakcEnabled;
	int damage;

	void Start()
	{
		hitCollider = GetComponent<Collider2D>();
		contactFilter = new ContactFilter2D {
			layerMask = hitLayerMask,
			useLayerMask = true,
			useTriggers = true
		};
		eventSystem.OnBladeThrustStarted += EnableTriggerEnterDamage;
		eventSystem.OnBladeThrustEnded += DisableTriggerEnterDamage;
	}

	void OnDestroy()
	{
		eventSystem.OnBladeThrustStarted -= EnableTriggerEnterDamage;
		eventSystem.OnBladeThrustEnded -= DisableTriggerEnterDamage;
	}


	void EnableTriggerEnterDamage(PlayerData data, float speed, float time, int dmg)
	{
		damage = dmg;
		List<Collider2D> hits = new List<Collider2D>();
		hitCollider.OverlapCollider(contactFilter, hits);
		foreach (Collider2D hit in hits) {
			hit.GetComponent<Enemy>().GetHit(damage);		
		}
		attakcEnabled = true;
	}

	void DisableTriggerEnterDamage()
	{
		attakcEnabled = false;
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (!attakcEnabled) {
			return;
		}

		if (!(hitLayerMask == (hitLayerMask | (1 << collider.gameObject.layer)))) {
			return;
		}

		collider.GetComponent<Enemy>().GetHit(damage);
	}
}
