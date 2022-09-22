using Scripts.Interfaces;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Scripts.Game
{
	public class Checkpoint : MonoBehaviour, IInteract
	{
		
		public enum CheckpointName
		{
			Top,
			Left
		}

		// Player respown position
		[SerializeField] private Transform respownPostion;
		public Transform RespownPosition { get => respownPostion; }

		// Name of the checkpoint
		[SerializeField] private CheckpointName checkpointName;
		public CheckpointName Name { get => checkpointName; }


		public Interaction Interact(GameObject sender)
		{
			GameEventSystem.Instance.OnCheckpointReached?.Invoke(this);
			return Interaction.Checkpoint;
		}
	}
}