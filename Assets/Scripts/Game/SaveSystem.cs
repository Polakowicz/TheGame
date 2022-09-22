using Scripts.Game;
using System;
using System.Collections;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;


namespace Scripts.Game
{
	public class SaveSystem : MonoBehaviour
	{
		// List of  all checkpints
		[SerializeField] private Checkpoint[] checkpoints;

		[Serializable]
		public class SaveData
		{
			public int hp;
			public Checkpoint.CheckpointName checkpointName;
		}
		private SaveData data;


		private void Awake()
		{
			data = new SaveData();
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
			data.checkpointName = checkpoint.Name;
			SaveGame();
		}

		private void SaveGame()
		{
			XmlSerializer serializer = new XmlSerializer(typeof(SaveData));
			FileStream stream = new FileStream(Application.dataPath + "/../savedGame.xml", FileMode.Create);
			serializer.Serialize(stream, data);
			stream.Close();
		}
	}
}