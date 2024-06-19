//  Felix-Bang：MapEditor
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
// Describe：地图编辑器
// Createtime：2018/9/25


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using FBApplication;
using System.IO;
using System;

namespace FBEditor
{
    [CustomEditor(typeof(FBMap))]
	public class MapEditor : Editor
	{
        [HideInInspector]
        public FBMap Map=null;
        /// <summary> 关卡列表 </summary>
        List<FileInfo> f_files = new List<FileInfo>();
        /// <summary> 当前编辑的关卡索引号 </summary>
        int f_selectInfoIndex = -1;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (Application.isPlaying)
            {
                // 关联目标 为Map赋值
                Map = target as FBMap;
                
                // 第一行按钮
                EditorGUILayout.BeginHorizontal();
                //LoadLevelFiles();
                int currentIndex = EditorGUILayout.Popup(f_selectInfoIndex, GetNames(f_files));
                if (currentIndex != f_selectInfoIndex)
                {
                    f_selectInfoIndex = currentIndex;
                    //加载关卡
                    LoadLevel();
                }

                if (GUILayout.Button("读取列表"))
                {
                    LoadLevelFiles();
                }
                EditorGUILayout.EndHorizontal();

                // 第二行按钮
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("清空塔点"))
                    Map.ClearHolder();

                if (GUILayout.Button("清空路径"))
                    Map.ClearRoad();
                EditorGUILayout.EndHorizontal();

                // 第三行按钮
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("保存数据"))
                    SaveLevel();
                EditorGUILayout.EndHorizontal();
            }

            if (GUI.changed)
                EditorUtility.SetDirty(target);
        }

        private string[] GetNames(List<FileInfo> files)
        {
            List<string> names = new List<string>();
            foreach (FileInfo file in files)
                names.Add(file.Name);

            return names.ToArray();
        }

        private void LoadLevel()
        {
            FileInfo file = f_files[f_selectInfoIndex];
            FBLevel level = new FBLevel();
            FBTools.FillLevel(file.FullName, ref level);
            Map.LoadLevel(level);
        }

        private void LoadLevelFiles()
        {
            Clear();
            f_files = FBTools.GetLevelFiles();
            if (f_files.Count > 0)
            {
                f_selectInfoIndex = 0;
                LoadLevel();
            }
        }

        private void SaveLevel()
        {
            //获取当前加载的关卡
            FBLevel level = Map.Level;

            //临时索引点
            List<FBCoords> list = null;

            //收集塔点
            list = new List<FBCoords>();
            for (int i = 0; i < Map.Grids.Count; i++)
            {
                FBGrid g = Map.Grids[i];
                if (g.CanHold)
                {
                    FBCoords c = new FBCoords(g.Index_X,g.Index_Y);
                    list.Add(c);
                }
            }
            level.Holders = list;

            //收集寻路点
            list = new List<FBCoords>();
            for (int i = 0; i < Map.Road.Count; i++)
            {
                FBGrid g = Map.Road[i];
                FBCoords c = new FBCoords(g.Index_X, g.Index_Y);
                list.Add(c);
            }
            level.Path = list;

            //保存
            string fileName = f_files[f_selectInfoIndex].FullName;
            FBTools.SaveLevel(fileName,level);
            //弹框
            EditorUtility.DisplayDialog("保存关卡数据","保存成功","确定");
        }

        private void Clear()
        {
            f_files.Clear();
            f_selectInfoIndex = -1;
        }
    }
}

