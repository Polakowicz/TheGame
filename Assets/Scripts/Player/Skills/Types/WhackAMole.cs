using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhackAMole : Skill
{
	CircleCollider2D range;
	LayerMask mask;
	ContactFilter2D filter;

	[SerializeField]
	float radiusGrow;
	bool charging;

	void Start()
	{
		range = GetComponent<CircleCollider2D>();
		mask = LayerMask.GetMask("Enemy");
		filter = new ContactFilter2D {
			layerMask = mask,
			useLayerMask = true,
			useTriggers = true
		};

		range.radius = 0;
		charging = false;
	}

	void Update()
	{
		if (!charging) return;

		range.radius += radiusGrow * Time.deltaTime;
	}

	public override void StartUsingSkill()
	{
		charging = true;
	}

	public override void StopUsingSkill()
	{
		List<Collider2D> colliders = new List<Collider2D>();
		range.OverlapCollider(filter, colliders);
		foreach (Collider2D collider in colliders) {
			collider.GetComponent<Enemy>().Overthrow();
		}

		charging = false;
		range.radius = 0;
	}
}