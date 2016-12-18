using UnityEngine;
using System.Collections;

using Parameter;
public class CameraControl : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        this.transform.position = new Vector3(-20, 0.5f, -18.5f);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Param.ExpPageNow == 0)
        {
            this.transform.position = new Vector3(0, 0.5f, -18.5f);
        }
        else
        {
            this.transform.position = new Vector3(-20, 0.5f, -18.5f);
        }
	}
}
