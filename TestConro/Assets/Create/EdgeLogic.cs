using UnityEngine;
using System.Collections;

public class EdgeLogic : MonoBehaviour {
	public int xLength;
	public int yLength;
	public float rato;

	public float getXSize()
	{
		return rato * xLength;
	}

	public float getYSize()
	{
		return rato * yLength;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
