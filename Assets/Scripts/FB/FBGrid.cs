//  Felix-Bang：Tile
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
// Describe：背景网格（单个）
// Createtime：2018/9/19


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FBApplication
{
	public class FBGrid 
	{
        public int Index_X;
        public int Index_Y;
        public bool CanHold;    //是否可以放置塔
        public Object Data;     //保存的相关数据

        public FBGrid(int x, int y)
        {
            Index_X = x;
            Index_Y = y;
        }

        public override string ToString()
        {
            return string.Format("[X:{0},Y:{1},CanHold:{2}]", Index_X, Index_Y,CanHold);
        }
    }
}

