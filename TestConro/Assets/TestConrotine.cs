using UnityEngine;
using System.Collections;

public class TestConrotine : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (Run());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public IEnumerator Run()
	{
		UnityEngine.Debug.Log (UnityEngine.Time.frameCount.ToString());
		yield return null;
		UnityEngine.Debug.Log (UnityEngine.Time.frameCount.ToString());
		yield return 1;
		UnityEngine.Debug.Log (UnityEngine.Time.frameCount.ToString());
		yield return 0;
		UnityEngine.Debug.Log (UnityEngine.Time.frameCount.ToString());
		yield return new WaitForEndOfFrame();
		UnityEngine.Debug.Log (UnityEngine.Time.frameCount.ToString());
	}
}
