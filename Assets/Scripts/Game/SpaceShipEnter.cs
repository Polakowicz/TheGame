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
            if (FindObjectOfType<InteractionSystem>().pickedItems.Count == 5) {
                    SceneManager.LoadScene("EndingCutscene");
                    return Interaction.None;
                } else {
                    GameEventSystem.Instance.OnCheckpointReached?.Invoke(this);
                    SceneManager.LoadScene(spaceShipSceneName);
                    return Interaction.None;
                }
        }
    }
}