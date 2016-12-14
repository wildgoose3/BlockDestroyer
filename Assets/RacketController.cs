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
    private float VanishTime;
 
    private GameObject Restart;
    private GameObject Title;
    private GameObject Next;
    private bool TitlePushed=false;
    private bool NextPushed=false;
    private bool RestartPushed=false;
    private int StageMax = 3;

    private AudioSource SoudTitle;
    private AudioSource GameStart;
  
    private AudioSource GameOver;
    private AudioSource RacketRevive;
    private AudioSource ItemGood;
    private AudioSource ItemBad;
    private AudioSource Item1Up;
    private AudioSource ItemNormal;

   /* private AudioSource ItemExpand;
    private AudioSource ItemShrink; 
    private AudioSource ItemShield;
    private AudioSource ItemVanish;
    private AudioSource ItemSplit;
    private AudioSource ItemPierce;
    private AudioSource ItemSoft;
    private AudioSource ItemHuge;*/
    
    // Use this for initialization
    void Start()
    {
        Restart=GameObject.Find("Restart");
        Title = GameObject.Find("Title");
        Next = GameObject.Find("Next");
                
        Restart.SetActive(false);
        Title.SetActive(false);
        Next.SetActive(false);

        RB=GetComponent<Rigidbody>();   

        Param.TotalScore = 0;
        Param.Stage = 1;
        Param.RacketRest = 2;
        Param.Move = 20;
        Param.Split = false;
        Param.RacketWidth = DefaultSize;     

        BlockGenerator();
        NextGameStart();

        Param.PlayedTime=0;

        AudioSource[] audioSources = GetComponents<AudioSource>();
        SoudTitle = audioSources[0];
        GameStart = audioSources[1];
        GameOver = audioSources[2];
        RacketRevive = audioSources[3];
        ItemGood = audioSources[4];
        ItemBad = audioSources[5];
        Item1Up = audioSources[6];
        ItemNormal = audioSources[7];
        GameStart.PlayOneShot(GameStart.clip);
    }

// Update is called once per frame
void Update()
    {
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
        this.RB.velocity = new Vector3(0, 0, 0);
        this.transform.Translate(Param.Move / 100, 0, 0, Space.World);
        if (Param.BallRest <= 0)
        {
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = true;               
            Param.Moving = false;
            
            RestartWait += Time.deltaTime;
            if (RestartWait > RestartTime)
            {
               if (Param.RacketRest <= 0)
               {
                Param.GameStatus = "GAME OVER";
               }
               else
               {
                    Param.RacketRest -= 1;
                    NextGameStart();
               }
               RestartWait = 0;
             }
        }
        if (Param.GameStatus == "GAME OVER")
        {
            bool SoundPlayed = false;
            if (SoundPlayed == false)
            {
                GameOver.PlayOneShot(GameOver.clip);
                SoundPlayed = true;
            }
            this.RB.velocity = new Vector3(0, 0, 0);
            Restart.SetActive(true);
            Title.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space)|| TitlePushed)
            {
                SoudTitle.PlayOneShot(SoudTitle.clip);
                SceneManager.LoadScene("START");
                SoundPlayed = false;
            }
            if (RestartPushed)
            {
                GameStart.PlayOneShot(GameStart.clip);
                RestartPushed = false;
                Restart.SetActive(false);
                Title.SetActive(false);
                Param.TotalScore = 0;
                Param.RacketRest = 2;
                Param.GameStatus = "";
                Param.PlayedTime = 0;
                BlockGenerator();
                NextGameStart();
                SoundPlayed = false;
            }
        }
        else if (Param.GameStatus == "CLEAR!!")
        {
            Next.SetActive(true);
            Param.ShieldTime = 0;
            if (Input.GetKeyDown(KeyCode.Space)||NextPushed)
            {
                Param.Stage += 1;
                if (Param.Stage > StageMax)
                {
                    Param.Stage = 1;
                }
                Param.GameStatus = "";
                Param.PlayedTime = 0;
                BlockGenerator();
                NextGameStart();
                NextPushed = false;
                Next.SetActive(false);
                GameStart.PlayOneShot(GameStart.clip);
             }
        }
    }  
    void OnTriggerEnter(Collider other)//アイテムの当たり判定
    {
        switch (other.gameObject.name)
        {
       case "ExpandPrefab(Clone)":
             ItemGood.PlayOneShot(ItemGood.clip);
             if (transform.localScale.x < 5)
             {
                transform.localScale = new Vector3(transform.localScale.x + 0.5f, transform.localScale.y, transform.localScale.z);
             }
             break;
       case "ShrinkPrefab(Clone)":
             ItemBad.PlayOneShot(ItemBad.clip);
             if (transform.localScale.x > 1)
             {
                transform.localScale = new Vector3(transform.localScale.x - 0.5f, transform.localScale.y, transform.localScale.z);
             }
             break;
       case "SplitPrefab(Clone)":
             ItemGood.PlayOneShot(ItemGood.clip);
             Param.Split = true;
             break;
        case "Racket1UpPrefab(Clone)":
              Item1Up.PlayOneShot(Item1Up.clip);
              Param.RacketRest += 1;
              break;
        case "HugePrefab(Clone)":
              ItemGood.PlayOneShot(ItemGood.clip);
              Param.BallStatus="Huge";
              break;
        case "SoftPrefab(Clone)":
              ItemBad.PlayOneShot(ItemBad.clip);
              Param.BallStatus = "Soft";
              break;
        case "ShieldPrefab(Clone)":
              ItemGood.PlayOneShot(ItemGood.clip);
              Param.ShieldTime=10.0f;
              break;
        case "VanishPrefab(Clone)":
              ItemBad.PlayOneShot(ItemBad.clip);
              VanishTime = 5.0f;
              transform.localScale = new Vector3(DefaultSize, transform.localScale.y, transform.localScale.z);
              this.transform.position = new Vector3(0, this.transform.position.y, this.transform.position.z);
              this.GetComponent<MeshRenderer>().enabled = false;
              this.GetComponent<Collider>().enabled = false;
              break;
        case "NormalPrefab(Clone)":
              ItemNormal.PlayOneShot(ItemNormal.clip);
              Param.BallStatus = "";
              break;
        case "PiercePrefab(Clone)":
              ItemGood.PlayOneShot(ItemGood.clip);
              Param.BallStatus = "Pierce";
              break;
        }
        if (other.gameObject.tag == "Item")
        {
            Destroy(other.gameObject);
        }
    }
    void VanishControl()//「滅」を取った時の処理・ラケットが消滅・５秒後に復活
    {
        VanishTime -= Time.deltaTime;
        if (this.GetComponent<MeshRenderer>().enabled == false && this.GetComponent<Collider>().enabled == false)
        {
            if (VanishTime < 0)
            {
                RacketRevive.PlayOneShot(RacketRevive.clip);
                this.GetComponent<MeshRenderer>().enabled = true;
                this.GetComponent<Collider>().enabled = true;
            }
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
        Ball.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.51f, 0);
        
        Param.ShieldTime = 0;
        VanishTime = 0;
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
                    for (float i = -3; i <= 1; i++)
                    {
                        GameObject Brick4 = Instantiate(BrickPrefab) as GameObject;
                        Brick4.transform.position = new Vector3(i, -2, 0);
                        Brick4.gameObject.tag = "Brick4";
                        Param.BlockRest += 1;
                    }

                    for (int i = -1; i <= 3; i++)
                    {
                        GameObject Brick1 = Instantiate(BrickPrefab) as GameObject;
                        Brick1.transform.position = new Vector3(i, -1, 0);
                        Brick1.gameObject.tag = "Brick1";
                        Param.BlockRest += 1;
                    }
                    for (int i = -3; i <= 1; i++)
                    {
                        GameObject Brick3 = Instantiate(BrickPrefab) as GameObject;
                        Brick3.transform.position = new Vector3(i, 0, 0);
                        Brick3.gameObject.tag = "Brick3";
                        Param.BlockRest += 1;
                    }

                    for (int i = -1; i <= 3; i++)
                    {
                        GameObject Brick2 = Instantiate(BrickPrefab) as GameObject;
                        Brick2.transform.position = new Vector3(i, -1, 0);
                        Brick2.gameObject.tag = "Brick2";
                        Param.BlockRest += 1;
                    }

                    for (int i = -3; i <= 1; i++)
                    {
                        GameObject Brick5 = Instantiate(BrickPrefab) as GameObject;
                        Brick5.transform.position = new Vector3(i, 2, 0);
                        Brick5.gameObject.tag = "Brick5";
                        Param.BlockRest += 1;
                    }
                    for (int i = -1; i <= 3; i++)
                    {
                        GameObject Brick1 = Instantiate(BrickPrefab) as GameObject;
                        Brick1.transform.position = new Vector3(i, 3, 0);
                        Brick1.gameObject.tag = "Brick1";
                        Param.BlockRest += 1;
                    }
                    for (int i = -3; i <= 1; i++)
                    {
                        GameObject Brick3 = Instantiate(BrickPrefab) as GameObject;
                        Brick3.transform.position = new Vector3(i, 4, 0);
                        Brick3.gameObject.tag = "Brick3";
                        Param.BlockRest += 1;
                    }
                    break;
                }
        }
    }
}

