//  Felix-Bang：Map
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
// Describe：地图 描述一个关卡地图状态
// Createtime：2018/9/10


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FBApplication
{
    //鼠标点击参数类
    public class FBGridClickEventArgs : EventArgs
    {
        public int MouseButton;  //0左键，1右键
        public FBGrid Grid;

        public FBGridClickEventArgs(int mouseButton, FBGrid grid)
        {
            this.MouseButton = mouseButton;
            this.Grid = grid;
        }
    }

	public class FBMap : MonoBehaviour
	{
        #region 常量
        public const int RowCount = 8;           //行数
        public const int ColumnCount = 12;       //列数
        #endregion

        #region 事件
        public EventHandler<FBGridClickEventArgs> OnFBGridClick;
        #endregion

        #region 字段
        float f_mapWidth;
        float f_mapHeight;
        float f_tileWidth;
        float f_tileHeight;

        List<FBGrid> f_grid = new List<FBGrid>();          //背景网格
        List<FBGrid> f_road = new List<FBGrid>();          //道路网个
        
        private FBLevel f_level;

        public bool IsDrawGizoms = true;                 //是否绘制网格      
        
        #endregion

        #region 属性
        public FBLevel Level
        {
            get { return f_level; }
        }

        public string BackgroundImage
        {
            set
            {
                SpriteRenderer render = transform.Find("Background").GetComponent<SpriteRenderer>();
                StartCoroutine(FBTools.LoadImage(value, render));
            }
        }

        public string RoadImage
        {
            set
            {
                SpriteRenderer render = transform.Find("Road").GetComponent<SpriteRenderer>();
                StartCoroutine(FBTools.LoadImage(value, render));
            }
        }

        public List<FBGrid> Grids

        {
            get { return f_grid; }
        }

        public List<FBGrid> Road
        {
            get { return f_road; }
        }

        //怪物的寻路路径
        public Vector3[] Path
        {
            get
            {
                List<Vector3> f_path = new List<Vector3>();
                for (int i = 0; i < f_road.Count; i++)
                {
                    FBGrid t = f_road[i];
                    Vector3 point = GetPosition(t);
                    f_path.Add(point);
                }
                return f_path.ToArray();
            }
        }

        public Rect MapRect
        {
            get { return new Rect(-f_mapWidth / 2,-f_mapHeight / 2, f_mapWidth, f_mapHeight); }
        }

        #endregion

        #region 方法
        public void LoadLevel(FBLevel level)
        {
            //清除当前状态
            Clear();

            //保存
            f_level = level;

            //加载图片
            BackgroundImage = "file://" + FBConsts.MpsDir + "/" + level.Background;
            RoadImage = "file://" + FBConsts.MpsDir + "/" + level.Road;

            //寻路点
            for (int i = 0; i < level.Path.Count; i++)
            {
                FBCoords c = level.Path[i];
                FBGrid t = GetGrid(c.X, c.Y);
                f_road.Add(t);
            }

            //炮塔点
            for (int i = 0; i < level.Holders.Count; i++)
            {
                FBCoords c = level.Holders[i];
                FBGrid t = GetGrid(c.X, c.Y);
                t.CanHold = true;
            }
        }

        //清除塔位信息
        public void ClearHolder()
        {
            foreach (FBGrid g in f_grid)
            {
                if (g.CanHold)
                    g.CanHold = false;
            }
        }

        //清除寻路格子集合
        public void ClearRoad()
        {
            f_road.Clear();
        }

        //清除所有信息
        public void Clear()
        {
            f_level = null;
            ClearHolder();
            ClearRoad();
        }

        #endregion

        #region Unity回调

        //只在运行期起作用
        void Awake()
        {
            //计算地图和格子大小
            CalculateSize();

            //创建所有的格子
            for (int i = 0; i < RowCount; i++)
                for (int j = 0; j < ColumnCount; j++)
                    f_grid.Add(new FBGrid(j, i));

            //监听鼠标点击事件
            OnFBGridClick += MapOnGridClick;         
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                FBGrid g = GetTileUnderMouse();

                if (g != null)
                {
                    //触发鼠标左键点击事件
                    FBGridClickEventArgs e = new FBGridClickEventArgs(0,g);
                    if (OnFBGridClick != null)
                        OnFBGridClick(this, e);
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                FBGrid g = GetTileUnderMouse();
 
                if (g != null)
                {
                    //触发鼠标右键点击事件
                    FBGridClickEventArgs e = new FBGridClickEventArgs(1, g);
                    if (OnFBGridClick != null)
                        OnFBGridClick(this, e);
                }
            }
        }

        //只在编辑器里起作用 类似Update不停的执行
        void OnDrawGizmos()
        {
            if (!IsDrawGizoms)
                return;

            //计算地图和格子大小
            CalculateSize();

            //绘制格子
            Gizmos.color = Color.green;

            //绘制行
            for (int row = 0; row <= RowCount; row++)
            {
                Vector2 from = new Vector2(-f_mapWidth / 2, -f_mapHeight / 2 + row * f_tileHeight);
                Vector2 to = new Vector2(-f_mapWidth / 2 + f_mapWidth, -f_mapHeight / 2 + row * f_tileHeight);
                Gizmos.DrawLine(from, to);
            }

            //绘制列
            for (int col = 0; col <= ColumnCount; col++)
            {
                Vector2 from = new Vector2(-f_mapWidth / 2 + col * f_tileWidth, f_mapHeight / 2);
                Vector2 to = new Vector2(-f_mapWidth / 2 + col * f_tileWidth, -f_mapHeight / 2);
                Gizmos.DrawLine(from, to);
            }

            foreach (FBGrid g in f_grid)
            {
                if (g.CanHold)
                {
                    Vector3 pos = GetPosition(g);
                    Gizmos.DrawIcon(pos, "holder.png", true);
                }
            }

            Gizmos.color = Color.red;
            for (int i = 0; i < f_road.Count; i++)
            {
                //起点
                if (i == 0)
                    Gizmos.DrawIcon(GetPosition(f_road[i]), "start.png", true);

                //终点
                if (f_road.Count > 1 && i == f_road.Count - 1)
                    Gizmos.DrawIcon(GetPosition(f_road[i]), "end.png", true);

                //红色的连线
                if (f_road.Count > 1 && i != 0)
                {
                    Vector3 from = GetPosition(f_road[i - 1]);
                    Vector3 to = GetPosition(f_road[i]);
                    Gizmos.DrawLine(from, to);
                }
            }
        }
        #endregion

        #region 事件回调
        private void MapOnGridClick(object sender, FBGridClickEventArgs e)
        {
            //当前场景不是LevelBuilder 不能编辑
            if (gameObject.scene.name != "LevelBuilder")
                return;

            if (Level == null)
                return;           

            if (e.MouseButton == 0 && !f_road.Contains(e.Grid))
                e.Grid.CanHold = !e.Grid.CanHold;

            if (e.MouseButton == 1 && !e.Grid.CanHold)
            {
                if (f_road.Contains(e.Grid))
                    f_road.Remove(e.Grid);
                else
                    f_road.Add(e.Grid);
            }                 
        }
        #endregion

        #region 帮助方法
        //计算地图大小 网格大小
        void CalculateSize()
        {
            Vector3 pos1 = Camera.main.ViewportToWorldPoint(new Vector3(0, 0));
            Vector3 pos2 = Camera.main.ViewportToWorldPoint(new Vector3(1, 1));

            f_mapWidth = pos2.x - pos1.x;
            f_mapHeight = pos2.y - pos1.y;

            f_tileWidth = f_mapWidth / ColumnCount;
            f_tileHeight = f_mapHeight / RowCount;
        }

        /// <summary>
        /// 根据网格获取网格的世界坐标
        /// </summary>
        /// <param name="g"> 网格 </param>
        /// <returns></returns>
        public Vector3 GetPosition(FBGrid g)
        {
            return new Vector3(
                -f_mapWidth / 2 + (g.Index_X + 0.5f) * f_tileWidth,
                -f_mapHeight / 2 + (g.Index_Y + 0.5f) * f_tileHeight,
                0);
        }

        /// <summary> 根据索引获取网格 </summary>>
        FBGrid GetGrid(int x, int y)
        {
            int index = x + y * ColumnCount;

            if (index < 0 || index >= f_grid.Count)
                return null;

            return f_grid[index];
        }

        /// <summary>
        /// 根据位置获取网格
        /// </summary>
        /// <param name="position">坐标</param>
        /// <returns></returns>
        public FBGrid GetGrid(Vector3 position)
        {
            int gridX=(int)((position.x + f_mapWidth / 2) / f_tileWidth);
            int gridY = (int)((position.y + f_mapHeight / 2) / f_tileHeight);
            return GetGrid(gridX, gridY);
        }

        /// <summary> 获取鼠标下面的格子 <summary>
        FBGrid GetTileUnderMouse()
        {
            Vector2 wordPos = GetWorldPosition();
            //int col = (int)((wordPos.x + f_mapWidth / 2) / f_tileWidth);
            //int row = (int)((wordPos.y + f_mapHeight / 2) / f_tileHeight);
            return GetGrid(wordPos);
        }

        /// <summary> 获取鼠标所在位置的世界坐标 <summary>
        Vector3 GetWorldPosition()
        {
            Vector3 viewPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            Vector3 worldPos = Camera.main.ViewportToWorldPoint(viewPos);
            return worldPos;
        }
        #endregion
    }
}

