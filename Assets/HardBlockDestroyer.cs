using UnityEngine;
using System.Collections;

using Parameter;

public class HardBlockDestroyer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Param.GameStatus == "CLEAR!!")
        {
            Destroy(this.gameObject);
        }
	}
}
