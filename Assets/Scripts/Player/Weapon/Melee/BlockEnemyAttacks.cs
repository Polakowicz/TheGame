using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockEnemyAttacks : MonoBehaviour
{
    [SerializeField] PlayerEventSystem eventSystem;
	[SerializeField] LayerMask blockLayerMask;

	Collider2D blockCollider;
	ContactFilter2D contactFilter;

	bool blockEnabled;

	void Start()
	{
		blockCollider = GetComponent<Collider2D>();
		contactFilter = new ContactFilter2D {
			layerMask = blockLayerMask,
			useLayerMask = true,
			useTriggers = true
		};
		eventSystem.OnBladeBlockStarted += EnableBlock;
		eventSystem.OnBladeBlockEnded += DisableBlock;
	}

	void OnDestroy()
	{
		eventSystem.OnBladeBlockStarted -= EnableBlock;
		eventSystem.OnBladeBlockEnded -= DisableBlock;
	}

	void EnableBlock()
	{
		List<Collider2D> blocks = new List<Collider2D>();
		blockCollider.OverlapCollider(contactFilter, blocks);
		foreach (Collider2D block in blocks) {
			var redirectScript = block.GetComponent<BlockDirectionChange>();
			if (redirectScript != null) {
				redirectScript.Redirect();
			} else {
				Debug.LogError("Bullet has not BllockDirectionChangeScript");
			}
		}
		blockEnabled = true;
	}

	void DisableBlock()
	{
		blockEnabled = false;
		Debug.Log("Cancel");
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (!blockEnabled) {
			return;
		}

		if (!(blockLayerMask == (blockLayerMask | (1 << collision.gameObject.layer)))) {
			return;
		}
		
		Destroy(collision.gameObject);
	}
}
