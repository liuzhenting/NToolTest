using UnityEngine;
using System.Collections;

public class StageLogic : MonoBehaviour {

	public DoorComponent leftDoor;
	public DoorComponent rightDoor;
    public Transform dynamicRoot;
    public Transform staticRoot;
	public bool clear = false;
	public int xLength;
	public int yLength;
	public eNodeType type;
    public int stageId;
	public void InitDoorState(LevelNode node,int stageId)
	{
        leftDoor.mStageId = stageId;
        rightDoor.mStageId = stageId;
        leftDoor.SetDoorState (node.left);
		rightDoor.SetDoorState (node.right);
		type = node.type;
        this.stageId = stageId;
    }

	public void OnEnter()
	{
		if (clear) {
			return;
		} else {
			clear = true;
		}
	}

    public void OnExit()
    {

    }

    public void OpenAllDoor()
	{
		leftDoor.OpenDoor ();
		rightDoor.OpenDoor ();
	}

	public void OpenAllEnableDoor()
	{
		if (!leftDoor.DoorDiableState)
			leftDoor.OpenDoor ();
		if (!rightDoor.DoorDiableState)
			rightDoor.OpenDoor ();
	}

	public void CloseAllDoor()
	{
		leftDoor.CloseDoor ();
		rightDoor.CloseDoor ();
	}
}
