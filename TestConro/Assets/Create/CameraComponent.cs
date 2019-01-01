using UnityEngine;
using System.Collections;

public class CameraComponent : MonoBehaviour {
	public Player target;


	// Update is called once per frame
	void LateUpdate () {
		if (target != null) {
			Vector3 pos = Vector3.zero;
			if (target.Type == eLocationType.stage) {
				pos = new Vector3 (target.transform.position.x, GameLoop.Instance.level.GetCurrentStagePos ().y, -8.8f);
			}
			else if(target.Type == eLocationType.corridor)
			{
				pos = new Vector3 (target.transform.position.x, GameLoop.Instance.level.GetCurrentStagePos ().y, -8.8f);

			}
			else {
				float y = target.transform.position.y;
				pos = new Vector3 (target.transform.position.x,y, -8.8f);

			}

			transform.position = pos;
		}
	}
}
