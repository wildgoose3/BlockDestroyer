using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using Parameter;

public class UIController : MonoBehaviour
{

    // Use this for initialization
    void Start ()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        Param.Score.GetComponent<Text>().text = "" + Param.TotalScore;//得点表示
        Param.Rest.GetComponent<Text>().text = "× " + Param.RacketRest;//ラケットの残り
        Param.DebugText.GetComponent<Text>().text = "" + Param.Moving;//ボールが動いているかどうか
        Param.BallText.GetComponent<Text>().text = "× " + Param.BallRest;//ボールが動いているかどうか
        if (Param.GameOver == true)
        {
            Param.GOText.GetComponent<Text>().text = "GAME OVER";
        }
        if (Param.BlockRest <= 0)
        {
            Param.GOText.GetComponent<Text>().text = "STAGE CLEAR!!";
        }
        
    }
}
