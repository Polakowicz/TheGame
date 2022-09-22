using Scripts.Interfaces;
using System.Collections;
using UnityEngine;

namespace Scripts.Game
{
	public class Checkpoint : MonoBehaviour, IInteract
	{
		// Player respown position
		[SerializeField] private Transform respownPostion;
		public Transform RespownPosition { get => respownPostion; }

		public Interaction Interact(GameObject sender)
		{
			GameEventSystem.Instance.OnCheckpointReached?.Invoke(this);
			return Interaction.Checkpoint;
		}
	}
}