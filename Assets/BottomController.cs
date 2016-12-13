using UnityEngine;
using System.Collections;

using Parameter;

public class BottomController : MonoBehaviour {

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            Param.BallRest -= 1;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Item")
        {
            Destroy(other.gameObject);
        }
    }
}
