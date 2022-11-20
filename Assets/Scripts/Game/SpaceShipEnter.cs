using Scripts.Interfaces;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Game
{
    public class SpaceShipEnter : Checkpoint
    {
        private static readonly string spaceShipSceneName = "ShipInside";

        public override Interaction Interact(GameObject sender)
        {
            GameEventSystem.Instance.OnCheckpointReached?.Invoke(this);
            SceneManager.LoadScene(spaceShipSceneName);
            return Interaction.None;
        }
    }
}