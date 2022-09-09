using UnityEngine;

namespace Scripts.Player
{
    public class Manager : MonoBehaviour
    {
        public AudioManager AudioManager { get; private set; }
        public AnimationController AnimationController { get; private set; }
        public PowerUpController PowerUpController { get; private set; }

        public enum PlayerState
        {
            Walk,
            Dash,
            Stun,
            Charging
        }
        [HideInInspector] public PlayerState State;
        [HideInInspector] public Vector2 AimDirection;
        [HideInInspector] public Vector2 MoveDirection;

		private void Awake()
		{
            PowerUpController = GetComponent<PowerUpController>();
        }

		private void Start()
        {
            AudioManager = FindObjectOfType<AudioManager>();
            AnimationController = GetComponentInChildren<AnimationController>();
            
        }
    }
}
