using UnityEngine;

namespace Scripts.Player
{
    public class Manager : MonoBehaviour
    {
        public AudioManager AudioManager { get; private set; }
        public AnimationController AnimationController { get; private set; }
        public PowerUpController PowerUpController { get; private set; }
        public Animator Animator { get; private set; }

        public enum PlayerState
        {
            Walk,
            Dash,
            Stun,
            Charging,
            Dead
        }
        [HideInInspector] public PlayerState State;
        [HideInInspector] public Vector2 AimDirection;
        [HideInInspector] public Vector2 MoveDirection;

		private void Awake()
		{
            PowerUpController = GetComponent<PowerUpController>();
			AudioManager = FindObjectOfType<AudioManager>();
			AnimationController = GetComponentInChildren<AnimationController>();
			Animator = GetComponentInChildren<Animator>();
		}
    }
}
