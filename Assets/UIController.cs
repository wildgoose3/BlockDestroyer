using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using Parameter;

public class UIController : MonoBehaviour
{
    private GameObject GOText;
    private GameObject Score;
    private GameObject Rest;
    private GameObject StageText;
    private GameObject DebugText;

    // Use this for initialization
    void Start ()
    {
        GOText = GameObject.Find("GameStatus");
        Score = GameObject.Find("Score");
        Rest = GameObject.Find("RacketRest");
        StageText = GameObject.Find("Stage");
        DebugText = GameObject.Find("Debug");
    }

    // Update is called once per frame
    void Update()
    {
        GOText.GetComponent<Text>().text = Param.GameStatus;//ゲームステータス：クリア、ゲームオーバー、nullの３種類
        Score.GetComponent<Text>().text = "" + Param.TotalScore;//得点表示
        Rest.GetComponent<Text>().text = "× " + Param.RacketRest;//ラケットの残り
        StageText.GetComponent<Text>().text = "STAGE  " + Param.Stage;//ステージ数 
        DebugText.GetComponent<Text>().text =" Block"+Param.BlockRest+ " " + Param.GameStatus+" " + Param.ShieldTime +" "+ Param.PlayedTime+'\n'+Param.FlickX+","+Param.FlickY+Param.Moving+Param.BallStatus;
    }
}
