using JetBrains.Annotations;
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

		public Checkpoint GetCheckpointFromName(string checkpointName)
		{
            foreach (var checkpoint in allCheckpoints)
            {
                if (checkpoint.name == checkpointName)
                {
                    return checkpoint;
                }
            }

			return null;
            //return allCheckpoints.AsEnumerable().Where(x => x.Name == checkpointName).Single();
		}

	}
}