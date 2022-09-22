using System.Collections;
using System.Linq;
using UnityEngine;

namespace Scripts.Game
{
	public class CheckpointsPositions : MonoBehaviour
	{
		public static CheckpointsPositions Instance;

		[SerializeField] private Checkpoint[] allCheckpoints;

		private void Awake()
		{
			if (Instance == null)
			{
				Instance = this;
			} else
			{
				Destroy(gameObject);
			}
		}

		public Checkpoint GetCheckpointFromName(Checkpoint.CheckpointName checkpointName)
		{
			return allCheckpoints.AsEnumerable().Where(x => x.Name == checkpointName).Single();
		}

	}
}