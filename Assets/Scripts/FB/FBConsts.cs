//  Felix-Bang：Consts
//　　 へ　　　　　／|
//　　/＼7　　　 ∠＿/
//　 /　│　　 ／　／
//　│　Z ＿,＜　／　　 /`ヽ
//　│　　　　　ヽ　　 /　　〉
//　 Y　　　　　`　 /　　/
//　ｲ●　､　●　　⊂⊃〈　　/
//　()　 へ　　　　|　＼〈
//　　>ｰ ､_　 ィ　 │ ／／
//　 / へ　　 /　ﾉ＜| ＼＼
//　 ヽ_ﾉ　　(_／　 │／／
//　　7　　　　　　　|／
//　　＞―r￣￣`ｰ―＿
// Describe：常量
// Createtime：2018/9/20


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FBApplication
{
	public static class FBConsts
	{
        //目录
        public static readonly string LevelDir = Application.dataPath + @"/Resources/Res/Levels";
        public static readonly string MpsDir = Application.dataPath + @"/Resources/Res/Maps";
        public static readonly string CardsDir = Application.dataPath + @"/Resources/Res/Cards";

        //参数
        public const string GameProgress = "GameProgress";
        public const float DotClosedDistance = 0.1f;
        public const float RangeClosedDistance = 0.7f;

        
       


        //Model
        public const string M_GameModel = "M_GameModel";
        public const string M_RoundModel = "M_RoundModel";

        //View
        public const string V_Start = "V_Start";
        public const string V_Select = "V_Select";
        public const string V_Board = "V_Board";
        public const string V_CountDown = "V_CountDown";
        public const string V_Win = "V_Win";
        public const string V_Lost = "V_Lost";
        public const string V_System = "V_System";
        public const string V_Complete = "V_Complete";
        public const string V_Spawner = "V_Spawner";
        public const string V_TowerPopup = "V_TowerPopup";

        //Control
        /// <summary> 启动游戏 </summary>
        public const string E_StartUp = "E_StartUp";

        /// <summary> 进入场景 </summary>
        public const string E_SceneEnter = "E_SceneEnter";
        /// <summary> 退出场景 </summary>
        public const string E_SceneExit = "E_SceneExit";

        /// <summary> 开始关卡 </summary>
        public const string E_LevelStart = "E_LevelStart";                     //FBStartLevelArgs
        /// <summary> 结束关卡 </summary>
        public const string E_LevelEnd = "E_LevelEnd";                         //FBEndLevelArgs

        /// <summary> 结束倒计时 </summary>
        public const string E_CountDownComplete = "E_CountDownComplete";

        /// <summary> 开始回合 </summary>
        public const string E_RoundStart = "E_RoundStart";                     //FBRoundStartArgs
                                                                   
        /// <summary> 出怪 </summary>
        public const string E_SpawnMonster = "E_SpawnMonster";                 //FBSpawnMonsterArgs

        /// <summary> 炮塔 </summary>
        public const string E_ShowTowerCreat = "E_ShowTowerCreat";             //FBShowTowerCreatArgs
        public const string E_ShowTowerUpgrade = "E_ShowTowerUpgrade";         //FBShowTowerUpgradeArgs
        public const string E_TowerHide = "E_TowerHide";

        public const string E_SpawnTower = "E_SpawnTower";                     //FBSpawnTowerArgs
        public const string E_UpgradeTower = "E_UpgradeTower";                 //FBUpgradeTowerArgs
        public const string E_SellTower = "E_SellTower";                       //FBSellTowerArgs
    }

    public enum GameSpeed
    {
        One,
        Two
    }
    public enum MonsterType
    {
        Monster0,
        Monster1,
        Monster2,
        Monster3,
        Monster4,
        Monster5,
     
    }
}

