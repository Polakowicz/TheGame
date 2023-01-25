using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Scripts.Game.Part;

namespace Scripts.Game
{
	public class GameEventSystem : MonoBehaviour
	{
		public static GameEventSystem Instance;

        public bool isCutsceneActive = false;

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

            OnCutsceneStarted += () => isCutsceneActive = true;
            OnCutsceneEnded += () => isCutsceneActive = false;
        }

		public void RestartGame()
		{ 
			StartType = string.IsNullOrEmpty(SaveSystem.Data.checkpointName) ?
				GameStartType.LoadedGame : GameStartType.NewGame;

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public Action OnPlayerDied;
		public Action<int> OnPlayerHPChanged;

		public Action<Checkpoint> OnCheckpointReached;
		public Action OnGameSaveLoaded;

		public Action OnEnemyKilled;

		public Action OnCutsceneStarted;
		public Action OnCutsceneEnded;

		public Action<PartType> OnPartCollected;
		public Action OnAllPartsCollected;
	}
}
