using Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts.Player
{
	public class Interact : MonoBehaviour
	{
		private PlayerInput input;
		private InputAction interactAction;
		private PlayerManager manager;

		private Collider2D interactRange;

		private void Start()
		{
			interactRange = GetComponent<Collider2D>();
			manager = GetComponentInParent<PlayerManager>();
			input = GetComponentInParent<PlayerInput>();

			interactAction = input.actions["Interact"];
			interactAction.performed += PerformInteraction;
		}
		private void OnDestroy()
		{
			interactAction.performed -= PerformInteraction;
		}

		private void PerformInteraction(InputAction.CallbackContext context)
		{
			List<Collider2D> hits = new List<Collider2D>();
			interactRange.OverlapCollider(new ContactFilter2D().NoFilter(), hits);
			foreach(var hit in hits) {
				if (!hit.TryGetComponent<IInteract>(out var interactionInterface)) continue;
				var result = interactionInterface.Interact(gameObject);
				if (result == Interaction.None) continue;

				switch (result) {
					case Interaction.None:
						continue;

					case Interaction.Checkpoint:
						manager.AnimationController.Interact();
						break;
				}
			}
		}
	}
}