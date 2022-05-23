using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
	//Components
	Rigidbody2D rb;
	[SerializeField] PlayerEventSystem eventSystem;
	[SerializeField] PlayerInput playerInput;

	//Input actions
	InputAction moveAction;
	InputAction dashAction;

	//Parameters
	[SerializeField] float basicSpeed = 5f;
	[SerializeField] float dashSpeed = 30f;
	[SerializeField] float dashTime = 0.1f;

	//Internal variables
	Vector2 direction;
	bool isInDash;
	float speed;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		moveAction = playerInput.actions["Move"];
		dashAction = playerInput.actions["Dash"];

		dashAction.performed += PerformDash;
		eventSystem.OnBladeThrustStarted += PerformThrustDash;
		eventSystem.OnBeamPullTowardsEnemyStarted += PerformBeamPull;
		eventSystem.OnKicked += PerformKicked;

		speed = basicSpeed;
	}

	void OnDestroy()
	{
		dashAction.performed -= PerformDash;
		eventSystem.OnBladeThrustStarted -= PerformThrustDash;
		eventSystem.OnBeamPullTowardsEnemyStarted -= PerformBeamPull;
		eventSystem.OnKicked -= PerformKicked;
	}

	void Update()
	{
		if (!isInDash) {
			direction = moveAction.ReadValue<Vector2>();
		}
	}

	void FixedUpdate()
	{
		if (isInDash) {
			rb.velocity = direction.normalized * speed;
		} else {
			rb.velocity = direction * speed * eventSystem.playerData.speedMultiplier;
		}

		eventSystem.playerData.moveDireciton= rb.velocity;
	}

	void PerformDash(InputAction.CallbackContext context)
	{
		if (isInDash) {
			return;
		}
		isInDash = true;
		speed = dashSpeed;

		eventSystem.OnDodge?.Invoke();
		StartCoroutine(DashDelay(dashTime));
	}
	void PerformThrustDash(PlayerData data, float s, float t, int d)
	{
		direction = data.aimDirection;
		isInDash = true;
		speed = s;
		StartCoroutine(TrustDelay(t));
	}
	void PerformBeamPull(GameObject enemy, float v, float stunTime)
	{
		direction = enemy.transform.position - transform.position;
		var s = direction.magnitude;
		speed = v;
		var t = s / v;
		isInDash = true;
		StartCoroutine(BeamPullDelay(t, stunTime));
	}
	void PerformKicked(Vector2 direction, float v, float s)
	{
		var t = s / v;
		this.direction = direction;
		isInDash=true;
		speed = v;
		StartCoroutine(DashDelay(t));
	}

	IEnumerator DashDelay(float delay)
	{
		yield return new WaitForSeconds(delay);
		speed = basicSpeed;
		isInDash = false;
	}
	IEnumerator TrustDelay(float delay)
	{
		yield return new WaitForSeconds(delay);
		speed = basicSpeed;
		isInDash = false;
		eventSystem.EndBladeThrust();
	}
	IEnumerator BeamPullDelay(float delay, float stunTime)
	{
		yield return new WaitForSeconds(delay);
		speed = basicSpeed;
		isInDash = false;
		eventSystem.EndBeamPullTowardsEnemy(stunTime);
	}
}
