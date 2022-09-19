using System;
using System.Collections;
using UnityEngine;

namespace Scripts.Game
{
	public class GameEventSystem : MonoBehaviour
	{
		public static GameEventSystem Instance;

		private void Awake()
		{
			if (Instance == null)
				Instance = this;
			else
				Destroy(gameObject);
		}

		public Action OnPlayerDied;
		public Action<int> OnPlayerHPChanged;

		public Action<Checkpoint> OnCheckpointReached;
	}
}
