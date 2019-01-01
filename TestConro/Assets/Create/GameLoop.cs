using UnityEngine;
using System.Collections;

public class GameLoop : MonoBehaviour {
	public LevelLogic level;
	public Player player; 
	public bool inBattle;
	// Use this for initialization
	private static GameLoop instance;
	public static GameLoop Instance
	{
		get {
			return instance;
		}
	}

	void Awake()
	{
		instance = this;
	}

	void Start()
	{
		Init ();
	}

	void Init()
	{
		LevelResManager.Instance.LoadLevelAllRes (1);
		StartCoroutine (StartLevel());
	}

	IEnumerator StartLevel()
	{
		yield return new WaitForEndOfFrame ();
		level.Init ();
		level.LoadLevel ();
		player.transform.position=level.GetStartPos ();
		player.StartGame ();
		player.SetMoveType (eLocationType.stage);
		inBattle = false;
	}

	public void OnStartLevelClick()
	{

	}
}
