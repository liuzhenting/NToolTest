using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public enum eDoorType
{
	Left,
	Right,
	Top,
	Bottom
}

public class DoorComponent : MonoBehaviour {

	public List<GameObject> mWallObjects;
	public List<GameObject> mDoorObjects;
	public eDoorType mDoorType;
	private bool mDoorCloseState;
	private bool mDoorDiableState=false;
	public bool DoorDiableState
	{
		get{
			return mDoorDiableState;
		}
	}

	public bool DoorCloseState
	{
		get{
			return mDoorCloseState;
		}
	}
	public void CloseDoor()
	{
		mDoorCloseState = true;
		if (mWallObjects != null) {
			for (int i = 0; i < mWallObjects.Count; i++) {
				mWallObjects [i].SetActive (true);
			}
		}

		if (mDoorObjects != null) {
			for (int i = 0; i < mDoorObjects.Count; i++) {
				mDoorObjects [i].SetActive (false);
			}
		}
	}

	public void OpenDoor()
	{
		mDoorCloseState = false;
		if (mWallObjects != null) {
			for (int i = 0; i < mWallObjects.Count; i++) {
				mWallObjects [i].SetActive (false);
			}
		}

		if (mDoorObjects != null) {
			for (int i = 0; i < mDoorObjects.Count; i++) {
				mDoorObjects [i].SetActive (true);
			}
		}
	}

	public void SetDoorState(bool state)
	{
		mDoorDiableState = !state;
		mDoorCloseState = true;
		if (mWallObjects != null) {
			for (int i = 0; i < mWallObjects.Count; i++) {
				mWallObjects [i].SetActive (!state);
			}
		}

		if (mDoorObjects != null) {
			for (int i = 0; i < mDoorObjects.Count; i++) {
				mDoorObjects [i].SetActive (state);
			}
		}
	}

	public void OnTriggerEnter()
	{
		if (mDoorDiableState) {
			return;
		}
		if (mDoorCloseState) {//now is inbattle
			return;
		}
		GameLoop.Instance.player.OnEnterDoorTrigger (this);
	}

	public void OnTriggerExit()
	{
		if (mDoorDiableState) {
			return;
		}
		if (mDoorCloseState) {
			return;
		}
		//GameLoop.Instance.player.OnExitDoorTrigger (this);
	}
		
}
