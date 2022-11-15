using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Scripts.Game.Part;

namespace Scripts.Game
{
    [Serializable]
    public class SaveData : MonoBehaviour
    {
        public int hp;
        public string checkpointName;
        public HashSet<PartType> collectedParts;

        private void Awake()
        {
            collectedParts = new HashSet<PartType>();
        }

        private void Start()
        {
            GameEventSystem.Instance.OnPartCollected += CollectPart;
        }

        private void OnDestroy()
        {
            GameEventSystem.Instance.OnPartCollected -= CollectPart;
        }

        private void CollectPart(PartType type)
        {
            collectedParts.Add(type);
            if (collectedParts.Count == Enum.GetNames(typeof(PartType)).Length)
            {
                GameEventSystem.Instance.OnAllPartsCollected();
            }
        }
    }
}