using Scripts.Game;
using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;


namespace Scripts.Game
{
	public class SaveSystem : MonoBehaviour
	{
		// Path where data will be saved
		private string filePath; 

		// List of  all checkpints
		//[SerializeField] private Checkpoint[] allCheckpoints;
		//[SerializeField] private GameObject playerPrefab;

		// Data to save in file
		[Serializable]
		public class SaveData
		{
			public int hp;
			public Checkpoint.CheckpointName checkpointName;
		}
		public SaveData Data;

		// Active checkpoint
		//public Checkpoint Checkpoint { get; private set; }

		private void Awake()
		{
			DontDestroyOnLoad(gameObject);
			filePath = Application.dataPath + "/../savedGame.xml";
			Data = new SaveData();
		}
		private void Start()
		{
			GameEventSystem.Instance.OnCheckpointReached += SaveCheckPoint;
		}
		private void OnDestroy()
		{
			GameEventSystem.Instance.OnCheckpointReached -= SaveCheckPoint;
		}

		private void SaveCheckPoint(Checkpoint checkpoint)
		{
			Data.checkpointName = checkpoint.Name;
			SaveGame();
		}

		private void SaveGame()
		{
			XmlSerializer serializer = new XmlSerializer(typeof(SaveData));
			FileStream stream = new FileStream(filePath, FileMode.Create);
			serializer.Serialize(stream, Data);
			stream.Close();
		}

		public void LoadGame()
		{
			XmlSerializer serializer = new XmlSerializer(typeof(SaveData));
			FileStream stream = new FileStream(filePath, FileMode.Open);
			SaveData loadedData = serializer.Deserialize(stream) as SaveData;
			stream.Close();

			if(loadedData == null)
			{
				Debug.LogError("Unable to laod saved game");
				return;
			}

			Data = loadedData;

			//Checkpoint = 
		}
	}
}