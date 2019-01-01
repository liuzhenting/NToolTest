using UnityEngine;
using System.Collections;

public class StageLogic : MonoBehaviour {

	public DoorComponent leftDoor;
	public DoorComponent rightDoor;
	public DoorComponent topDoor;
	public DoorComponent bottomDoor;
	public bool clear = false;
	public int xLength;
	public int yLength;
	public eNodeType type;
	public void InitDoorState(LevelNode node)
	{
		leftDoor.SetDoorState (node.left);
		rightDoor.SetDoorState (node.right);
		topDoor.SetDoorState (node.top);
		bottomDoor.SetDoorState (node.bottom);
		type = node.type;

	}

	public void OnPlayerEnterStage()
	{
		if (clear) {
			return;
		} else {
			clear = true;
		}
	}

	public void OpenAllDoor()
	{
		leftDoor.OpenDoor ();
		rightDoor.OpenDoor ();
		topDoor.OpenDoor ();
		bottomDoor.OpenDoor ();
	}

	public void OpenAllEnableDoor()
	{
		if (!leftDoor.DoorDiableState)
			leftDoor.OpenDoor ();
		if (!rightDoor.DoorDiableState)
			rightDoor.OpenDoor ();
		if (!topDoor.DoorDiableState)
			topDoor.OpenDoor ();
		if (!bottomDoor.DoorDiableState)
			bottomDoor.OpenDoor ();
	}

	public void CloseAllDoor()
	{
		leftDoor.CloseDoor ();
		rightDoor.CloseDoor ();
		topDoor.CloseDoor ();
		bottomDoor.CloseDoor ();
	}
}
