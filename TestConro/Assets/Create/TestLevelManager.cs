using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class TestLevelManager : MonoBehaviour {
	public LevelManager levelManager;
	public tk2dSprite spritetest ;
	public float rato;
	public float centeroffset;
	public GameObject root;
	// Use this for initialization
	void Start () {
		levelManager = new LevelManager ();
		levelManager.Init ();
		LevelManager.width = 1;
		LevelManager.heigh = 0.6f;
		LevelManager.h1 = 0.3f;
		LevelManager.h2 = 0.05f;
		root = null;
	}

	public tk2dSprite CreateItem(Vector3 pos, float h,string spritename,Transform parent,string name)
	{
		GameObject obj = new GameObject ();
		obj.transform.parent = parent;
		obj.name = name;
		BoxCollider collider=obj.AddComponent<BoxCollider> ();
		tk2dSprite sprite=obj.AddComponent<tk2dSprite> ();
		sprite.SetSprite (spritetest.Collection,spritename);
		sprite.scale = new Vector3 (h,h,h);
		sprite.transform.localPosition =pos;
		return sprite;
	}

	public GameObject CreateHorizonEdge(Vector3 pos, float h,string spritename,Transform parent,string name)
	{
		GameObject obj = new GameObject ();
		obj.transform.parent = parent;
		obj.name = name;
		tk2dSprite sprite=obj.AddComponent<tk2dSprite> ();
		sprite.SetSprite (spritetest.Collection,spritename);
		sprite.scale = new Vector3 (h,0.1f,h);
		sprite.transform.localPosition =pos;
		sprite.color = Color.green;
		return obj;
	}

	public GameObject CreateVerticalEdge(Vector3 pos, float h,string spritename,Transform parent,string name)
	{
		GameObject obj = new GameObject ();
		obj.transform.parent = parent;
		obj.name = name;
		tk2dSprite sprite=obj.AddComponent<tk2dSprite> ();
		sprite.SetSprite (spritetest.Collection,spritename);
		sprite.scale = new Vector3 (0.1f,h,h);
		sprite.transform.localPosition =pos;
		sprite.color = Color.green;
		return obj;
	}

	public void CreateLevel()
	{		
		levelManager.Init ();
		levelManager.CreateLevel ();

		if (root != null)
			Destroy (root);
		root = new GameObject ();
		root.name = "LevelManager";
		root.transform.parent = null;


		List<LevelNode> nodes=levelManager.LevelNodes;
		for (int i = 0; i < nodes.Count; i++) {
			tk2dSprite nodeentity=CreateItem (nodes[i].pos, rato, "crate", root.transform, nodes[i].Index.ToString ());
			if (nodes [i].type==eNodeType.Start) {
				nodeentity.color = Color.gray;
			}
			if (nodes [i].type==eNodeType.End) {
				nodeentity.color = Color.cyan;
			}
		}

		List<LevelEdge> edges=levelManager.LevelEdges;
		for (int i = 0; i < edges.Count; i++) {
			if (edges [i].edgeType == eEdgeType.Horizon) {
				CreateHorizonEdge (edges[i].pos, 1, "crate", root.transform, nodes[i].Index.ToString ());
			} else {
				CreateVerticalEdge (edges[i].pos, 1, "crate", root.transform, nodes[i].Index.ToString ());
			}
		}
		levelManager.Print ();
	}
}
