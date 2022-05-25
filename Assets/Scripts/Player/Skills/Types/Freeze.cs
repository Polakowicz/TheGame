using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Freeze : Skill
{
	Collider2D range;
	LayerMask mask;
	ContactFilter2D filter;

	[SerializeField]
	float freezTime;

	[SerializeField]
	float freezStrength;

	void Start()
	{
		range = GetComponent<Collider2D>();
		mask = LayerMask.GetMask("Enemy");
		filter = new ContactFilter2D {
			layerMask = mask,
			useLayerMask = true,
			useTriggers = true
		};
	}

	public Action UnfreezEnemy;

	public override void UseSkill()
	{
		List<Collider2D> colliders = new List<Collider2D>();
		range.OverlapCollider(filter, colliders);
		foreach(Collider2D collider in colliders) {
			collider.GetComponent<Enemy>().Freez(this, freezStrength);
		}

		StartCoroutine(UnfreezDelay());
	}

	private IEnumerator UnfreezDelay()
	{
		yield return new WaitForSeconds(freezTime);
		UnfreezEnemy?.Invoke();
	}
}
