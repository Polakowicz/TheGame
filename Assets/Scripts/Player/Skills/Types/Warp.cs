using System.Collections;
using UnityEngine;

public class Warp : Skill
{
	CircleCollider2D range;
	LayerMask mask;
	ContactFilter2D filter;

	[SerializeField]
	float radiusGrow;
	[SerializeField]
	int maxEnemies;
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
}