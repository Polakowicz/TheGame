using Scripts.Game;
using System.Collections;
using UnityEngine;

namespace Scripts.Tools
{
    public class TestingScript : MonoBehaviour
    {
        private void Start()
        {
            GameEventSystem.Instance.OnAllPartsCollected += () => { Debug.Log("All parts collected"); };
        }
    }
}