using UnityEngine;
using System.Collections;
using UnityEditor;
[CustomEditor(typeof(CreateLogic))]
public class CreateLogicEditor :  Editor {

	private CreateLogic createLogic;

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

	}
}
