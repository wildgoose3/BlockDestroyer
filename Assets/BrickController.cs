using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

using Parameter;

public class BrickController : MonoBehaviour
{
    private float BrickHP; //ブロックの耐久力
    private int DestroyScore; //ブロックを壊したときに入る点数、耐久力によって異なる
    public GameObject ExpandPrefab;//アイテム「拡」
    public GameObject ShrinkPrefab;//アイテム「縮」
    public GameObject SplitPrefab;//アイテム「裂」
    public GameObject Racket1UpPrefab;//アイテム「増」
    public GameObject ShieldPrefab;//アイテム「底」
    public GameObject VanishPrefab;//アイテム「滅」
    public GameObject NormalPrefab;//アイテム「普」
    public GameObject PiercePrefab;//アイテム「貫」
    public GameObject HugePrefab;//アイテム「巨」
    public GameObject SoftPrefab;//アイテム「軟」

    // Use this for initialization
    void Start()
    {
        //ブロックの耐久力と得点を設定
        if (this.gameObject.tag == "Brick5")
        {
            this.BrickHP = 5;
            this.DestroyScore = 950;
        }
        else if (this.gameObject.tag == "Brick4")
        {
            this.BrickHP = 4;
            this.DestroyScore = 450;
        }
        else if (this.gameObject.tag == "Brick3")
        {
            this.BrickHP = 3;
            this.DestroyScore = 250;
        }
        else if (this.gameObject.tag == "Brick2")
        {
            this.BrickHP = 2;
            this.DestroyScore = 150;
        }
        else
        {
            this.BrickHP = 1;
            this.DestroyScore = 50;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //長期化したら上からアイテム
        if (Param.Moving)
        {
            LongPlayed();
        }
        
        //「貫」を取った時、ブロックをTriggerに変化
        /*if (Param.BallStatus == "Pierce" && this.BrickHP <= Param.BallPower)
        {
            GetComponent<Collider>().isTrigger = true;
        }
        else
        {
            GetComponent<Collider>().isTrigger = false;
        }*/

        // Updateではブロックの残り耐久力に応じて色を変えます
        if (this.BrickHP > 4 && this.BrickHP <= 5)
        {
            GetComponent<Renderer>().material.color = new Color(1, 0, 0, 0);
        }
        else if (this.BrickHP > 3 && this.BrickHP <= 4)
        {
            GetComponent<Renderer>().material.color = new Color(1, 0, 1, 0);
        }
        else if (this.BrickHP > 2 && this.BrickHP <= 3)
        {
            GetComponent<Renderer>().material.color = new Color(0, 0, 1, 0);
        }
        else if (this.BrickHP > 1 && this.BrickHP <= 2)
        {
            GetComponent<Renderer>().material.color = new Color(0, 1, 1, 0);
        }
        else if (this.BrickHP <= 1)
        {
            GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0);
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ball")
        {
            Param.TotalScore += 50;//ブロックが残ったか壊れたかにかかわらず当たった時に50点加算
            this.BrickHP -= Param.BallPower;//ブロックの耐久力を１回分減少
            this.GetComponent<ParticleSystem>().Play();//パーティクルを再生
            BlockDestroyed();
        }
    }

    void OntriggerEnter(Collider other)//「貫」を取った時の処理（現状はボールがすり抜けるだけ）
    {
        if (other.gameObject.tag == "Ball")
        {
            Param.TotalScore += 50;//ブロックが残ったか壊れたかにかかわらず当たった時に50点加算
            this.BrickHP -= Param.BallPower;//ブロックの耐久力を１回分減少
            this.GetComponent<ParticleSystem>().Play();//パーティクルを再生
            BlockDestroyed();
        }
    }
    //ブロック破壊時の処理
    void BlockDestroyed()
    {
        if (this.BrickHP <= 0)
        {
            Param.TotalScore += this.DestroyScore;//ブロックが壊れた時に得点加算             

            //パーティクルが消えるのを防ぐため、非表示、Colliderを無効に
            this.GetComponent<MeshRenderer>().enabled = false;
            this.GetComponent<Collider>().enabled = false;

            //ブロック破壊時にアイテム生成
            ItemAppear();
            Param.BlockRest -= 1;//ブロックの残り個数を減少
            if (Param.BlockRest <= 0)
            {
                Param.GameStatus = "CLEAR!!";
            }
            //１秒後にオブジェクト破棄
            Destroy(this.gameObject, 1.0f);
        }
    }

    //ブロック破壊時にアイテム生成
    void ItemAppear()
    {
        switch (Param.Stage)//ステージごとに出現率などを変更
        {
            case 1:
                {
                    int num = Random.Range(0, 10);
                    if (num == 0)
                    {
                        GameObject Shrink = Instantiate(ShrinkPrefab) as GameObject;
                        Shrink.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
                    }
                    else if (num == 1)
                    {
                        GameObject Expand = Instantiate(ExpandPrefab) as GameObject;
                        Expand.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
                    }
                    else if (num == 2)
                    {
                        GameObject Split = Instantiate(SplitPrefab) as GameObject;
                        Split.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
                    }
                    else if (num == 3)
                    {
                        GameObject Soft = Instantiate(SoftPrefab) as GameObject;
                        Soft.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
                    }
                    else if (num == 4 && this.gameObject.tag == "Brick2")
                    {
                        GameObject Racket1Up = Instantiate(Racket1UpPrefab) as GameObject;
                        Racket1Up.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
                    }
                    else if (num == 6)
                    {
                        GameObject Shield = Instantiate(ShieldPrefab) as GameObject;
                        Shield.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
                    }
                    /*else if (num == 7)
                    {
                        GameObject Normal = Instantiate(NormalPrefab) as GameObject;
                        Normal.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
                    }*/
                    else if (num == 8)
                    {
                        GameObject Huge = Instantiate(HugePrefab) as GameObject;
                        Huge.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
                    }
                    /*if (num == 9)
                    {
                        GameObject Pierce = Instantiate(PiercePrefab) as GameObject;
                        Pierce.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
                    }*/
                }
                break;
            case 2:
                {
                    int num = Random.Range(0, 10);
                    if (num == 0)
                    {
                        GameObject Shrink = Instantiate(ShrinkPrefab) as GameObject;
                        Shrink.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
                    }
                    else if (num == 1)
                    {
                        GameObject Expand = Instantiate(ExpandPrefab) as GameObject;
                        Expand.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
                    }
                    else if (num == 2)
                    {
                        GameObject Split = Instantiate(SplitPrefab) as GameObject;
                        Split.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
                    }
                    else if (num == 3)
                    {
                        GameObject Soft = Instantiate(SoftPrefab) as GameObject;
                        Soft.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
                    }
                    else if (num == 4 && this.gameObject.tag == "Brick2")
                    {
                        GameObject Racket1Up = Instantiate(Racket1UpPrefab) as GameObject;
                        Racket1Up.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
                    }
                    else if (num == 5 && this.gameObject.tag == "Brick3")
                    {
                        GameObject Vanish = Instantiate(VanishPrefab) as GameObject;
                        Vanish.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
                    }
                    else if (num == 6)
                    {
                        GameObject Shield = Instantiate(ShieldPrefab) as GameObject;
                        Shield.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
                    }
                    else if (num == 7)
                    {
                        GameObject Normal = Instantiate(NormalPrefab) as GameObject;
                        Normal.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
                    }

                }
                break;
            case 3:
                {
                    int num = Random.Range(0, 10);
                    if (num == 0)
                    {
                        GameObject Shrink = Instantiate(ShrinkPrefab) as GameObject;
                        Shrink.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
                    }
                    else if (num == 1)
                    {
                        GameObject Expand = Instantiate(ExpandPrefab) as GameObject;
                        Expand.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
                    }
                    else if (num == 2)
                    {
                        GameObject Split = Instantiate(SplitPrefab) as GameObject;
                        Split.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
                    }
                    else if (num == 3)
                    {
                        GameObject Soft = Instantiate(SoftPrefab) as GameObject;
                        Soft.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
                    }
                    else if (num == 4 && this.gameObject.tag == "Brick2")
                    {
                        GameObject Racket1Up = Instantiate(Racket1UpPrefab) as GameObject;
                        Racket1Up.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
                    }
                    else if (num == 5 && this.gameObject.tag == "Brick3")
                    {
                        GameObject Vanish = Instantiate(VanishPrefab) as GameObject;
                        Vanish.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
                    }
                    else if (num == 6)
                    {
                        GameObject Shield = Instantiate(ShieldPrefab) as GameObject;
                        Shield.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
                    }
                    else if (num == 7)
                    {
                        GameObject Normal = Instantiate(NormalPrefab) as GameObject;
                        Normal.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
                    }

                }
                break;
        }
    }
    void LongPlayed()//長期化した時にランダムでアイテムを生成
    {
        switch (Param.Stage)
        {
            case 1:
                {
                    if (Param.PlayedTime > 120)
                    {
                        int num = Random.Range(0, 10000);
                        if (num <= 1)
                        {
                            GameObject Split = Instantiate(SplitPrefab) as GameObject;
                            Split.transform.position = new Vector3(Random.Range(-3, 3),5, 0);
                        }
                    }
                }
                break;
            case 2:
                {
                    if (Param.PlayedTime > 120)
                    {
                        int num = Random.Range(0, 10000);
                        if (num == 0)
                        {
                            GameObject Split = Instantiate(SplitPrefab) as GameObject;
                            Split.transform.position = new Vector3(Random.Range(-3, 3), 5, 0);
                        }
                        if (num == 1)
                        {
                            GameObject Normal = Instantiate(NormalPrefab) as GameObject;
                            Normal.transform.position = new Vector3(Random.Range(-3, 3), 5, 0);
                        }
                    }
                }
                break;
            case 3:
                {
                    if (Param.PlayedTime > 120)
                    {
                        int num = Random.Range(0, 10000);
                        if (num == 0)
                        {
                            GameObject Split = Instantiate(SplitPrefab) as GameObject;
                            Split.transform.position = new Vector3(Random.Range(-3, 3), 5, 0);
                        }
                        if (num == 1)
                        {
                            GameObject Normal = Instantiate(NormalPrefab) as GameObject;
                            Normal.transform.position = new Vector3(Random.Range(-3, 3), 5, 0);
                        }
                    }
                }
                break;
        }
        
    }
}

