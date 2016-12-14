using UnityEngine;
using System.Collections;

using Parameter;

public class BottomController : MonoBehaviour {
    private AudioSource BallFall;

    // Use this for initialization
    void Start () {
        BallFall= GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            Param.BallRest -= 1;
            if (Param.BallRest <= 0)
            {
                BallFall.PlayOneShot(BallFall.clip);
            }
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Item")
        {
            Destroy(other.gameObject);
        }
    }
}
