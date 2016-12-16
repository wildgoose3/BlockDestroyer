using UnityEngine;
using System.Collections;

using Parameter;
public class CameraControl : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        this.transform.position = new Vector3(-20, 1, -18);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Param.ExpPageNow == 0)
        {
            this.transform.position = new Vector3(0, 1, -18);
        }
        else
        {
            this.transform.position = new Vector3(-20, 1, -18);
        }
	}
}
