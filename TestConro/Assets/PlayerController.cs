using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public tk2dDemoReloadController t2c;
	void OnTriggerEnter(Collider other)
	{
		t2c.StartJump ();
	}
}
