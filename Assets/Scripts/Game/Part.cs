using Scripts.Interfaces;
using System.Collections;
using UnityEngine;

namespace Scripts.Game
{
    public class Part : MonoBehaviour, IInteract
    {
        public enum PartType
        {
            Type1,
            Type2,
            Type3,
            Type4
        }

        [SerializeField] private PartType partType;
        public PartType Type { get => partType; }

        private void Awake()
        {
            if (GameEventSystem.Instance.SaveSystem.Data.collectedParts.Contains(Type))
            {
                // This part was already collected
                Destroy(gameObject);
            }
        }

        public Interaction Interact(GameObject sender)
        {
            GameEventSystem.Instance.OnPartCollected(Type);
            StartCoroutine(Destroy());
            return Interaction.None;    
        }

        private IEnumerator Destroy()
        {
            yield return new WaitForEndOfFrame();
            Destroy(gameObject);
        }
    }
}