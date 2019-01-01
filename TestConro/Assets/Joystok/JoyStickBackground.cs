using UnityEngine;
using System.Collections;

public class JoyStickBackground : MonoBehaviour
{

	public Transform thumb;

	private bool isReset = true;
	[SerializeField]
	protected Vector3 originPos = Vector3.zero;                 // 原点
	protected Vector3 offsetFromOrigin = Vector3.zero;          // 原点到拖拽位置的向量
	public Vector3 OffsetFromOrigin
	{
		set { offsetFromOrigin = value; }
		get { return offsetFromOrigin; }
	}
	// Use this for initialization
	void Start()
	{

	}

	private Vector3 lastoffsetFromOrigin = Vector3.zero;

	public Player t2c;
	// Update is called once per frame
	void Update()
	{
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
		if (pressed)
		{
			Ray ray = UICamera.currentCamera.ScreenPointToRay(UICamera.currentTouch.pos);
			float dist = 0f;
			Vector3 currentPos = ray.GetPoint(dist);
			currentPos =thumb.parent.worldToLocalMatrix.MultiplyPoint(currentPos);
			thumb.localPosition  = new Vector3(currentPos.x,currentPos.y,0);
			isReset = false;
			///< 更新当前坐标到原点的向量
			UpdateVector3FromOrigin(currentPos);
		}
		else
		{
			thumb.localPosition = Vector3.zero;
			isReset = true;
			UpdateVector3FromOrigin(thumb.localPosition);
		}
	}

	void OnDrag(Vector2 delta)
	{
		Ray ray = UICamera.currentCamera.ScreenPointToRay(UICamera.currentTouch.pos);
		float dist = 0f;

		Vector3 currentPos = ray.GetPoint(dist);
		currentPos =thumb.parent.worldToLocalMatrix.MultiplyPoint(currentPos);
		thumb.localPosition = new Vector3(currentPos.x,currentPos.y,0);
		UpdateVector3FromOrigin(currentPos);

	}

	///< 更新当前坐标到原点的向量
	void UpdateVector3FromOrigin(Vector3 pos)
	{
		OffsetFromOrigin = pos - originPos;
	}

	void OnDisable()
	{
		thumb.localPosition = Vector3.zero;
		isReset = true;
		UpdateVector3FromOrigin(thumb.localPosition);
	}

}
