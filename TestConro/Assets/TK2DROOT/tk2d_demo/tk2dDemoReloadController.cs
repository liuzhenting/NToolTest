using UnityEngine;
using System.Collections;

[AddComponentMenu("2D Toolkit/Demo/tk2dDemoReloadController")]
public class tk2dDemoReloadController : MonoBehaviour 
{
	void Reload()
	{
		Application.LoadLevel(Application.loadedLevel);
	}

	public CharacterController cc;
	public float step;
	void Up()
	{
		cc.Move (new Vector3(0,step,0));
	}

	void Down()
	{
		cc.Move (new Vector3(0,-1*step,0));
	}

	void Left()
	{
		cc.Move (new Vector3(-1*step,0,0));
	}

	void Right()
	{
		cc.Move (new Vector3(step,0,0));
	}



	public float walkSpeed = 2.0f;
	public float jumpHeight = 0.5f;
	public float gravity = 20.0f;
	// The current move direction in x-z
	private Vector3 moveDirection = Vector3.zero;
	// The current vertical speed
	private float verticalSpeed = 0.0f;
	// The current x-z move speed
	private float moveSpeed = 0.0f;
	CollisionFlags collisionFlags=CollisionFlags.None ;
	float CalculateJumpVerticalSpeed (float targetJumpHeight)
	{
		// From the jump height and gravity we deduce the upwards speed 
		// for the character to reach at the apex.
		return Mathf.Sqrt(2 * targetJumpHeight * gravity);
	}
	public float inAirControlAcceleration=3.0f;
	private Vector3 inAirVelocity = Vector3.zero;
	void UpdateSmoothedMovementDirection ()
	{
		if (IsGrounded ()) {

		} else {
			Vector3 targetDirection = new Vector3 (1,0,0)* moveSpeed;
			inAirVelocity += targetDirection.normalized * Time.deltaTime * inAirControlAcceleration;
		}
	}

	void ApplyJumping ()
	{
		if (IsGrounded ()) {
			// Jump
			// - Only when pressing the button down
			// - With a timeout so you can press the button slightly before landing	
			if (startjumpover) {
				jumping = false;
			} else {
				startjumpover = true;
				verticalSpeed = CalculateJumpVerticalSpeed (jumpHeight);
			}
		}
	}

	void ApplyGravity ()
	{

		if (IsGrounded ()) {
			verticalSpeed = 0.0f;
		} else {
				verticalSpeed -= gravity * Time.deltaTime;
		}
	}

	void Update()
	{
		ApplyGravity ();
		if(jumping)ApplyJumping ();
		var movement = new Vector3 (1, 0, 0)*moveSpeed + new Vector3 (0, verticalSpeed, 0)+inAirVelocity;
		movement *= Time.deltaTime;

		// Move the controller
		collisionFlags=cc.Move(movement);
	}

	bool IsGrounded () {
		return (collisionFlags & CollisionFlags.CollidedBelow) != 0;
	}

	bool jumping=false;
	bool startjumpover=true;
	public void StartJump()
	{
		//if (jumping)
			//return;
		jumping = true;
		startjumpover=false;
	}
		
	public void OnJoystick(float XAxis)
	{
		if (XAxis == 0) {
			moveSpeed = 0;
		}

		if (XAxis < 0) {
			moveSpeed = -1*walkSpeed;
		}

		if (XAxis > 0) {
			moveSpeed = 1*walkSpeed;
		}
	}
}
