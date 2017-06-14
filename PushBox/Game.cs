using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace 推箱子
{
    //枚举类型在命名空间内命名都行
    public enum State
    {
        Person,
        Box,
        Wall,
        Target,
        Empty,         //指游戏区域内的空白区
        PersonTarget,  //指人刚好站在所在目标的格子
        BoxTarget,     //箱子在目标上
        None           //游戏区域外的空白地区，自定义地图时用到
    }

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    [Serializable]
    public class Game : ICloneable
    {
        #region 说明
        //继承ICloneable接口，必须实现Clone()函数
        //C#下的接口在语法上类似C++抽象基类，继承了接口，必须要实现接口指定的
        //函数，不然编译不通过
        //这里要求实现Game类Clone()函数，允许Game类对象进行深复制 
        #endregion
        //定义地图大小
        //Game类的数据
        private static int row = 10;
        private static int col = 12;
        public State[,] map = new State[Row, Col];
        public System.Drawing.Point personLoc;   //人所在的位置(行/列)
        private int targetNum;

        public int TargetNum
        {
            get { return targetNum; }
            set { targetNum = value; }
        }
        
        public static int Row
        {
            get { return row; }
        }

        public static int Col
        {
            get { return col; }
        }

        //Game类的构造函数
        public Game()
        {
        }
        public object Clone()
        { 
            Game game = new Game();
            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Col; j++)
                {
                    game.map[i, j] = this.map[i, j];
                }
            }
            game.personLoc = this.personLoc;
            game.TargetNum = this.TargetNum;
            return game; 
        }
    }
}
