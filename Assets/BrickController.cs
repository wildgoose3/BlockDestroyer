using UnityEngine;
using System.Collections;

using Parameter;

public class BrickController : MonoBehaviour
{
   
    private int BrickHP; //ブロックの耐久力
    private int DestroyScore; //ブロックを壊したときに入る点数、耐久力によって異なる


    public GameObject extendPrefab;
    public GameObject shrinkPrefab;

    // Use this for initialization
    void Start()
    {


        //ブロックの耐久力と得点を設定
        if (this.gameObject.name == "Brick5Prefab(Clone)")
        {
            this.BrickHP = 5;
            this.DestroyScore = 950;
        }
        else if (this.gameObject.name == "Brick4Prefab(Clone)")
        {
            this.BrickHP = 4;
            this.DestroyScore = 450;
        }
        else if (this.gameObject.name == "Brick3Prefab(Clone)")
        {
            this.BrickHP = 3;
            this.DestroyScore = 250;
        }
        else if (this.gameObject.name == "Brick2Prefab(Clone)")
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
        // Updateではブロックの残り耐久力に応じて色を変えます
        if (this.BrickHP == 5)
        {
            GetComponent<Renderer>().material.color = new Color(1, 0, 0, 0);
        }
        else if (this.BrickHP == 4)
        {
            GetComponent<Renderer>().material.color = new Color(1, 0, 1, 0);
        }
        else if (this.BrickHP == 3)
        {
            GetComponent<Renderer>().material.color = new Color(0, 0, 1, 0);
        }
        else if (this.BrickHP == 2)
        {
            GetComponent<Renderer>().material.color = new Color(0, 1, 1, 0);
        }
        else if (this.BrickHP == 1)
        {
            GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0);
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ball")
        {
            Param.TotalScore += 50;//ブロックが残ったか壊れたかにかかわらず当たった時に50点加算
            this.BrickHP -= 1;//ブロックの耐久力を１回分減少
            GetComponent<ParticleSystem>().Play();//パーティクルを再生


            if (this.BrickHP <= 0)
            {
                Param.TotalScore += this.DestroyScore;//ブロックが壊れた時に得点加算
                Param.BlockRest -= 1;//ブロックの残り個数を減少

                //パーティクルが消えるのを防ぐため、非表示、Colliderを無効に
                GetComponent<MeshRenderer>().enabled = false;
                GetComponent<Collider>().enabled = false;

                //ブロック破壊時にアイテム生成

                    int num = Random.Range(0, 4);
                    if (num == 0)
                    {
                        GameObject Extend = Instantiate(extendPrefab) as GameObject;
                        Extend.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
                    }
                    else if (num == 1)
                    {
                        GameObject Shrink = Instantiate(shrinkPrefab) as GameObject;
                        Shrink.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
                }

                    //１秒後にオブジェクト破棄
                    Destroy(this.gameObject, 1.0f);
                if (Param.BlockRest <= 0)
                {
                    Param.GameOver = false;
                }
                
            }

        }
    }
}
