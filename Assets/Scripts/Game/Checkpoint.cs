using Scripts.Interfaces;
using UnityEngine;
using UnityEngine.Assertions;

namespace Scripts.Game
{
	public class Checkpoint : MonoBehaviour, IInteract
	{
		protected readonly string defaultName = "(Name of checkpont)";

		// Player respown position
		[SerializeField] protected Transform respownPostion;
		public Transform RespownPosition { get => respownPostion; }

		// Name of the checkpoint
		[SerializeField] protected string checkpointName;
		public string Name { get => checkpointName; }

		private void Awake()
		{
			Assert.AreNotEqual(checkpointName, defaultName, "Change name of checkpoint");
		}

		// Activation
		virtual public Interaction Interact(GameObject sender)
		{
			FindObjectOfType<AudioManager>().Play("Checkpoint");
			GameEventSystem.Instance.OnCheckpointReached?.Invoke(this);
			return Interaction.Checkpoint;
		}
	}
}