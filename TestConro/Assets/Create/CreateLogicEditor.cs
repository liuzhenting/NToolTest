using UnityEngine;
using System.Collections;
using UnityEditor;
[CustomEditor(typeof(CreateLogic))]
public class CreateLogicEditor :  Editor {

	private CreateLogic createLogic;
    private static string staticPrefabPath = "Assets/Resources/Prefabs/";
	void OnEnable()
	{
		//获取当前自定义的Inspector对象
		createLogic = (CreateLogic) target;

	}

	public override void OnInspectorGUI()
	{

		EditorGUILayout.Space();
		EditorGUILayout.Space();
		EditorGUILayout.LabelField("Base Info");
		createLogic.rato = EditorGUILayout.FloatField("Rato", createLogic.rato);
		createLogic.centeroffset = EditorGUILayout.FloatField("createLogic", createLogic.centeroffset);
		createLogic.xlenth = EditorGUILayout.IntField("xlenth", createLogic.xlenth);
		createLogic.ylenth = EditorGUILayout.IntField("ylenth", createLogic.ylenth);
		createLogic.type = EditorGUILayout.TextField("type", createLogic.type);
		createLogic.stageName = EditorGUILayout.TextField("stageName", createLogic.stageName);
		createLogic.level = EditorGUILayout.IntField("level", createLogic.level);
        createLogic.scene = EditorGUILayout.IntField("scene", createLogic.scene);
        createLogic.spritetest = (tk2dSprite)EditorGUILayout.ObjectField("spritetest", createLogic.spritetest,typeof(tk2dSprite));
		if(GUILayout.Button("CreateStage")){
			createLogic.CreateStage ();
		}

		EditorGUILayout.Space();
		EditorGUILayout.Space();
		if(GUILayout.Button("SaveStage")){
			createLogic.SaveStage ();
		}

		EditorGUILayout.Space();
		EditorGUILayout.Space();
		if(GUILayout.Button("DeleteStage")){
			createLogic.DeleteStage ();
		}

		EditorGUILayout.Space();
		EditorGUILayout.Space();

		EditorGUILayout.Space();
		EditorGUILayout.Space();
		if(GUILayout.Button("CreateHEdge")){
			createLogic.CreateHEdge ();
		}

		if(GUILayout.Button("CreateVEdge")){
			createLogic.CreateVEdge ();
		}
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		if(GUILayout.Button("SaveEdge")){
			createLogic.SaveEdge ();
		}
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		if(GUILayout.Button("DeleteEdge")){
			createLogic.DeleteEdge ();
		}

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        if (GUILayout.Button("TestStage"))
        {
            createLogic.TestStage();
        }

        PlaceDynamicObjectUI();
        PlaceStaticObjectUI();
    }

    void PlaceDynamicObjectUI()
    {
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("DynamicObject");
        //从一个文本加载 根据scene加载
        ArrayList Ids=new ArrayList();
        for(int i=0;i<Ids.Count;i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(Ids[i].ToString());//这里换成显示名字
            if (GUILayout.Button("Add"))
            {
                createLogic.AddDynamicObject((int)Ids[i]);
                //todo
            }
            EditorGUILayout.EndHorizontal();
        }
    }


    private ArrayList staticPrefabs;
    void PlaceStaticObjectUI()
    {
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("StaticObject");
        string LevelStaticPath = "Static_" + createLogic.scene;
        if (GUILayout.Button("FreshStaticPrefab"))
        {
            staticPrefabs= new ArrayList();
            string[] ids = AssetDatabase.FindAssets("t:Prefab", new string[] { staticPrefabPath + LevelStaticPath });
            for (int i = 0; i < ids.Length; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(ids[i]);
                GameObject originTwoCube = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)) as GameObject;
                staticPrefabs.Add(originTwoCube);
            }
            //todo
        }


        //从一个文件夹加载
        for (int i = 0; i < staticPrefabs.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(((GameObject)staticPrefabs[i]).name);
            if (GUILayout.Button("Add"))
            {
                createLogic.AddStaticObject((GameObject)staticPrefabs[i]);
                //todo
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}
