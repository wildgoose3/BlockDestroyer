using UnityEngine;
using System.Collections;

using Parameter;

public class SPInput : MonoBehaviour {

    private Touch touch;
    private Vector2 StartPos;
    private Vector2 EndPos;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.touchCount != 0)
        {
            touch = Input.touches[0];
        }
        if (touch.phase == TouchPhase.Began)
        {
            StartPos = touch.position;
        }

        if (touch.phase == TouchPhase.Moved)
        {
            if (touch.deltaPosition.x > 1.0f)
            {
                Param.SPDirection = "right";
            }
            else if (touch.deltaPosition.x < -1.0f)
            {
                Param.SPDirection = "left";
            }
            else
            {
                Param.SPDirection = "";
            }
        }
        if (touch.phase == TouchPhase.Ended)
        {
            EndPos = touch.position;
            Param.SPDirection = "";
        }
        Param.FlickX = EndPos.x - StartPos.x;
        Param.FlickY = EndPos.y - StartPos.y;
      
    }
}
