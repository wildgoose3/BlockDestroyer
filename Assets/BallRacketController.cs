using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using Parameter;

public class BallRacketController : MonoBehaviour {

    public GameObject Debug;
    private float Move = 10;
    private float Jump = 0;
    private Rigidbody RB;

    // Use this for initialization
    void Start () {
        RB = GetComponent<Rigidbody>();
        Debug = GameObject.Find("Debug");
        Param.Moving = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        Debug.GetComponent<Text>().text = ""+RB.velocity.magnitude;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Move = -10;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            Move = 10;
        }
        else
        {
            Move = 0;
        }

        //Restart();

        if (Param.GameOver)
        {
            this.RB.velocity = new Vector3(0, 0, 0);
            //  this.transform.Translate(0, 0, 0, Space.World);
        }
        else if (Param.GameOver == false)
        {
            this.RB.velocity = new Vector3(Move, Jump, 0);

            if (Param.Moving == false)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (this.gameObject.tag == "Ball")
                    {
                        this.RB.velocity=new Vector3(12,12,0); 
                    }
                    Param.Moving = true;

                }
            }
        }
        if (this.gameObject.tag == "Ball")
        {
            if (Param.Moving==true)
            {
                if (RB.velocity.magnitude < 10.0f)
                {
                    RB.velocity = RB.velocity.normalized * 10;
                }
                if (RB.velocity.normalized.x > 0.95f)
                {
                    if (RB.velocity.normalized.y > 0)
                    {
                        RB.velocity = new Vector3(0.95f, 0.31225f).normalized * RB.velocity.magnitude;
                    }
                    else
                    {

                        RB.velocity = new Vector3(0.95f, -0.31225f).normalized * RB.velocity.magnitude;
                    }
                }
                else if (RB.velocity.normalized.y > 0.95f)
                {
                    if (RB.velocity.normalized.x > 0)
                    {
                        RB.velocity = new Vector3(0.31225f, 0.95f).normalized * RB.velocity.magnitude;
                    }
                    else
                    {
                        RB.velocity = new Vector3(-0.31225f, 0.95f).normalized * RB.velocity.magnitude;
                    }
                }
                else if (RB.velocity.normalized.x < -0.95f)
                {
                    if (RB.velocity.normalized.y > 0)
                    {
                        RB.velocity = new Vector3(-0.95f, 0.31225f).normalized * RB.velocity.magnitude;
                    }
                    else
                    {

                        RB.velocity = new Vector3(-0.95f, -0.31225f).normalized * RB.velocity.magnitude;
                    }
                }
                else if (RB.velocity.normalized.y < -0.95f)
                {
                    if (RB.velocity.normalized.x > 0)
                    {
                        RB.velocity = new Vector3(0.31225f, -0.95f).normalized * RB.velocity.magnitude;
                    }
                    else
                    {
                        RB.velocity = new Vector3(-0.31225f, -0.95f).normalized * RB.velocity.magnitude;
                    }
                }
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (this.gameObject.tag == "Player")
        {
            if (other.gameObject.name == "ExtendPrefab(Clone)")
            {
                if (transform.localScale.x < 5)
                {
                    transform.localScale = new Vector3(transform.localScale.x + 0.5f, transform.localScale.y, transform.localScale.z);
                }

            }
            if (other.gameObject.name == "ShrinkPrefab(Clone)")
            {
                if (transform.localScale.x > 1)
                {
                    transform.localScale = new Vector3(transform.localScale.x - 0.5f, transform.localScale.y, transform.localScale.z);
                }
            }
            /*if (other.gameObject.name == "SplitPrefab(Clone)")
            {
                GameObject Ball = Instantiate(ballPrefab) as GameObject;
                Ball.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.25f, 0);
            }*/
            if (other.gameObject.tag == "Item")
            {
                Destroy(other.gameObject);
            }
        }
    }
}
