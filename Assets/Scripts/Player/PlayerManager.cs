using Scripts.Game;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts.Player
{
    public class PlayerManager : MonoBehaviour
    {
        public AudioManager AudioManager { get; private set; }
        public PlayerAnimationController AnimationController { get; private set; }
        public PowerUpController PowerUpController { get; private set; }
        public PlayerHealth PlayerHealth { get; private set; }

        private PlayerInput playerInput;

        public enum PlayerState
        {
            Walk,
            Dash,
            Stun,
            Charging,
            Dead,
            Cutscene,
        }

        // Data shared between components
        private PlayerState state;
        public PlayerState State { get => state; set {
                // Only this script can change from and to cutscene state
                if (value == PlayerState.Cutscene || state == PlayerState.Cutscene) return;

                state = value;
            } }
        [HideInInspector] public Vector2 AimDirection;
        [HideInInspector] public Vector2 MoveDirection;

		private void Awake()
		{
            PowerUpController = GetComponent<PowerUpController>();
			AudioManager = FindObjectOfType<AudioManager>();
			AnimationController = GetComponentInChildren<PlayerAnimationController>();
            playerInput = GetComponent<PlayerInput>();
            PlayerHealth= GetComponent<PlayerHealth>();
		}

        private void Start()
        {
            GameEventSystem.Instance.OnCutsceneStarted += SetCutsceneState;
            GameEventSystem.Instance.OnCutsceneEnded += EndCutsceneState;

			if (GameEventSystem.Instance.StartType == GameEventSystem.GameStartType.LoadedGame)
			{
				var checkpointName = GameEventSystem.Instance.SaveSystem.Data.checkpointName;
                //var ins = CheckpointsPositions.Instance;
                var position = CheckpointsPositions.Instance.GetCheckpointFromName(checkpointName);

                if(position != null)
                {
                    transform.position = position.RespownPosition.position;
                }
			}
		}

		private void OnDestroy()
		{
			GameEventSystem.Instance.OnCutsceneStarted -= SetCutsceneState;
			GameEventSystem.Instance.OnCutsceneEnded -= EndCutsceneState;
		}

		private void SetCutsceneState()
        {
            state = PlayerState.Cutscene;
            playerInput.DeactivateInput();
        }

        private void EndCutsceneState()
        {
            state = PlayerState.Walk;
            playerInput.ActivateInput();
        }

    }
}
