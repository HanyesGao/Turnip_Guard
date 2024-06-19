//  Felix-Bang：Round
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
// Describe：回合
// Createtime：2018/9/19


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FBApplication
{
	public class FBRound
	{
        public int MonsterID;  //怪物种类ID
        public int Count;      //怪物数量

        public FBRound(int monsterID, int count)
        {
            MonsterID = monsterID;
            Count = count;
        }

        public override string ToString()
        {
            return string.Format("[MonsterID:{0},Count:{1}]", MonsterID, Count);
        }
    }
}

