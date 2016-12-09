using UnityEngine;
using System.Collections;

namespace Parameter
{
    public static class Param
    {
        public static int RacketRest=3; //現在残っているラケット
        public static int BallRest=1; //現在残っているボール
        public static int BlockRest; //残りブロック
        public static int TotalScore; //得点
        public static bool Moving;//ボールリリース中かどうか
        public static bool GameOver = false;//ゲームオーバー
        public static int Stage=1;//ステージ数
        public static GameObject GOText = GameObject.Find("GameOver");//ゲームオーバーテキスト
        public static GameObject Score = GameObject.Find("Score"); //得点表示用テキスト
        public static GameObject Rest= GameObject.Find("RacketRest");
        public static GameObject DebugText = GameObject.Find("Debug");
        public static GameObject BallText = GameObject.Find("MovingBall");
    }
}

