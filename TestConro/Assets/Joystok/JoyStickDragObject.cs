using UnityEngine;
using System.Collections;

public class JoyStickDragObject : MonoBehaviour
{
	public Transform target;

	Vector3 mTargetPos;                                         // 目标当前位置
	Vector3 mLastPos;

	int mTouchID = 0;

	bool mStarted = false;
	bool mPressed = false;

	[SerializeField]
	protected Vector3 scale = new Vector3(1f, 1f, 0f);
	protected Vector3 originPos = Vector3.zero;                 
	protected Vector3 offsetFromOrigin = Vector3.zero;          // 原点到拖拽位置的向量

	public Vector3 OffsetFromOrigin
	{
		set { offsetFromOrigin = value; }
		get { return offsetFromOrigin; }
	}

	// Use this for initialization
	void Start () {
	}

	private bool needsend=false;

	private Vector3 lastoffsetFromOrigin = Vector3.zero;

	public Player t2c;
	// Update is called once per frame
	void Update () {
		if (OffsetFromOrigin == Vector3.zero) {//first zero mean stop move
			if(lastoffsetFromOrigin == Vector3.zero)
			{
				lastoffsetFromOrigin = OffsetFromOrigin;
				return;
			}
		}
		lastoffsetFromOrigin= OffsetFromOrigin;
		//EventManager.instance.Raise(new JoystickAxisEvent(OffsetFromOrigin.normalized.x,OffsetFromOrigin.normalized.y));
		t2c.OnJoystick(OffsetFromOrigin.normalized.x,OffsetFromOrigin.normalized.y);
	}

	void OnPress(bool pressed)
	{
		if (enabled && NGUITools.GetActive(gameObject) && target != null)
		{
			if (pressed)
			{
				if (!mPressed)
				{
					mTouchID = UICamera.currentTouchID;
					mPressed = true;
					mStarted = false;
					CancelMovement();
				}
			}
			else if (mPressed && mTouchID == UICamera.currentTouchID)
			{
				mPressed = false;
				target.localPosition = Vector3.zero;
				UpdateVector3FromOrigin(target.localPosition);
			}
		}

	}
	void OnDrag(Vector2 delta)
	{
		Ray ray = UICamera.currentCamera.ScreenPointToRay(UICamera.currentTouch.pos);
		float dist = 0f;

		Vector3 currentPos = ray.GetPoint(dist);
		currentPos =transform.parent.worldToLocalMatrix.MultiplyPoint(currentPos);

		///< 更新当前坐标到上一时刻坐标的向量
		Vector3 offset = currentPos - mLastPos;
		///< 更新当前坐标到原点的向量
		UpdateVector3FromOrigin(currentPos);
		mLastPos = currentPos;

		if (!mStarted)
		{
			mStarted = true;
			offset = Vector3.zero;
		}

		//Debug.LogWarning("offset: " + offset + " OffsetFromOrigin: " + OffsetFromOrigin);
		Move(offset);
	}

	void Move(Vector3 moveDelta)
	{
		mTargetPos += moveDelta;
		target.localPosition = mTargetPos;
	}

	void CancelMovement()
	{
		mTargetPos = (target != null) ? target.localPosition : Vector3.zero;
		UpdateVector3FromOrigin(mTargetPos);
	}

	///< 更新当前坐标到原点的向量
	void UpdateVector3FromOrigin(Vector3 pos)
	{
		OffsetFromOrigin = pos - originPos;

	}

	void OnDisable()
	{
		target.localPosition = Vector3.zero;
		UpdateVector3FromOrigin(target.localPosition);
	}
}
