using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using Parameter;

public class UIController : MonoBehaviour
{
    private GameObject GOText;
    private GameObject Score;
    private GameObject Rest;
    private GameObject DebugText;
    private GameObject StageText;

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
        Score.GetComponent<Text>().text = "" + Param.TotalScore;//得点表示
        Rest.GetComponent<Text>().text = "× " + Param.RacketRest;//ラケットの残り
        StageText.GetComponent<Text>().text = "STAGE  " + Param.Stage;//ステージ数
        GOText.GetComponent<Text>().text = Param.GameStatus;//ゲームステータス：クリア、ゲームオーバー、nullの３種類
        DebugText.GetComponent<Text>().text = "Ball"+Param.BallRest+" Block"+Param.BlockRest+" "+Param.PlayedTime;
    }
}
