using Scripts.Game;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;
using static Scripts.Game.Part;

namespace Scripts.Game
{
	public class SaveSystem : MonoBehaviour
	{
		// Path where data will be saved
		private string filePath; 

		// Data to save in file
		public class SaveData
		{
            public int hp;
            public string checkpointName;
            public HashSet<PartType> collectedParts;

			public SaveData()
			{
				// Initialize objects
				collectedParts = new HashSet<PartType>();
			}
        }
		public SaveData Data;

		private void Awake()
		{
			DontDestroyOnLoad(gameObject);
			filePath = Application.dataPath + "/../savedGame.xml";
			Data = new SaveData();
		}
		private void Start()
		{
			GameEventSystem.Instance.OnCheckpointReached += SaveCheckPoint;
            GameEventSystem.Instance.OnPartCollected += CollectPart;

        }
		private void OnDestroy()
		{
			GameEventSystem.Instance.OnCheckpointReached -= SaveCheckPoint;
            GameEventSystem.Instance.OnPartCollected -= CollectPart;
        }


		// Implementation for subscribed events
		private void SaveCheckPoint(Checkpoint checkpoint)
		{
			Data.checkpointName = checkpoint.Name;
			SaveGame();
		}
        private void CollectPart(PartType type)
        {
            Data.collectedParts.Add(type);
            if (Data.collectedParts.Count == Enum.GetNames(typeof(PartType)).Length)
            {
                GameEventSystem.Instance.OnAllPartsCollected();
            }
        }


		// Save and Load data from and to file
        private void SaveGame()
		{
			XmlSerializer serializer = new XmlSerializer(typeof(SaveData));
			FileStream stream = new FileStream(filePath, FileMode.Create);
			serializer.Serialize(stream, Data);
			stream.Close();
		}
		public bool LoadGame()
		{
			if (!File.Exists(filePath))
			{
				return false;
			}

			XmlSerializer serializer = new XmlSerializer(typeof(SaveData));
			FileStream stream = new FileStream(filePath, FileMode.Open);
			SaveData loadedData = serializer.Deserialize(stream) as SaveData;
			stream.Close();

			if(loadedData == null)
			{
				return false;
			}

			Data = loadedData;
			return true;
		}
	}
}