//  Felix-Bang：Level
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
// Describe：关卡数据
// Createtime：2018/9/19


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FBApplication
{
	public class FBLevel
	{
        /// <summary> 名称 </summary>
        public string Name;
        /// <summary> 卡片 </summary>
        public string CardImage;
        /// <summary> 背景 </summary>
        public string Background;
        /// <summary> 地图 </summary>
        public string Road;
        /// <summary> 初始金币 </summary>
        public int InitScore;
        /// <summary> 可放置炮塔的位置 </summary>
        public List<FBCoords> Holders = new List<FBCoords>();
        /// <summary> 路径节点 </summary>
        public List<FBCoords> Path = new List<FBCoords>();
        /// <summary> 回合 </summary>
        public List<FBRound> Rounds = new List<FBRound>();
	}
}

