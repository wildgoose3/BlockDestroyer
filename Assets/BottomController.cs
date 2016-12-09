using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
            if (Param.BallRest <= 0)
            {
                Param.RacketRest -= 1;
                Param.Moving = false;
                /*if (Param.RacketRest <= 0)
                {
                    Param.GameOver =true;

                }*/
            }
        }
        if (other.gameObject.tag == "Item")
        {
            Destroy(other.gameObject);
        }
    }
}
