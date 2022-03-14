using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAim : MonoBehaviour
{
	//Components
	PlayerInput playerInput;
	Rigidbody2D rb;
	[SerializeField] GameObject crosshair;

	//Input actions

	InputAction aimAction;
	//Parameters
	[SerializeField] float crosshairDistanc = 2f;
	[SerializeField] float gunDistance = 1f;

	//Properties
	public Vector2 GunPosition { get; private set; }
	public Quaternion BulletRotation { get; private set; }

	//Internal variables
	bool gamepad;

	void Start()
	{
		playerInput = GetComponent<PlayerInput>();
		rb = GetComponent<Rigidbody2D>();
		aimAction = playerInput.actions["Aim"];
		//playerInput.onControlsChanged += OnControlsChanged;
	}

	void OnDestroy()
	{
		//playerInput.onControlsChanged -= OnControlsChanged;
	}

	void Update()
	{
		Vector2 lookDirection;
		if (gamepad) {
			lookDirection = aimAction.ReadValue<Vector2>();
			//var aimDirection = aimAction.ReadValue<Vector2>();
			//if (aimDirection == Vector2.zero) {
			//	return;
			//}
			//crosshair.transform.localPosition = aimDirection.normalized * crosshairDistanc;

			//GunPosition = (Vector2)transform.position + aimDirection.normalized * gunDistance;
		} else {
			var mousePos = aimAction.ReadValue<Vector2>();
			lookDirection = (Vector2)Camera.main.ScreenToWorldPoint(mousePos) - (Vector2)transform.position;
			//crosshair.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(mousePos);

			//GunPosition = transform.position + crosshair.transform.localPosition.normalized * gunDistance;
		}
		float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
		rb.rotation = angle;
		GunPosition = crosshair.transform.position;


	}

	public void OnControlsChanged(PlayerInput input)
	{
		gamepad = input.currentControlScheme.Equals("Gamepad");
		//To change in future. It is public and made through the inspector because onControlsChange does not work
	}
}
