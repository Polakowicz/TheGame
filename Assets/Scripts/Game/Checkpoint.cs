using Scripts.Interfaces;
using UnityEngine;
using UnityEngine.Assertions;

namespace Scripts.Game
{
	public class Checkpoint : MonoBehaviour, IInteract
	{
		private readonly string defaultName = "(Name of checkpont)";

		// Player respown position
		[SerializeField] private Transform respownPostion;
		public Transform RespownPosition { get => respownPostion; }

		// Name of the checkpoint
		[SerializeField] private string checkpointName;
		public string Name { get => checkpointName; }

		private void Awake()
		{
			Assert.AreNotEqual(checkpointName, defaultName, "Change name of checkpoint");
		}

		// Activation
		public Interaction Interact(GameObject sender)
		{
			GameEventSystem.Instance.OnCheckpointReached?.Invoke(this);
			return Interaction.Checkpoint;
		}
	}
}