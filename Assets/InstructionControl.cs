using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InstructionControl : MonoBehaviour
{
    private int ExpPageNow;
    private int ExpPageMax=2;
    private GameObject NextPage;
    private GameObject PrevPage;
    private GameObject Subject;
    private GameObject MainText;
    private string Sub;
    private string Main;
 
    // Use this for initialization
    void Start ()
    {
        ExpPageNow = 1;
        NextPage = GameObject.Find("NextPage");
        PrevPage = GameObject.Find("PrevPage");
        Subject = GameObject.Find("Subject");
        MainText= GameObject.Find("MainText");
    }
	
	// Update is called once per frame
	void Update ()
    {
        TextChange();
        Subject.GetComponent<Text>().text = Sub;
        MainText.GetComponent<Text>().text = Main;
        switch (ExpPageNow)
        {
            case 1:
                PrevPage.SetActive(false);
                NextPage.SetActive(true);
                break;
            case 2:
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

    void TextChange()
    {
        switch (ExpPageNow)
        {
            case 1:
                Sub = "ゲーム説明" + '\n' + "About This Game";
                Main = "画面下部のラケットを操作してボールを打ち返し、" + '\n' + "画面上部のブロックにぶつけて破壊するゲームです。" + '\n' + "動いているボールをすべて下に落としてしまうとラケットが１つ減ります。" + '\n' + "ラケットをすべて失うとゲームオーバーとなります。";
                break;
            case 2:
                Sub = "操作説明" + '\n' + "How To Play";
                Main = "上にスワイプ：ボールを発射" + '\n' + "左右にスワイプ：ラケットを移動";
                break;
        }
    }
}


