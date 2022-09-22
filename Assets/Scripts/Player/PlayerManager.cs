using Scripts.Game;
using UnityEngine;

namespace Scripts.Player
{
    public class PlayerManager : MonoBehaviour
    {
        public AudioManager AudioManager { get; private set; }
        public PlayerAnimationController AnimationController { get; private set; }
        public PowerUpController PowerUpController { get; private set; }

        public enum PlayerState
        {
            Walk,
            Dash,
            Stun,
            Charging,
            Dead
        }

        // Data shared between components
        [HideInInspector] public PlayerState State;
        [HideInInspector] public Vector2 AimDirection;
        [HideInInspector] public Vector2 MoveDirection;

		private void Awake()
		{
            PowerUpController = GetComponent<PowerUpController>();
			AudioManager = FindObjectOfType<AudioManager>();
			AnimationController = GetComponentInChildren<PlayerAnimationController>();
		}

        private void Start()
        {
            if(GameEventSystem.Instance.StartType == GameEventSystem.GameStartType.LoadedGame)
            {

				transform.position = Vector3.zero;
			}

		}

        private void LoadPosition()
        {
            Debug.Log("POSITION");
            
		}
    }
}
