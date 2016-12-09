using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

using Parameter;

public class RacketController : MonoBehaviour
{
    private float Move=10;
    private float DefaultSize = 2;
    private Rigidbody RB;
    public GameObject BallPrefab;
    private float RestartTime = 3.0f;
    private float RestartWait = 0;

    // Use this for initialization
    void Start()
    {
        RB = GetComponent<Rigidbody>();
        Param.Moving = false;
        GameObject Ball = Instantiate(BallPrefab) as GameObject;
        Ball.transform.position = new Vector3(this.transform.position.x, this.transform.position.y+0.5f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Param.GameOver == false)
        {*/
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
            this.RB.velocity = new Vector3(0,0,0);    
            this.transform.Translate(Move / 100, 0, 0, Space.World);

            if (Param.BallRest <= 0)
            {
                GetComponent<MeshRenderer>().enabled = false;
                GetComponent<Collider>().enabled = false;

                RestartWait += Time.deltaTime;
                if (RestartWait > RestartTime)
                {
                    if (Param.RacketRest <= 0)
                    {
                        Param.GameOver = true;
                    }
                    else
                    {
                        this.transform.localScale = new Vector3(this.DefaultSize, 0.5f, 0.5f);
                        this.transform.position = new Vector3(0, -4, 0);
                        GetComponent<MeshRenderer>().enabled = true;
                        GetComponent<Collider>().enabled = true;

                        GameObject Ball = Instantiate(BallPrefab) as GameObject;
                        Param.BallRest = 1;
                        Ball.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.5f, 0);
                    }
                    RestartWait = 0;
                }
            }
        /*}*/
  
        if (Param.GameOver)
        {
          this.RB.velocity = new Vector3(0, 0, 0);
        }
            
    }
    
    void OnTriggerEnter(Collider other)
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
    void Restart()
    {

    }
}
