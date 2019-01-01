using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
public class CreateLogic : MonoBehaviour {
	public float rato;
	public float centeroffset;
	public int xlenth;
	public int ylenth;
	public string type="start";
	public string stageName="test";
	public tk2dSprite spritetest ;
	private const string prefabPath="Assets/Resources/Stages/";
	public int level=1;
	private StageLogic Stage; 
	private GameObject Edge;

	public void CreateStage()
	{
		GameObject obj = new GameObject ();
		obj.transform.parent = null;
		obj.name="Stage";
		Stage=obj.AddComponent<StageLogic> ();

		CreateLeft (xlenth, ylenth, 1);
		CreateRight (xlenth, ylenth, 1);
		CreateTop (xlenth, ylenth, 1);
		CreateBottom (xlenth, ylenth, 1);
		CreateBackGround (xlenth, ylenth, 1,"bg",Stage.transform);
		Stage.OpenAllDoor ();
		Stage.xLength = xlenth;
		Stage.yLength = ylenth;
	
	}

	public void SaveStage()
	{
		string foldPath = prefabPath +"level_"+ level.ToString ();
		if (!Directory.Exists(foldPath))
		{
			Directory.CreateDirectory(foldPath);
		}
		string prefabName = string.Format ("{1}_{0}_{2}x{3}",stageName,type,xlenth,ylenth);
		Stage.gameObject.name = prefabName;
		PrefabUtility.CreatePrefab (foldPath+"/"+prefabName+".prefab",Stage.gameObject);
	}

	public void DeleteStage()
	{
		GameObject.DestroyImmediate (Stage.gameObject);
		Stage = null;
	}

	public void CreateHEdge()
	{
		Edge = new GameObject ();
		Edge.transform.parent = null;
		Edge.name="hEdge";

		float h = 1;
		int xreal = 30 / 2;
		float hreal = h  * rato;
		for (int i = xreal*-1; i <= xreal; i++) {
			float px = hreal*i;
			float py = hreal*-1.5f;
			GameObject wall=CreateItem (new Vector3(px,py,0f),h,"crate",Edge.transform,10,false);
		}

		for (int i = xreal*-1; i <= xreal; i++) {
			float px = hreal*i;
			float py = hreal*1.5f;
			GameObject wall=CreateItem (new Vector3(px,py,0f),h,"crate",Edge.transform,10,false);
		}
	}

	public void CreateVEdge()
	{
		Edge = new GameObject ();
		Edge.transform.parent = null;
		Edge.name="vEdge";

		float h = 1;
		int y = 15;
		float hreal = h  * rato;
		for (int i = 1; i < y - 1; i++) {
			float px = hreal * -1f;
			float py = 0.5f * hreal + i * hreal-y/2*hreal;
			GameObject wall=CreateItem (new Vector3(px,py,0f),h,"crate",Edge.transform,10,false);
		}
		for (int i = 1; i < y - 1; i++) {
			float px = hreal * 1f;
			float py = 0.5f * hreal + i * hreal-y/2*hreal;
			GameObject wall=CreateItem (new Vector3(px,py,0f),h,"crate",Edge.transform,10,false);
		}
	}

	public void SaveEdge()
	{
		string foldPath = prefabPath +"level_"+ level.ToString ();
		if (!Directory.Exists(foldPath))
		{
			Directory.CreateDirectory(foldPath);
		}
		string prefabName = Edge.name;
		PrefabUtility.CreatePrefab (foldPath+"/"+prefabName+".prefab",Edge.gameObject);
	}

	public void DeleteEdge()
	{
		GameObject.DestroyImmediate (Edge);
		Edge = null;
	}

	public GameObject CreateItem(Vector3 pos, float h,string spritename,Transform parent,int sortorder=21,bool hasCollider=true)
	{
		GameObject obj = new GameObject ();
		obj.transform.parent = parent;

		tk2dSprite sprite=obj.AddComponent<tk2dSprite> ();
		if (hasCollider) {
			BoxCollider collider = obj.AddComponent<BoxCollider> ();

		}
		sprite.SetSprite (spritetest.Collection,spritename);
		sprite.scale = new Vector3 (h,h,h);
		sprite.SortingOrder = sortorder;
		sprite.transform.localPosition =pos;

		if (!hasCollider){
			BoxCollider collider = obj.GetComponent<BoxCollider> ();
			if (collider != null)
				DestroyImmediate (collider);
		}
		return obj;
	}

	public void CreateLeft(int x,int y,float h)
	{
		GameObject obj = new GameObject ();
		obj.transform.parent = Stage.gameObject.transform;
		obj.name = "left";
		float hreal = h * rato;

		DoorComponent door = CreateDoor (eDoorType.Right,new Vector3(hreal*0.1f,hreal*2));
		door.transform.parent = obj.transform;

		door.transform.localPosition = new Vector3 ((x /2-1)*hreal * -1f,hreal*1.5f-centeroffset,0);
		door.mWallObjects = new System.Collections.Generic.List<GameObject> ();
		Stage.leftDoor = door;
		for (int i = 1; i < y - 1; i++) {
			float px = (x /2)*hreal * -1f;
			float py = 0.5f * hreal + i * hreal-centeroffset;
			GameObject wall=CreateItem (new Vector3(px,py,0f),h,"crate",obj.transform);
			if (i == 1 || i==2) {
				door.mWallObjects.Add (wall);

			}
		}
	}

	public void CreateRight(int x,int y,float h)
	{
		GameObject obj = new GameObject ();
		obj.transform.parent = Stage.gameObject.transform;
		obj.name = "right";
		float hreal = h * rato;

		DoorComponent door = CreateDoor (eDoorType.Right,new Vector3(hreal*0.1f,2*hreal));
		door.transform.parent = obj.transform;

		door.transform.localPosition = new Vector3 ((x /2-1)*hreal * 1f,hreal*1.5f-centeroffset,0);
		door.mWallObjects = new System.Collections.Generic.List<GameObject> ();
		Stage.rightDoor = door;

		for (int i = 1; i < y - 1; i++) {
			float px = (x /2)*hreal * 1f;
			float py = 0.5f * hreal + i * hreal-centeroffset;
			GameObject wall=CreateItem (new Vector3(px,py,0f),h,"crate",obj.transform);
			if (i == 1 || i==2) {
				door.mWallObjects.Add (wall);

			}
		}
	}

	public void CreateTop(int x,int y,float h)
	{
		float hreal = h * rato;

		GameObject obj = new GameObject ();
		obj.transform.parent = Stage.gameObject.transform;
		obj.name = "top";

		DoorComponent door = CreateDoor (eDoorType.Top,new Vector3(2*hreal,hreal*0.1f));
		door.transform.parent = obj.transform;

		door.transform.localPosition = new Vector3 (0,(y-3.5f)*hreal-centeroffset,0);
		door.mWallObjects = new System.Collections.Generic.List<GameObject> ();
		Stage.topDoor = door;
		int xreal = x / 2;
		for (int i = xreal*-1; i <= xreal; i++) {
			float px = hreal*i;
			float py = (y-1)*hreal+hreal*0.5f-centeroffset;
			GameObject wall=CreateItem (new Vector3(px,py,0f),h,"crate",obj.transform);
			if (i == 0) {
				door.mWallObjects.Add (wall);

			}
		}
	}

	public void CreateBottom(int x,int y,float h)
	{
		float hreal = h * rato;
		GameObject obj = new GameObject ();
		obj.transform.parent = Stage.gameObject.transform;
		obj.name = "bottom";

		DoorComponent door = CreateDoor (eDoorType.Bottom,new Vector3(2*hreal,hreal*0.1f));
		door.transform.parent = obj.transform;

		door.transform.localPosition = new Vector3 (0,hreal*1.5f-centeroffset,0);
		door.mWallObjects = new System.Collections.Generic.List<GameObject> ();

		Stage.bottomDoor = door;

		int xreal = x / 2;
		for (int i = xreal*-1; i <= xreal; i++) {
			float px = hreal*i;
			float py = hreal*0.5f-centeroffset;
			GameObject wall=CreateItem (new Vector3(px,py,0f),h,"crate",obj.transform);
			if (i == 0) {
				door.mWallObjects.Add (wall);

			}
		}
			
	}

	public void CreateBackGround(int x,int y,float h,string spritename,Transform parent)
	{
		float hreal = h * rato;
		float wigth = hreal * x;
		float height = hreal * y;

		GameObject obj = new GameObject ();
		obj.transform.parent = parent;
		tk2dSprite sprite=obj.AddComponent<tk2dSprite> ();
		sprite.SetSprite (spritetest.Collection,spritename);
		sprite.scale = new Vector3 (1,1,1);
		sprite.SortingOrder = 20;
		sprite.transform.localPosition =Vector3.zero;

	}

	public DoorComponent CreateDoor(eDoorType type,Vector3 size)
	{
		GameObject obj = new GameObject ();
		obj.name="door";
		obj.layer = LayerMask.NameToLayer ("door");
		BoxCollider collider=obj.AddComponent<BoxCollider> ();
		collider.size = size;

		DoorComponent door=obj.AddComponent<DoorComponent> ();
		door.mDoorType = type;
		return door;
	}
}
