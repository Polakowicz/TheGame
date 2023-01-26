using Scripts.Tools;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

namespace Scripts.Game
{
    public class StartCutscene : ExtendedMonoBehaviour
    {
        public PlayableDirector director;

        private void Start()
        {
            if(GameEventSystem.Instance.StartType == GameEventSystem.GameStartType.NewGame)
            {
                GameEventSystem.Instance.OnCutsceneStarted.Invoke();
                director.Play();
                StartCoroutine(WaitAndDo((float)director.duration, () => GameEventSystem.Instance.OnCutsceneEnded.Invoke()));
            }
        }
    }
}