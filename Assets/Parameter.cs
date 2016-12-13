using UnityEngine;
using System.Collections;

namespace Parameter
{
    public static class Param
    {
        public static int RacketRest; //現在残っているラケット
        public static int BallRest; //現在残っているボール
        public static int BlockRest; //残りブロック
        public static int TotalScore; //得点
        public static float Move;//ラケットの移動速度
        public static bool Moving;//ボールリリース中かどうか
        public static int Stage;//ステージ数
        public static string GameStatus;
        public static string SPDirection;
        public static float FlickX;
        public static float FlickY;
        public static bool Split;
        public static float BallSize;
        public static float BallPower;
        public static string BallStatus;
        public static float PlayedTime;
        public static float RacketWidth;
    }
}

