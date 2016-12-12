using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

using Parameter;

public class RacketController : MonoBehaviour
{
    private float DefaultSize = 2;
    private Rigidbody RB;
    public GameObject BallPrefab;
    private float RestartTime = 3.0f;
    private float RestartWait = 0;
    public GameObject BrickPrefab;
    public GameObject HardBlockPrefab;

    private GameObject Shield;
    private float ShieldTime;
    private float VanishTime;
 
    private GameObject Restart;
    private GameObject Title;
    private GameObject Next;
    private bool TitlePushed=false;
    private bool NextPushed=false;
    private bool RestartPushed=false;

    // Use this for initialization
    void Start()
    {
        Restart=GameObject.Find("Restart");
        Title = GameObject.Find("Title");
        Next = GameObject.Find("Next");
        Shield = GameObject.Find("Shield");

        Debug.Log(this.gameObject.name);
        Debug.Log(Restart.gameObject.name);
        Debug.Log(Title.gameObject.name);
        Debug.Log(Next.gameObject.name);
        Debug.Log(Shield.gameObject.name);
        Restart.SetActive(false);
        Title.SetActive(false);
        Next.SetActive(false);

        RB.GetComponent<Rigidbody>();   

        Shield.GetComponent<MeshRenderer>().enabled = false;
        Shield.GetComponent<Collider>().enabled = false;

        Param.TotalScore = 0;
        Param.Stage = 1;
        Param.RacketRest = 3;
        Param.Move = 20;
        Param.Split = false;        

        BlockGenerator();
        NextGameStart();

        Param.PlayedTime=0;
        }

// Update is called once per frame
void Update()
    {
        Debug.Log(this.gameObject.name);
        Debug.Log(Restart.gameObject.name);
        Debug.Log(Title.gameObject.name);
        Debug.Log(Next.gameObject.name);
        Debug.Log(Shield.gameObject.name);
        // ShieldControl();
        VanishControl();
        if (Param.Moving)
        {
            Param.PlayedTime += Time.deltaTime;
        }
            if (Input.GetKey(KeyCode.LeftArrow) || Param.SPDirection == "left")
            {
                Param.Move = -15;
            }
            else if (Input.GetKey(KeyCode.RightArrow) || Param.SPDirection == "right")
            {
                Param.Move = 15;
            }
            else
            {
                Param.Move = 0;
            }
            //this.RB.velocity = new Vector3(0, 0, 0);
        //実行中
        if (this.RB == null) Debug.Break();
        this.transform.Translate(Param.Move / 100, 0, 0, Space.World);
        
        if (Param.BallRest <= 0)
        {
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;

            RestartWait += Time.deltaTime;
            if (RestartWait > RestartTime)
            {
               if (Param.RacketRest <= 0)
               {
                Param.GameStatus = "GAME OVER";
               }
               else
               {
                    NextGameStart();
               }
               RestartWait = 0;
             }
        }
        if (Param.GameStatus == "GAME OVER")
        {
            this.RB.velocity = new Vector3(0, 0, 0);
            Restart.SetActive(true);
            Title.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space)|| TitlePushed)
            {
                SceneManager.LoadScene("Title");
            }
            if (RestartPushed)
            {
                RestartPushed = false;
                Restart.SetActive(false);
                Title.SetActive(false);
                Param.TotalScore = 0;
                Param.RacketRest = 3;
                Param.GameStatus = "";
                Param.PlayedTime = 0;
                BlockGenerator();
                NextGameStart();
            }
        }
        else if (Param.GameStatus == "CLEAR!!")
        {
            Next.SetActive(true);
            ShieldTime = 0;
            if (Input.GetKeyDown(KeyCode.Space)||NextPushed)
            {
                Param.Stage += 1;
                Param.GameStatus = "";
                Param.PlayedTime = 0;
                BlockGenerator();
                NextGameStart();
                NextPushed = false;
                Next.SetActive(false);
            }
        }
    }
    
    void OnTriggerEnter(Collider other)//アイテムの当たり判定
    {
        switch (other.gameObject.name)
        {
        case "ExpandPrefab(Clone)":
                if (transform.localScale.x < 5)
                 {
                  transform.localScale = new Vector3(transform.localScale.x + 0.5f, transform.localScale.y, transform.localScale.z);
                 }
               break;
        case "ShrinkPrefab(Clone)":
              if (transform.localScale.x > 1)
              {
                transform.localScale = new Vector3(transform.localScale.x - 0.5f, transform.localScale.y, transform.localScale.z);
              }
                break;
        case "SplitPrefab(Clone)":
            Param.Split = true;
                break;
        case "Racket1UpPrefab(Clone)":
            Param.RacketRest += 1;
                break;
        case "HugePrefab(Clone)":
            Param.BallStatus="Huge";
                break;
        case "SoftPrefab(Clone)":
                Param.BallStatus = "Soft";
                break;
        case "ShieldPrefab(Clone)":
                ShieldTime=10.0f;
                Shield.GetComponent<MeshRenderer>().enabled = true;
                Shield.GetComponent<Collider>().enabled = true;
                break;
        case "VanishPrefab(Clone)":
                VanishTime = 5.0f;
                transform.localScale = new Vector3(DefaultSize, transform.localScale.y, transform.localScale.z);
                this.transform.position = new Vector3(0, this.transform.position.y, this.transform.position.z);
                this.GetComponent<MeshRenderer>().enabled = false;
                this.GetComponent<Collider>().enabled = false;
                break;
            case "NormalPrefab(Clone)":
                Param.BallStatus = "";
                break;
            case "PiercePrefab(Clone)":
                Param.BallStatus = "Pierce";
                break;
        }
        if (other.gameObject.tag == "Item")
        {
            Destroy(other.gameObject);
        }
    }
    void ShieldControl()//「底」を取った時の処理・10秒間底に壁ができる
    {
            ShieldTime -= Time.deltaTime;
            if (ShieldTime > 3.0f)
            {
                Shield.GetComponent<Renderer>().material.color = new Color(0.235f, 0.5f, 0.235f, 0);
            }
            else if (ShieldTime > 2.0f)
            {
                Shield.GetComponent<Renderer>().material.color = new Color(ShieldTime-2, ShieldTime-2, 0, 0);
            }
        else if (ShieldTime > 1.0f)
        {
            Shield.GetComponent<Renderer>().material.color = new Color(ShieldTime-1, ShieldTime-1, 0, 0);
        }
        else if (ShieldTime > 0)
            {
                Shield.GetComponent<Renderer>().material.color = new Color(ShieldTime, 0, 0, 0);
            }
            else
            {
                Shield.GetComponent<MeshRenderer>().enabled = false;
                Shield.GetComponent<Collider>().enabled = false;
            }
        
    }
    void VanishControl()//「滅」を取った時の処理・ラケットが消滅・５秒後に復活
    {
        VanishTime -= Time.deltaTime;
        
        if (VanishTime < 0)
        {
            this.GetComponent<MeshRenderer>().enabled = true;
            this.GetComponent<Collider>().enabled = true;
        }
    }
    void NextGameStart()//ゲームスタート、リスタート時の処理
    {
        Param.Moving = false;
        this.transform.localScale = new Vector3(this.DefaultSize, 0.5f, 0.5f);
        this.transform.position = new Vector3(0, -3, 0);
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<Collider>().enabled = true;
        GameObject Ball = Instantiate(BallPrefab) as GameObject;
        Param.BallRest = 1;
        Param.BallStatus = "";
        Ball.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.5f, 0);
        
        ShieldTime = 0;
        VanishTime = 0;
        //Shield.GetComponent<MeshRenderer>().enabled = false;
      //  Shield.GetComponent<Collider>().enabled = false;
    }

    public void TitleButtonDown()//ゲームオーバー時に出現
    {
        TitlePushed = true;
    }
    public void NextButtonDown()//ステージクリア時に出現
    {
        NextPushed = true;
    }
    public void RestartButtonDown()//ゲームオーバー時に出現
    {
        RestartPushed = true;
    }
    void BlockGenerator()//ステージ開始時のブロック生成
    {
            switch (Param.Stage)
        {
            case 1:
                for (float j = 2; j <= 3.5; j += 0.5f)
                {
                    for (float i = -3; i <= 3; i++)
                    {
                        GameObject Brick1 = Instantiate(BrickPrefab) as GameObject;
                        Brick1.transform.position = new Vector3(i, j, 0);
                        Brick1.gameObject.tag = "Brick1";
                        Param.BlockRest += 1;

                        GameObject Brick2 = Instantiate(BrickPrefab) as GameObject;
                        Brick2.transform.position = new Vector3(i, j + 0.25f, 0);
                        Brick2.gameObject.tag = "Brick2";
                        Param.BlockRest += 1;
                    }
                }
                break;

            case 2:
                for (float j = 2; j <= 4; j += 2)
                {
                    for (float i = -3; i <= 3; i++)
                    {
                        GameObject Brick1 = Instantiate(BrickPrefab) as GameObject;
                        Brick1.transform.position = new Vector3(i, j, 0);
                        Brick1.gameObject.tag = "Brick1";
                        Param.BlockRest += 1;
                    }
                }
                for (float i = -1.5f; i <= 1.5f; i+=3)
                {
                    GameObject HardBlock = Instantiate(HardBlockPrefab) as GameObject;
                    HardBlock.transform.position = new Vector3(i, 3, 0);
                }
                for (float j = 2.5f; j <= 3.5f; j++)
                {
                    for (int i = -3; i <= 3; i++)
                    {
                        GameObject Brick2 = Instantiate(BrickPrefab) as GameObject;
                        Brick2.transform.position = new Vector3(i, j, 0);
                        Brick2.gameObject.tag = "Brick2";
                        Param.BlockRest += 1;
                    }
                }
                for (float j = 2.25f; j <= 3.75f; j += 0.5f)
                {
                    for (float i = -2.5f; i <= 2.5f; i++)
                    {
                        GameObject Brick3 = Instantiate(BrickPrefab) as GameObject;
                        Brick3.transform.position = new Vector3(i, j, 0);
                        Brick3.gameObject.tag = "Brick3";
                        Param.BlockRest += 1;
                    }
                }
                break;
            case 3:
                {

                }
                break;

        }
    }
}

