using Scripts.Enemies;
using Scripts.Game;
using System.Collections;
using UnityEngine;

namespace Scripts.Tools
{
    public class TestingScript : MonoBehaviour
    {
        [SerializeField] private SmokerManager smoker;

        private void Start()
        {
            GameEventSystem.Instance.OnAllPartsCollected += () => { Debug.Log("All parts collected"); };
            GameEventSystem.Instance.OnPartCollected += (part) => { Debug.Log($"Part collected: {part}");  };
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                smoker.Activate();
            }
            
        }
    }
}