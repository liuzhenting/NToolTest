using UnityEngine;
using System.Collections;
public enum eLocationType
{
	corridor,
	ladder,
	stage
}
public class Player : MonoBehaviour {

	public CharacterController cc;
	public float walkSpeed = 2.0f;
	public float jumpHeight = 0.5f;
	public float gravity = 20.0f;
	private Vector3 moveDirection = new Vector3 (1, 0, 0);
	private float verticalSpeed = 0.0f;
	private float moveSpeed = 0.0f;
	CollisionFlags collisionFlags=CollisionFlags.None ;
	public float inAirControlAcceleration=3.0f;
	private Vector3 inAirVelocity = Vector3.zero;

	void Awake()
	{
		cc.enabled = false;
	}

	public void StartGame()
	{
		cc.enabled = true;
	}

	float CalculateJumpVerticalSpeed (float targetJumpHeight)
	{
		// From the jump height and gravity we deduce the upwards speed 
		// for the character to reach at the apex.
		return Mathf.Sqrt(2 * targetJumpHeight * gravity);
	}

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
		if (mType != eLocationType.stage)
			return;
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
		if (mType != eLocationType.stage)
			return;
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

		// Move the controller
		if (mType == eLocationType.stage) {
			var movement = moveDirection*moveSpeed + new Vector3 (0, verticalSpeed, 0)+inAirVelocity;
			movement *= Time.deltaTime;
			collisionFlags = cc.Move (movement);
		} else {
			var movement = moveDirection * moveSpeed;
			movement *= Time.deltaTime;
			transform.Translate (movement);
		}
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
		if (mType == eLocationType.stage) {
			jumping = true;
			startjumpover=false;
		}
	}

	private eLocationType mType;
	public eLocationType Type
	{
		get{
			return mType;
		}
	}

	public void SetMoveType(eLocationType type)
	{
		mType = type;
		switch(type)
		{
		case eLocationType.corridor:
			moveDirection = new Vector3 (1, 0, 0);
			cc.enabled = false;
			verticalSpeed = 0;
			break;
		case eLocationType.ladder:
			moveDirection = new Vector3 (0, 1, 0);
			cc.enabled = false;
			verticalSpeed = 0;
			break;
		case eLocationType.stage:
			moveDirection = new Vector3 (1, 0, 0);
			cc.enabled = true;
			break;
			default:
			moveDirection = new Vector3 (1, 0, 0);
			break;
		}
	}

	//public void OnDownLadder()
	//{
	//	if(GameLoop.Instance.level.currentStage.bottomDoor.DoorCloseState)
	//	{
	//		return;
	//	}
	//	float dist=Vector3.Distance(transform.position,GameLoop.Instance.level.currentStage.bottomDoor.transform.position);
	//	if (dist < LevelManager.h2*0.3f) {
	//		if (mType == eLocationType.stage) {
	//			SetMoveType (eLocationType.ladder);
	//			transform.position = GameLoop.Instance.level.currentStage.bottomDoor.transform.position;
	//		}
	//	}
	//}

	public void OnJoystick(float XAxis,float yAxis)
	{
		if (mType == eLocationType.corridor) {
			if (XAxis == 0) {
				moveSpeed = 0;
			}

			if (XAxis < 0) {
				moveSpeed = -1*walkSpeed;
			}

			if (XAxis > 0) {
				moveSpeed = 1*walkSpeed;
			}
			return;
		}
		if (mType == eLocationType.ladder) {
			if (yAxis == 0) {
				moveSpeed = 0;
			}

			if (yAxis < 0) {
				moveSpeed = -1*walkSpeed;
			}

			if (yAxis > 0) {
				moveSpeed = 1*walkSpeed;
			}
		}

		if (mType == eLocationType.stage) {
			if (XAxis == 0) {
				moveSpeed = 0;
			}

			if (XAxis < 0) {
				moveSpeed = -1*walkSpeed;
			}

			if (XAxis > 0) {
				moveSpeed = 1*walkSpeed;
			}
			return;
		}
	}

	///// <summary>
	///// 触碰到门
	///// </summary>
	///// <param name="door">Door.</param>
	//public void OnEnterDoorTrigger( DoorComponent door)
	//{
	//	if (mType == eLocationType.corridor || mType == eLocationType.ladder) {
	//		//in logic
	//		SetMoveType(eLocationType.stage);
	//		GameLoop.Instance.level.currentStage = door.transform.parent.parent.GetComponent<StageLogic> ();
	//		transform.position = door.transform.position;
	//	} else {
	//		//out logic
	//		SetMoveType(eLocationType.corridor);
	//	}
	//}

	public void OnExitDoorTrigger( int StageId,bool toIn)
	{
        if(toIn)//进来
        {

        }
        else//出去
        {
            SetMoveType(eLocationType.corridor);
        }
    }

	public void OnHitTargetHeadTrigger()
	{

	}

	public void OnHitTargetBodyTrigger()
	{

	}
}
