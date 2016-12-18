using UnityEngine;
using System.Collections;
using Parameter;

public class ItemDrop : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Param.Moving)
        {
            transform.Translate(0, -0.05f, 0, Space.World);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
