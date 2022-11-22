using Scripts.Interfaces;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts.Player
{
	public class PlayerInteract : MonoBehaviour
	{
		// Components
		private PlayerInput input;
		private InputAction interactAction;
		private PlayerManager manager;

		// To find object to interact
		private Collider2D interactRange;

		private void Awake()
		{
			// Get components
			interactRange = GetComponent<Collider2D>();
			manager = GetComponentInParent<PlayerManager>();
			input = GetComponentInParent<PlayerInput>();

			// Set varibles
			interactAction = input.actions["Interact"];	
		}
		private void Start()
		{
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
				// Object must have Iinteract inteface to interact with it
				if (!hit.TryGetComponent<IInteract>(out var interactionInterface)) continue;

				var result = interactionInterface.Interact(gameObject);

				switch (result) {
					// Objact may not be active to interacte, ther return none interaction and do nothing and go to next object
					case Interaction.None:
						continue;

					case Interaction.Checkpoint:
						manager.AnimationController.Interact();
						manager.PlayerHealth.RestoreHP();
						break;
				}
			}
		}
	}
}