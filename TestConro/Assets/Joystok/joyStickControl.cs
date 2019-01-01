using UnityEngine;
using System.Collections;

public class joyStickControl : MonoBehaviour {

	public static float h=0;
	public static float v = 0;

	private float parentHeight;
	private float parentWidth;

	private bool isPress=false;

	UITexture parentSpirite;

	void Awake()
	{
		parentSpirite = transform.parent.GetComponent<UITexture>();
		parentWidth = parentSpirite.width;
		parentHeight = parentSpirite.height;
	}


	// Update is called once per frame
	void Update () {

		if (isPress)
		{
			Vector2 touchpos = UICamera.lastTouchPosition;

			touchpos -=new  Vector2(parentWidth / 2, parentHeight / 2);
			float distance = Vector2.Distance(touchpos, Vector2.zero);
			if(distance<53)
			{
				transform.localPosition = touchpos;
			}
			else
			{
				transform.localPosition = touchpos.normalized * 53;
			}

			h = transform.localPosition.x / 53;
			v = transform.localPosition.y / 53;

		}
		else
		{
			transform.localPosition = Vector2.zero;
			h = 0;
			v = 0;
		}

	}

	void OnPress(bool isPress)
	{
		this.isPress = isPress;
	}
}