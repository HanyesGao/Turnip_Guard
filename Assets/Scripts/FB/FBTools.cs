//  Felix-Bang：Tools
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
// Describe：
// Createtime：2018/9/20


using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

namespace FBApplication
{
	public class FBTools
	{
        //读取关卡列表
        public static List<FileInfo> GetLevelFiles()
        {
            string[] files = Directory.GetFiles(FBConsts.LevelDir, "*.xml");

            List<FileInfo> list = new List<FileInfo>();
            for (int i = 0; i < files.Length; i++)
            {
                FileInfo file = new FileInfo(files[i]);
                list.Add(file);
            }
            return list;
        }

        //填充Level类数据
        public static void FillLevel(string fileName, ref FBLevel level)
        {
            FileInfo file = new FileInfo(fileName);
            StreamReader sr = new StreamReader(file.OpenRead(), Encoding.UTF8);

            XmlDocument doc = new XmlDocument();
            doc.Load(sr);

            level.Name = doc.SelectSingleNode("/Level/Name").InnerText;
            level.CardImage = doc.SelectSingleNode("/Level/CardImage").InnerText;
            level.Background = doc.SelectSingleNode("/Level/Background").InnerText;
            level.Road = doc.SelectSingleNode("/Level/Road").InnerText;
            level.InitScore = int.Parse(doc.SelectSingleNode("/Level/InitScore").InnerText);

            XmlNodeList nodes;

            nodes = doc.SelectNodes("/Level/Holder/Point");
            for (int i = 0; i < nodes.Count; i++)
            {
                XmlNode node = nodes[i];
                FBCoords p = new FBCoords(
                    int.Parse(node.Attributes["X"].Value),
                    int.Parse(node.Attributes["Y"].Value));

                level.Holders.Add(p);
            }

            nodes = doc.SelectNodes("/Level/Path/Point");
            for (int i = 0; i < nodes.Count; i++)
            {
                XmlNode node = nodes[i];

                FBCoords p = new FBCoords(
                    int.Parse(node.Attributes["X"].Value),
                    int.Parse(node.Attributes["Y"].Value));

                level.Path.Add(p);
            }

            nodes = doc.SelectNodes("/Level/Rounds/Round");
            for (int i = 0; i < nodes.Count; i++)
            {
                XmlNode node = nodes[i];

                FBRound r = new FBRound(
                        int.Parse(node.Attributes["Monster"].Value),
                        int.Parse(node.Attributes["Count"].Value)
                    );

                level.Rounds.Add(r);
            }

            sr.Close();
            sr.Dispose();
        }

        //保存关卡
        public static void SaveLevel(string fileName, FBLevel level)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            sb.AppendLine("<Level>");

            sb.AppendLine(string.Format("<Name>{0}</Name>", level.Name));
            sb.AppendLine(string.Format("<CardImage>{0}</CardImage>", level.CardImage));
            sb.AppendLine(string.Format("<Background>{0}</Background>", level.Background));
            sb.AppendLine(string.Format("<Road>{0}</Road>", level.Road));
            sb.AppendLine(string.Format("<InitScore>{0}</InitScore>", level.InitScore));

            sb.AppendLine("<Holder>");
            for (int i = 0; i < level.Holders.Count; i++)
            {
                sb.AppendLine(string.Format("<Point X=\"{0}\" Y=\"{1}\"/>", level.Holders[i].X, level.Holders[i].Y));
            }
            sb.AppendLine("</Holder>");

            sb.AppendLine("<Path>");
            for (int i = 0; i < level.Path.Count; i++)
            {
                sb.AppendLine(string.Format("<Point X=\"{0}\" Y=\"{1}\"/>", level.Path[i].X, level.Path[i].Y));
            }
            sb.AppendLine("</Path>");

            sb.AppendLine("<Rounds>");
            for (int i = 0; i < level.Rounds.Count; i++)
            {
                sb.AppendLine(string.Format("<Round Monster=\"{0}\" Count=\"{1}\"/>", level.Rounds[i].MonsterID, level.Rounds[i].Count));
            }
            sb.AppendLine("</Rounds>");

            sb.AppendLine("</Level>");

            string content = sb.ToString();

            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                ConformanceLevel = ConformanceLevel.Auto,
                IndentChars = "\t",
                OmitXmlDeclaration = false
            };

            XmlWriter xw = XmlWriter.Create(fileName, settings);

            StreamWriter sw = new StreamWriter(fileName, false, Encoding.UTF8);
            sw.Write(content);
            sw.Flush();
            sw.Dispose();
        }

        //加载图片
        public static IEnumerator LoadImage(string url, SpriteRenderer render)
        {
            WWW www = new WWW(url);

            while (!www.isDone)
                yield return www;

            Texture2D texture = www.texture;
            Sprite sp = Sprite.Create(
                texture,
                new Rect(0, 0, texture.width, texture.height),
                new Vector2(0.5f, 0.5f));
            render.sprite = sp;
        }

        //加载图片
        public static IEnumerator LoadImage(string url, Image image)
        {
            WWW www = new WWW(url);

            while (!www.isDone)
                yield return www;

            Texture2D texture = www.texture;
            Sprite sp = Sprite.Create(
                texture,
                new Rect(0, 0, texture.width, texture.height),
                new Vector2(0.5f, 0.5f));
            image.sprite = sp;
        }
    }
}

