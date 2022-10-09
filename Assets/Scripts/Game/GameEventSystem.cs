using System;
using System.Collections;
using UnityEngine;

namespace Scripts.Game
{
	public class GameEventSystem : MonoBehaviour
	{
		public static GameEventSystem Instance;

		public SaveSystem SaveSystem { get; private set; }

		public enum GameStartType
		{
			NewGame,
			LoadedGame,
		}
		public GameStartType StartType { get; set; }

		private void Awake()
		{
			if (Instance == null) {
				Instance = this;
				DontDestroyOnLoad(gameObject);
				SaveSystem = GetComponent<SaveSystem>();
			} else
			{
				Destroy(gameObject);
			}		
		}

		public Action OnPlayerDied;
		public Action<int> OnPlayerHPChanged;

		public Action<Checkpoint> OnCheckpointReached;
		public Action OnGameSaveLoaded;

		public Action OnEnemyKilled;
	}
}
