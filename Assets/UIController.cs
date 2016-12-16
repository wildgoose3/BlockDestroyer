using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using Parameter;

public class UIController : MonoBehaviour
{
    private GameObject GOText;
    private GameObject Score;
    private GameObject ScoreText;
    private GameObject Rest;
    private GameObject StageText;
    private GameObject DebugText;
    private GameObject RacketSymbol;
    // Use this for initialization
    void Start ()
    {
        GOText = GameObject.Find("GameStatus");
        Score = GameObject.Find("Score");
        ScoreText = GameObject.Find("ScoreText");
        Rest = GameObject.Find("RacketRest");
        StageText = GameObject.Find("Stage");
        DebugText = GameObject.Find("Debug");
        RacketSymbol= GameObject.Find("Image");
    }

    // Update is called once per frame
    void Update()
    {
        if (Param.ExpPageNow == 0)
        {
            GOText.SetActive(true);
            Score.SetActive(true);
            ScoreText.SetActive(true);
            Rest.SetActive(true);
            StageText.SetActive(true);
            DebugText.SetActive(true);
            RacketSymbol.SetActive(true);
        }
        else
        {
            GOText.SetActive(false);
            Score.SetActive(false);
            ScoreText.SetActive(false);
            Rest.SetActive(false);
            StageText.SetActive(false);
            DebugText.SetActive(false);
            RacketSymbol.SetActive(false);
        }
        GOText.GetComponent<Text>().text = Param.GameStatus;//ゲームステータス：クリア、ゲームオーバー、nullの３種類
        Score.GetComponent<Text>().text = "" + Param.TotalScore;//得点表示
        Rest.GetComponent<Text>().text = "× " + Param.RacketRest;//ラケットの残り
        StageText.GetComponent<Text>().text = "STAGE  " + Param.Stage;//ステージ数 
        DebugText.GetComponent<Text>().text =" Block"+Param.BlockRest+ " " + Param.GameStatus+" " + Param.ShieldTime +" "+ Param.PlayedTime+'\n'+Param.FlickX+","+Param.FlickY+Param.Moving+Param.BallStatus;
    }
}
