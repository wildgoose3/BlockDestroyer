using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExplanationControl : MonoBehaviour {

    private int ExpPageNow;
    private int ExpPageMax=2;
    private GameObject NextPage;
    private GameObject PrevPage;
    private GameObject Subject;
    private GameObject MainText;

    // Use this for initialization
    void Start () {
        ExpPageNow = 1;
        NextPage = GameObject.Find("NextPage");
        PrevPage = GameObject.Find("PrevPage");
        Subject = GameObject.Find("Subject");
        MainText= GameObject.Find("MainText");
}
	
	// Update is called once per frame
	void Update ()
    {
	    switch (ExpPageNow)
        {
            case 1:
                Subject.GetComponent<Text>().text = "ゲーム説明" + '\n' + "About This Game";
                MainText.GetComponent<Text>().text = "画面下部のラケットを操作してボールを打ち返し、"+'\n'+ "画面上部のブロックにぶつけて破壊するゲームです。" + '\n' + "動いているボールをすべて下に落としてしまうとラケットが１つ減ります。" + '\n' + "ラケットをすべて失うとゲームオーバーとなります。";
                PrevPage.SetActive(false);
                NextPage.SetActive(true);
                break;
            case 2:
                Subject.GetComponent<Text>().text = "操作説明" + '\n' + "How To Play";
                MainText.GetComponent<Text>().text = "上にスワイプ：ゲーム開始"+'\n'+"左右にスワイプ：ラケットを移動";
                PrevPage.SetActive(true);
                NextPage.SetActive(false);
                break;
        }
  	}

    public void PrevPushed()
    {
        if (ExpPageNow > 1)
        {
            ExpPageNow -= 1;
        }
    }

    public void NextPushed()
    {
        if (ExpPageNow < ExpPageMax)
        {
            ExpPageNow += 1;

        }
        }

    public void TitlePushed()
    {
        SceneManager.LoadScene("Title");
    }
}


