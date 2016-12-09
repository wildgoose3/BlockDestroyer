using UnityEngine;
using System.Collections;

using Parameter;

public class BallController : MonoBehaviour {
    private Rigidbody RB;
    private float speed = 25;
    private float Move;
    private GameObject DebugText;
    private bool Stick;

    // Use this for initialization
    void Start ()
    {
        RB=this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Param.Moving == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                this.RB.AddForce(speed * Mathf.Sin(Mathf.Rad2Deg * -50), speed * Mathf.Cos(Mathf.Rad2Deg * 50), 0);
                Param.Moving = true;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
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
                //this.RB.velocity = new Vector3(Move, 0, 0);
                this.transform.Translate(Move / 100, 0, 0, Space.World);
            
            if (this.transform.position.x > 2.5f)
            {
                this.transform.position = new Vector3(2.5f, this.transform.position.y, this.transform.position.z);
            }
            else if (this.transform.position.x < -2.5f)
            {
                this.transform.position = new Vector3(-2.5f, this.transform.position.y, this.transform.position.z);
            }

          

        }

        if (Param.Moving == true)
        {
            if (RB.velocity.magnitude < 10.0f)
            {
                RB.velocity = RB.velocity.normalized * 10;
            }
            else if (RB.velocity.magnitude >50.0f)
            {
                RB.velocity = RB.velocity.normalized * 50;
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
