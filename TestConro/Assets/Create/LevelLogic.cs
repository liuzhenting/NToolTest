using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum eNodeDirType
{
	left,
	right,
	top,
	bottom
}

public enum eEdgeType
{
	Vetical,
	Horizon,
}

public enum eNodeType
{
	Start,
	Boss,
	End,
	Normal,
}

public class LevelNode
{
	public int xIndex;
	public int yIndex;
	public bool left;
	public bool  right;
	public bool top;
	public bool bottom;
	public eNodeType type;
	public Vector3 pos;
	public LevelNode()
	{
		xIndex = 0;
		yIndex = 0;
		left = false;
		right = false;
		top = false;
		bottom = false;
		type = eNodeType.Normal;
	}

	public int Index
	{
		get{
			return yIndex*4+xIndex;

		}
		set {
			yIndex = value / 4;
			xIndex = value % 4;
			pos=GetPosition(LevelManager.width,LevelManager.heigh);
		}
	}

     /// <summary>
     /// jinjin shuiping Y=0
     /// </summary>
    public int Index2
    {
        get
        {
            return xIndex;

        }
        set
        {
            yIndex = 0;
            xIndex = Index2;
            pos = GetPosition(LevelManager.width, LevelManager.heigh);
        }
    }

    public List<int> GetAround()
	{
		List<int> arounds=new List<int>();
		if(xIndex + 1>=0&&xIndex + 1<4)arounds.Add(yIndex * 4 + xIndex + 1);
		if(xIndex - 1>=0&&xIndex - 1<4)arounds.Add(yIndex * 4 + xIndex - 1);
		if(yIndex + 1>=0&&yIndex + 1<4)arounds.Add((yIndex+1) * 4 + xIndex);
		if(yIndex - 1>=0&&yIndex - 1<4)arounds.Add((yIndex-1) * 4 + xIndex);
		return arounds;
	}
		

	public static eNodeDirType GetRelation(int IndexA ,int IndexB)
	{
		if ((IndexB - IndexA) / 4 > 0) {
			return eNodeDirType.top;
		}

		if ((IndexB - IndexA) / 4 < 0) {
			return eNodeDirType.bottom;
		}

		if ((IndexB - IndexA) % 4 > 0) {
			return eNodeDirType.right;
		}

		if ((IndexB - IndexA) % 4 < 0) {
			return eNodeDirType.left;
		}
		return eNodeDirType.left;
	}

	/// <summary>
	/// Gets the position.
	/// </summary>
	/// <returns>The position.</returns>
	/// <param name="width">Width. 格子的宽度</param>
	/// <param name="heigh">Heigh.格子的高</param>
	public Vector3 GetPosition(float width,float heigh)
	{
		float px = xIndex * width;
		float py = yIndex * heigh;
		return new Vector3 (px,py,0);
	}
}

public class LevelEdge
{
	public LevelNode start;
	public LevelNode end;
	public eEdgeType edgeType;
	public Vector3 pos;
	public LevelEdge(LevelNode A,LevelNode  B)
	{
		start = A;
		end = B;

		if (start.xIndex == end.xIndex) {
			edgeType = eEdgeType.Vetical;
		} else {
			edgeType = eEdgeType.Horizon;
		}
		pos=GetPosition(LevelManager.h1,LevelManager.h2);
	}

	/// <summary>
	/// Gets the position.
	/// </summary>
	/// <returns>The position.</returns>
	/// <param name="h1">H1.关卡的高</param>
	/// <param name="h2">H2.梯子高</param>
	public Vector3 GetPosition(float h1,float h2)
	{
		float px;
		float py;
		if (edgeType == eEdgeType.Horizon) {
			px=(start .pos.x+end.pos.x)/2;
			py = start.pos.y - h1 / 2 + h2 / 2;
		} else {
			px = start .pos.x;
			py = (start .pos.y+end.pos.y)/2;
		}
		return new Vector3 (px,py,0);
	}
}

public class LevelManager
{
	public static float width;
	public static float heigh;
	public static float h1;
	public static float h2;
	private List<LevelNode> levelNodes;
	private List<LevelEdge> levelEdges;
	private int step;
	private int stepindex;
	private int first;
	public List<LevelNode> LevelNodes
	{
		get{
			return levelNodes;
		}
	}

	public List<LevelEdge> LevelEdges
	{
		get{
			return levelEdges;
		}
	}

	public void Init()
	{
		levelNodes = new List<LevelNode> ();
		levelEdges = new List<LevelEdge> ();
		step = 0;
		stepindex = step;
	}

	public void CreateLevel()
	{
		step = Random.Range (6, 8);
		first = Random.Range (0,16);
		//int first =7;
		LevelNode tmp = new LevelNode ();
		tmp.Index = first;
		tmp.type = eNodeType.Start;
		levelNodes.Add (tmp);
		stepindex = step;
		step--;
		CreateNextLevelNode (tmp);
		if (step > 0) {
			Init ();
			CreateLevel ();
		}
	}



	public void CreateNextLevelNode(LevelNode last)
	{
		List<int> arounds = last.GetAround ();
		for (int i = arounds.Count-1; i >= 0; i--) {
			if (IsInLevelNodes (arounds [i])) {
				arounds.RemoveAt (i);
			}
		}
		if (arounds.Count <= 0)
			return;
		int select = Random.Range (0,arounds.Count);
		LevelNode node = new LevelNode ();
		node.Index = arounds[select];
		levelNodes.Add (node);
		select = node.Index;

		List<int> tmparounds = node.GetAround ();
		for (int i = 0; i < tmparounds.Count; i++) {
			int nodeindex=tmparounds [i];
			if (IsInLevelNodes (nodeindex)) {
				if (step == stepindex - 1 || ((step < stepindex - 1 && step > 1) && (nodeindex != first)) || ((step == 1) && (nodeindex == last.Index))) {
					eNodeDirType nodedirtype = LevelNode.GetRelation (nodeindex, select);
					LevelNode tmp = GetLevelNode (nodeindex);
					LevelEdge edge = new LevelEdge (node, tmp);
					levelEdges.Add (edge);
					switch(nodedirtype)
					{
					case eNodeDirType.right:
						{
							node.left = true;
							tmp.right = true;
							break;}
					case eNodeDirType.left:
						{
							node.right = true;
							tmp.left = true;
							break;}
					case eNodeDirType.top:
						{
							node.bottom = true;
							tmp.top = true;
							break;}
					case eNodeDirType.bottom:
						{
							tmp.bottom = true;
							node.top = true;
							break;
						}
					default:
						break;
					}
				}
			}
		}

		if (step == 1)
			node.type = eNodeType.End;
		step--;
		if (step > 0) {
			CreateNextLevelNode (node);
		}
	}

	public bool IsInLevelNodes(int index)
	{
		for (int i = 0; i < levelNodes.Count; i++) {
			if (levelNodes [i].Index == index) {
				return true;
			}
		}
		return false;
	}

	public LevelNode GetLevelNode(int index)
	{
		for (int i = 0; i < levelNodes.Count; i++) {
			if (levelNodes [i].Index == index) {
				return levelNodes [i];
			}
		}
		return null;
	}

    /// <summary>
    /// 创建一个水平的level
    /// </summary>
    public void CreateLevel2()
    {
        step = Random.Range(4, 6);
        first = 0;
        LevelNode tmp = new LevelNode();
        tmp.Index2 = first;
        tmp.left = false;
        tmp.right = true;
        tmp.type = eNodeType.Start;
        levelNodes.Add(tmp);

        for (int i=1;i<step;i++)
        {
            LevelNode node = new LevelNode();
            node.Index2 = i;
            if(i==step-1)
            {
                tmp.left = true;
                tmp.right = false;
                node.type = eNodeType.End;
            }
            else
            {
                tmp.left = true;
                tmp.right = true;
                node.type = eNodeType.Normal;
            }
            levelNodes.Add(node);

            LevelEdge edge = new LevelEdge(node, tmp);
            tmp = node;
            levelEdges.Add(edge);
        }

        Init();
        CreateLevel();
    }

    public void Print()
	{
		string str = "";
		for (int i = 0; i < levelNodes.Count; i++) {
			str=str+"==="+levelNodes[i].Index;
		}
		UnityEngine.Debug.LogError (str);
	}
}

public class EdgeInstance
{
	public GameObject gameObject;
	public void Load(LevelEdge levelEdge,int levelId)
	{
		gameObject=LevelResManager.Instance.GetEdge (levelId,levelEdge.edgeType);
		gameObject.transform.position = levelEdge.GetPosition (LevelManager.h1,LevelManager.h2);
	}

	public void UnLoad()
	{
		GameObject.Destroy (gameObject);
	}
}

public class StageInstance
{
	public StageLogic stageEntity;
	public void Load(LevelNode levelNode,int levelId,int stageId)
	{
		stageEntity=LevelResManager.Instance.GetStage (levelId,levelNode.type);
		stageEntity.gameObject.transform.position = levelNode.GetPosition (LevelManager.width,LevelManager.heigh);
		if (levelNode.type == eNodeType.Start) {
			stageEntity.gameObject.name="start";
		}
		if (levelNode.type == eNodeType.End) {
			stageEntity.gameObject.name="end";
		}
		if (levelNode.type == eNodeType.Boss) {
			stageEntity.gameObject.name="boss";
		}
		if (levelNode.type == eNodeType.Normal) {
			stageEntity.gameObject.name="normal";
		}
		stageEntity.InitDoorState (levelNode, stageId);
		stageEntity.OpenAllEnableDoor ();
	}

	public void UnLoad()
	{
		GameObject.Destroy (stageEntity.gameObject);
		stageEntity = null;
	}
}
public class LevelLogic : MonoBehaviour {
	public LevelManager levelManager;
	public List<StageInstance> stageList;
	public List<EdgeInstance> edgeList;
	public StageLogic currentStage;


	public void Init()
	{
		currentStage=null;
		levelManager = new LevelManager ();
		levelManager.Init ();
		LevelManager.width = 40*0.6f;
		LevelManager.heigh = 30*0.6f;
		LevelManager.h1 = 10*0.6f;
		LevelManager.h2 = 2*0.6f;
	}

	public void LoadLevel()
	{
		stageList = new List<StageInstance> ();
		edgeList = new List<EdgeInstance> ();
		levelManager.Init ();
        //levelManager.CreateLevel();
        levelManager.CreateLevel2 ();
		List<LevelNode> nodes=levelManager.LevelNodes;
		for (int i = 0; i < nodes.Count; i++) {
			StageInstance stage=new StageInstance ();
			stage.Load (nodes[i],1, i);
			stageList.Add (stage);
		}

		List<LevelEdge> edges=levelManager.LevelEdges;
		for (int i = 0; i < edges.Count; i++) {
			EdgeInstance edge=new EdgeInstance ();
			edge.Load (edges[i],1);
			edgeList.Add (edge);
		}
        EnterState(0);

    }

     /// <summary>
     /// 地图编辑器测试运行使用
     /// </summary>
     /// <param name="target"></param>
    public void PlaceOneStageForTest(StageLogic target)
    {
        stageList = new List<StageInstance>();
        edgeList = new List<EdgeInstance>();
        StageInstance stage = new StageInstance();
        stage.stageEntity = target;
        EnterState(0);
    }

	public void UnLoadLevel()
	{
		for (int i = 0; i < stageList.Count; i++) {
			stageList[i].UnLoad();
		}

		for (int i = 0; i < edgeList.Count; i++) {
			edgeList[i].UnLoad();
		}
		stageList = null;
		edgeList = null;
	}

	public Vector3 GetStartPos()
	{
		Vector3 pos= stageList [0].stageEntity.transform.position;
		pos = new Vector3 (pos.x-1,pos.y-4*0.6f,0);
		return pos;
	}

	public Vector3 GetCurrentStagePos()
	{
		if (currentStage != null) {
			Vector3 pos= currentStage.transform.position;
			return pos;
		}
		return Vector3.zero;
	}

    public void EnterState(int stageId)
    {
        currentStage = stageList[stageId].stageEntity;
        currentStage.OnEnter();
    }

    public void ExitState(int stageId)
    {
        if(currentStage!=null)
        {
            currentStage.OnExit();
        }
        currentStage = null;
    }
}
