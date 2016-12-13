using UnityEngine;
using System.Collections;

using Parameter;

public class BallController : MonoBehaviour
{

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
    private AudioSource SoundWall;
    private AudioSource SoundShoot;
    // Use this for initialization
    void Start()
    {
        AudioSource[] audioSources= GetComponents<AudioSource>();
        SoundWall = audioSources[0];
        SoundShoot = audioSources[1];
    }

    // Update is called once per frame
    void Update()
    {
        RB = this.GetComponent<Rigidbody>();
        VecAdj2 = Mathf.Sqrt(1 - VecAdj * VecAdj);
        BallStatusChange();
        if (Param.BlockRest <= 0)
        {
            this.RB.velocity = new Vector3(0, 0, 0);
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
            VelocityAdjust();//速度調整
            VectorAdjust();//方向調整
            BallSplit();//分裂処理
        }
    }


    void BallSplit()//分裂処理
    {
        if (Param.Split)
        {
                while (Param.BallRest < 3)
                {
                    GameObject Ball = Instantiate(BallPrefab) as GameObject;
                    this.RB.AddForce(1, 1, 0);
                    Param.BallRest += 1;
                }
            Param.Split = false;
        }
    }

    void BallStatusChange()//ボールの状態変更
    {
        switch (Param.BallStatus)
        {
            //「巨」を取った時、ボールが通常の４倍に
            case "Huge":
                {
                    this.transform.localScale = new Vector3(2, 2, 2);
                    Param.BallPower = DefaultBallPower;
                    this.GetComponent<Renderer>().material = DefaultBallColor;
                    this.GetComponent<ParticleSystem>().Stop();
                }
                break;
            //「軟」を取った時、攻撃力半減、ボールの柄が変わる
            case "Soft":
                {
                    this.transform.localScale = new Vector3(DefaultBallSize, DefaultBallSize, DefaultBallSize);
                    Param.BallPower = 0.5f;
                    this.GetComponent<Renderer>().material = Kamifusen;
                    this.GetComponent<ParticleSystem>().Stop();
                }
                break;
            //「貫」を取った時、攻撃力２倍、ボールのマテリアルを変化
            case "Pierce":
                {
                    this.transform.localScale = new Vector3(DefaultBallSize, DefaultBallSize, DefaultBallSize);
                    Param.BallPower = 2;
                    this.GetComponent<Renderer>().material = Pierce;
                    this.GetComponent<ParticleSystem>().Play();
                }
                break;
            default:
                {
                    this.transform.localScale = new Vector3(DefaultBallSize, DefaultBallSize, DefaultBallSize);
                    Param.BallPower = DefaultBallPower;
                    GetComponent<Renderer>().material = DefaultBallColor;
                    this.GetComponent<ParticleSystem>().Stop();
                }
                break;
        }
    }
    //効果音再生
    void OnCollisionEnter(Collision other)
    {
        //壁に当たった時
        if (other.gameObject.tag == "Wall"|| other.gameObject.tag == "Player")
        {
            SoundWall.PlayOneShot(SoundWall.clip);
        }
    }
    //ゲーム開始時の処理・ボールを動かす前はラケットに追従
    void BallRelease()
    {
        //PCではスペースキーでスタート
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.RB.AddForce(25 * Mathf.Sin(Mathf.Rad2Deg * -50), 25 * Mathf.Cos(Mathf.Rad2Deg * 50), 0);
            SoundShoot.PlayOneShot(SoundShoot.clip);
            Param.Moving = true;
        }
        //上方向にフリックでスタート
        else if (Param.FlickY > 30)
        {
            this.RB.AddForce(Param.FlickX / 10, Param.FlickY / 10, 0);
            Param.Moving = true;
        }
        this.transform.Translate(Param.Move / 100, 0, 0, Space.World);
        //ラケットの真ん中から動かないように位置を調整
        if (this.transform.position.x > (3.5f- Param.RacketWidth/2))
        {
            this.transform.position = new Vector3(2.5f, this.transform.position.y, this.transform.position.z);
        }
        else if (this.transform.position.x < -(3.5f - Param.RacketWidth / 2))
        {
            this.transform.position = new Vector3(-(3.5f - Param.RacketWidth / 2), this.transform.position.y, this.transform.position.z);
        }
    }
    //スピード調整
    void VelocityAdjust()
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
    //ボールの移動方向が水平・垂直に近くなったら矯正
    void VectorAdjust()
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
