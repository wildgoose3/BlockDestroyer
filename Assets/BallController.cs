using UnityEngine;
using System.Collections;

using Parameter;

public class BallController : MonoBehaviour {

    private Rigidbody RB;
    public GameObject BallPrefab;
    public Material DefaultBallColor;
    public Material Kamifusen;
    public Material Pierce;

    private float DefaultBallSize = 0.5f;
    private float DefaultBallPower = 1;
    private float SpeedMin = 5;
    private float SpeedMax = 30;
    private float VecAdj = 0.9f;
    private float VecAdj2;
    // Use this for initialization
    void Start ()
    {
        RB=this.GetComponent<Rigidbody>();
        VecAdj2 = Mathf.Sqrt(1 - VecAdj*VecAdj);
    }

    // Update is called once per frame
    void Update()
    {
        BallStatusChange();
        if (Param.BlockRest <= 0)
        {
            this.RB.velocity=new Vector3(0, 0, 0);
            this.transform.Translate(0, 0, 0, Space.World);
            Param.Moving = false;
            Destroy(this.gameObject);
        }

        if (Param.Moving == false)
        {
            BallRelease();
        }    

        if (Param.Moving == true)
        {
            VelocityAdjust();
            VectorAdjust();
            BallSplit();
        }
    }

    void BallSplit()
    {
        if (Param.Split)
        {
            for (int i = 1; i <= 2; i++)
            {
                GameObject Ball = Instantiate(BallPrefab) as GameObject;
                Param.BallRest += 1;
            }
            Param.Split = false;
        }
    }

    void BallStatusChange()
    {
        switch (Param.BallStatus)
        {
            case "Huge"://「巨」を取った時、ボールが通常の４倍に
                {
                    this.transform.localScale = new Vector3(2, 2, 2);
                    Param.BallPower = DefaultBallPower;
                    this.GetComponent<Renderer>().material = DefaultBallColor;
                }
                break;
            case "Soft"://「軟」を取った時、攻撃力半減、ボールの柄が変わる
                {
                    this.transform.localScale = new Vector3(DefaultBallSize, DefaultBallSize, DefaultBallSize);
                    Param.BallPower = 0.5f;
                    this.GetComponent<Renderer>().material = Kamifusen;
                }
                break;
            case "Pierce"://「貫」を取った時、攻撃力２倍、ボールの柄が変わる（動作しないため未実装）
                {
                    this.transform.localScale = new Vector3(DefaultBallSize, DefaultBallSize, DefaultBallSize);
                    Param.BallPower = 2;
                    this.GetComponent<Renderer>().material = DefaultBallColor;
                    GetComponent<Renderer>().material.color = new Color(1,0.7f, 0, 0);
                }
                break;
            default:
                {
                    this.transform.localScale = new Vector3(DefaultBallSize, DefaultBallSize, DefaultBallSize);
                    Param.BallPower = DefaultBallPower;
                    GetComponent<Renderer>().material = DefaultBallColor;
                }
                break;
            }
    }


    void BallRelease()//ゲーム開始時の処理
    {
        if (Input.GetKeyDown(KeyCode.Space))//PCではスペースキーでスタート
        {
            this.RB.AddForce(25 * Mathf.Sin(Mathf.Rad2Deg * -50), 25* Mathf.Cos(Mathf.Rad2Deg * 50), 0);
            Param.Moving = true;
        }
        else if (Param.FlickY > 30)//上方向にフリックでスタート
        {
            this.RB.AddForce(Param.FlickX / 10, Param.FlickY / 10, 0);
            Param.Moving = true;
        }
        this.transform.Translate(Param.Move / 100, 0, 0, Space.World);
        //ラケットの真ん中から動かないように位置を調整
        if (this.transform.position.x > 2.5f)
        {
            this.transform.position = new Vector3(2.5f, this.transform.position.y, this.transform.position.z);
        }
        else if (this.transform.position.x < -2.5f)
        {
            this.transform.position = new Vector3(-2.5f, this.transform.position.y, this.transform.position.z);
        }
    }

    void VelocityAdjust()//スピード調整
    {
        if (this.RB.velocity.magnitude < SpeedMin)
        {
            this.RB.velocity = RB.velocity.normalized * SpeedMin;
        }
        else if (this.RB.velocity.magnitude > SpeedMax)
        {
            this.RB.velocity = this.RB.velocity.normalized * SpeedMax;
        }
    }
    void VectorAdjust()//方向調整
    { 
        if (this.RB.velocity.normalized.x > VecAdj)
        {
            if (this.RB.velocity.normalized.y >= 0)
            {
                this.RB.velocity = new Vector3(VecAdj, VecAdj2, 0).normalized * this.RB.velocity.magnitude;
            }
            else
            {
                this.RB.velocity = new Vector3(VecAdj, -VecAdj2, 0).normalized * this.RB.velocity.magnitude;
            }
        }
        else if (this.RB.velocity.normalized.y > VecAdj)
        {
            if (this.RB.velocity.normalized.x >= 0)
            {
                this.RB.velocity = new Vector3(VecAdj2, VecAdj, 0).normalized * this.RB.velocity.magnitude;
            }
            else
            {
                this.RB.velocity = new Vector3(-VecAdj2, VecAdj, 0).normalized * this.RB.velocity.magnitude;
            }
        }
        else if (this.RB.velocity.normalized.x < -VecAdj)
        {
            if (this.RB.velocity.normalized.y >= 0)
            {
                this.RB.velocity = new Vector3(-VecAdj, VecAdj2).normalized * this.RB.velocity.magnitude;
            }
            else
            {
                this.RB.velocity = new Vector3(-VecAdj, -VecAdj2).normalized * this.RB.velocity.magnitude;
            }
        }
        else if (this.RB.velocity.normalized.y < -VecAdj)
        {
            if (this.RB.velocity.normalized.x >= 0)
            {
                this.RB.velocity = new Vector3(VecAdj2, -VecAdj).normalized * this.RB.velocity.magnitude;
            }
            else
            {
                RB.velocity = new Vector3(-VecAdj2, -VecAdj).normalized * RB.velocity.magnitude;
            }
        }
    }
}
