using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelResManager : MonoBehaviour {

	private GameObject start;
	private GameObject end;
	private GameObject hEdge;
	private GameObject vEdge;
	private List<GameObject> normal;
	private int level=-1;
	private const string prefabPath="Stages/";

	private static LevelResManager instance;
	public static LevelResManager Instance
	{
		get {
			return instance;
			}
	}

	void Awake()
	{
		instance = this;
	}


	public void LoadLevelAllRes(int level)
	{
		if (level == this.level) {
			return;
		}
		normal = new List<GameObject> ();
		this.level = level;
		string foldPath = prefabPath +"level_"+ level.ToString ()+"/";
		GameObject[] objects=Resources.LoadAll <GameObject>(foldPath);
		for(int i=0;i<objects.Length;i++)
		{
			GameObject obj = objects [i] as GameObject;
			if (obj.name.StartsWith ("start")) {
				start = obj;
			}
			if (obj.name.StartsWith ("end")) {
				end = obj;
			}
			if (obj.name.StartsWith ("normal")) {
				normal.Add (obj);
			}
			if (obj.name.StartsWith ("vEdge")) {
				vEdge = obj;
			}
			if (obj.name.StartsWith ("hEdge")) {
				hEdge = obj;
			}

		}
	}

	public EdgeLogic GetEdge(int levelId,eEdgeType type)
	{
		if (type == eEdgeType.Horizon) {
			return GameObject.Instantiate (hEdge).GetComponent<EdgeLogic>();
		}

		if (type == eEdgeType.Vetical) {
			return GameObject.Instantiate (vEdge).GetComponent<EdgeLogic>();
		}

		return null;
	}

	public StageLogic GetStage(int levelId,eNodeType type)
	{
		if (type == eNodeType.Start) {
			return GameObject.Instantiate (start).GetComponent<StageLogic>();
		}

		if (type == eNodeType.End) {
			return GameObject.Instantiate (end).GetComponent<StageLogic>();
		}

		if (type == eNodeType.Boss) {
			return GameObject.Instantiate (end).GetComponent<StageLogic>();
		}

		if (type == eNodeType.Normal) {
			int index=Random.Range(0,normal.Count);
			return GameObject.Instantiate (normal[index]).GetComponent<StageLogic>();
		}
		return null;
	}
}
