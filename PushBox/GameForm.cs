using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace 推箱子
{
    public partial class GameForm : Form
    {
        private string fileName = "GameLevelsData.txt";     //读/写关卡数据文件名
        private string fileName2 = "GameScoreData.txt";     //读写排行榜数据文件名

        //游戏数据设置
        Game game1 = new Game();                                           //初始值为地图状态
        Game game = new Game();                                           //初始值为地图状态
        Game currentGame = new Game();                                    //记录游戏进行中的地图状态
        GameScore Currentgamescore = new GameScore();                   //记录当前正在游戏中的用户的信息
        public List<Game> gameList = new List<Game>();                   //装载初始化的地图
        List<GameScore> gameScoreList = new List<GameScore>();          //装载排行榜的数据
        List<Game> BackStepMap = new List<Game>();                      //装载上一步的地图
        List<int> BackStepMove = new List<int>();                       //记录上一步地图的步数
        public int ModeNum = 0;                                         //模式序号
        private int BoxTarget = 0;                                      //用于判断当前关卡是否通过
        bool CanWrite = false;
        public GameForm(int modenum)
        {
            InitializeComponent();
            this.ModeNum = modenum;

        }

        //以下数据在statusStrip显示
        private string CurrentMode = "";                         //当前模式
        private int CurrentStepCounts = 0;                       //当前用户推过箱子的步数，步数越少，则水平越高
        private int CurrentLevel = 0;                            //当前关卡=CurrentLevel+1;

        public void InitGameMap(int modenum)                       //根据当前模式，对当前窗体显示的内容进行初始化
        {
            if (modenum == 0)
            {
                CurrentMode = "闯关模式";
                CurrentLevel = 0;
                CurrentStepCounts = 0;
                toolStripComboBox_Levels.Visible = false;
                toolStripLabel_Level.Visible = false;
            }
            else if (modenum == 1)
            {
                CurrentMode = "选关模式";
                CurrentLevel = 0;
                CurrentStepCounts = 0;
                toolStripComboBox_Levels.Visible = true;
                toolStripLabel_Level.Visible = true;
                toolStripComboBox_Levels.SelectedIndex = 0;
            }
            else if (modenum == 2)
            {
                CurrentMode = "我的地图模式";
                CurrentLevel = 0;
                CurrentStepCounts = 0;
                toolStripComboBox_Levels.Visible = false;
                toolStripLabel_Level.Visible = false;

            }
            currentGame = (Game)gameList[CurrentLevel].Clone();
            game = (Game)gameList[CurrentLevel].Clone();
        }

        public bool IsPassedCurrentLevels(int currentlevel)        //判断用户是否通过当前关卡
        {
            BoxTarget = 0;
            for (int i = 0; i < Game.Row; i++)
            {
                for (int j = 0; j < Game.Col; j++)
                {
                    if (currentGame.map[i, j] == State.BoxTarget)
                    {
                        BoxTarget++;
                    }
                    else continue;
                }
            }
            if (BoxTarget == currentGame.TargetNum && currentGame.TargetNum != 0 && BoxTarget != 0)
            {
                return true;
            }
            else return false;
        }


        #region 读/写关卡数据
        private void WriteGameList()                 //写当前gameList的数据进二进制序列化文件,暂时用不上
        {
            FileStream stream = null;
            stream = new FileStream(fileName, FileMode.Create);
            BinaryFormatter bfFormatter = new BinaryFormatter();
            bfFormatter.Serialize(stream, gameList);
            stream.Close();
        }
        private void ReadGameList()                  //从二进制序列化文件中读取初始化的地图数据
        {
            FileStream stream = null;
            try
            {
                stream = new FileStream(fileName, FileMode.Open);
            }
            catch (Exception ex)
            {
                MessageBox.Show("打开文件失败," + ex.Message);
                return;
            }
            BinaryFormatter bFormatter = new BinaryFormatter();
            gameList = (List<Game>)bFormatter.Deserialize(stream);
            stream.Close();
        }
        #endregion

        #region 读/写排行榜数据
        private void WriteGameScoreList()                 //写当前gameScoreList的数据进二进制序列化文件
        {
            FileStream stream = null;
            stream = new FileStream(fileName2, FileMode.Create);
            BinaryFormatter bfFormatter = new BinaryFormatter();
            bfFormatter.Serialize(stream, gameScoreList);
            stream.Close();
        }

        private void ReadGameScoreList()                  //从二进制序列化文件中读取初始化的地图数据
        {
            FileStream stream = null;
            try
            {
                stream = new FileStream(fileName2, FileMode.Open);
            }
            catch (Exception ex)
            {
                MessageBox.Show("打开文件失败," + ex.Message);
                return;
            }
            BinaryFormatter bFormatter = new BinaryFormatter();
            gameScoreList = (List<GameScore>)bFormatter.Deserialize(stream);
            stream.Close();
        }
        #endregion

        private void GameForm_Load(object sender, EventArgs e)
        {
            if (ModeNum == 0 || ModeNum == 1)
            {
                ReadGameList();                             
            }
            else if (ModeNum == 2)
            {
                fileName = "Player's Data.txt";
                ReadGameList();
            }
            InitGameMap(ModeNum);
            ReadGameScoreList();


            SetStyle(ControlStyles.ResizeRedraw
                | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.OptimizedDoubleBuffer, true);
            UpdateStyles();

            this.MaximizeBox = false;  //设置窗体不能被最大化且不会被拉伸
            this.Size = new Size
              (GameImages.sideLength * Game.Col, GameImages.sideLength * Game.Row + (this.Size.Height - this.ClientRectangle.Size.Height + 50));
            this.MaximumSize = this.Size;
            pictureBox_GameForm.Size = new Size(GameImages.sideLength * Game.Col, GameImages.sideLength * Game.Row);
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            Bitmap gameImage = new Bitmap(GameImages.sideLength * Game.Col, GameImages.sideLength * Game.Row);
            Graphics g = Graphics.FromImage(gameImage);
            this.DrawMap(currentGame, g);
            g.Dispose();
            g = e.Graphics;
            g.DrawImage(gameImage, 0, 0);
        }

        private void DrawMap(Game currentGame, Graphics g)
        {
            Rectangle cellRect = new Rectangle(0, 0, GameImages.sideLength, GameImages.sideLength);
            for (int i = 0; i < Game.Row; i++)
            {
                for (int j = 0; j < Game.Col; j++)
                {
                    cellRect.X = GameImages.sideLength * j;
                    cellRect.Y = GameImages.sideLength * i;
                    g.DrawImage(GameImages.getImage(currentGame.map[i, j]), cellRect);
                }
            }
        }

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            Game tempMap = new Game();
            int tempStep = 0;
            tempStep = CurrentStepCounts;
            tempMap = (Game)currentGame.Clone();
            BackStepMap.Add(tempMap);
            BackStepMove.Add(tempStep);
            

            #region 判断按键方向对direction赋值
            Direction direction = Direction.Up;
            if (e.KeyCode == Keys.Up)
            {
                direction = Direction.Up;
            }
            else if (e.KeyCode == Keys.Down)
            {
                direction = Direction.Down;
            }
            else if (e.KeyCode == Keys.Left)
            {
                direction = Direction.Left;
            }
            else if (e.KeyCode == Keys.Right)
            {
                direction = Direction.Right;
            }
            else
            {
                return;
            }
            #endregion

            #region 走法判断
            switch (direction)
            {
                case Direction.Up:    //方向向上
                    {

                        #region 上方是箱子
                        if (currentGame.personLoc.X - 1 >= 0 &&
                            (currentGame.map[currentGame.personLoc.X - 1, currentGame.personLoc.Y] == State.Box ||
                            currentGame.map[currentGame.personLoc.X - 1, currentGame.personLoc.Y] == State.BoxTarget))
                        {

                            #region 在目标上的箱子
                            //在目标上的箱子，又分为两种情况，人在目标上推，人不在目标上推
                            if (currentGame.map[currentGame.personLoc.X - 1, currentGame.personLoc.Y] == State.BoxTarget)
                            {
                                //人在目标上推
                                if (currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] == State.PersonTarget)
                                {
                                    //箱子上面是空地
                                    if (currentGame.personLoc.X - 2 >= 0 && currentGame.map[currentGame.personLoc.X - 2, currentGame.personLoc.Y] == State.Empty)
                                    {
                                        currentGame.map[currentGame.personLoc.X - 2, currentGame.personLoc.Y] = State.Box;
                                        currentGame.map[currentGame.personLoc.X - 1, currentGame.personLoc.Y] = State.PersonTarget;
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] = State.Target;
                                        currentGame.personLoc.X -= 1;
                                        CurrentStepCounts++;
                                    }

                                    //箱子上面是目标
                                    else if (currentGame.personLoc.X - 2 >= 0 && currentGame.map[currentGame.personLoc.X - 2, currentGame.personLoc.Y] == State.Target)
                                    {
                                        currentGame.map[currentGame.personLoc.X - 2, currentGame.personLoc.Y] = State.BoxTarget;
                                        currentGame.map[currentGame.personLoc.X - 1, currentGame.personLoc.Y] = State.PersonTarget;
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] = State.Target;
                                        currentGame.personLoc.X -= 1;
                                        CurrentStepCounts++;
                                    }
                                }

                                //人不在目标上推
                                else if (currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] == State.Person)
                                {
                                    //箱子上面是空地
                                    if (currentGame.personLoc.X - 2 >= 0 && currentGame.map[currentGame.personLoc.X - 2, currentGame.personLoc.Y] == State.Empty)
                                    {
                                        currentGame.map[currentGame.personLoc.X - 2, currentGame.personLoc.Y] = State.Box;
                                        currentGame.map[currentGame.personLoc.X - 1, currentGame.personLoc.Y] = State.PersonTarget;
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] = State.Empty;
                                        currentGame.personLoc.X -= 1;
                                        CurrentStepCounts++;
                                    }
                                    //箱子上面是目标
                                    else if (currentGame.personLoc.X - 2 >= 0 && currentGame.map[currentGame.personLoc.X - 2, currentGame.personLoc.Y] == State.Target)
                                    {
                                        currentGame.map[currentGame.personLoc.X - 2, currentGame.personLoc.Y] = State.BoxTarget;
                                        currentGame.map[currentGame.personLoc.X - 1, currentGame.personLoc.Y] = State.PersonTarget;
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] = State.Empty;
                                        currentGame.personLoc.X -= 1;
                                        CurrentStepCounts++;
                                    }
                                }
                            }
                            #endregion

                            #region 不在目标上的箱子

                            else if (currentGame.map[currentGame.personLoc.X - 1, currentGame.personLoc.Y] == State.Box)
                            {
                                //人在目标上推
                                if (currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] == State.PersonTarget)
                                {
                                    //箱子上面是空地
                                    if (currentGame.personLoc.X - 2 >= 0 && currentGame.map[currentGame.personLoc.X - 2, currentGame.personLoc.Y] == State.Empty)
                                    {
                                        currentGame.map[currentGame.personLoc.X - 2, currentGame.personLoc.Y] = State.Box;
                                        currentGame.map[currentGame.personLoc.X - 1, currentGame.personLoc.Y] = State.Person;
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] = State.Target;
                                        currentGame.personLoc.X -= 1;
                                        CurrentStepCounts++;
                                    }
                                    //箱子上面是目标
                                    else if (currentGame.personLoc.X - 2 >= 0 && currentGame.map[currentGame.personLoc.X - 2, currentGame.personLoc.Y] == State.Target)
                                    {
                                        currentGame.map[currentGame.personLoc.X - 2, currentGame.personLoc.Y] = State.BoxTarget;
                                        currentGame.map[currentGame.personLoc.X - 1, currentGame.personLoc.Y] = State.Person;
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] = State.Target;
                                        currentGame.personLoc.X -= 1;
                                        CurrentStepCounts++;

                                    }
                                }
                                //人不在目标上推不在目标上的箱子
                                else if (currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] == State.Person)
                                {
                                    //箱子上面是空地
                                    if (currentGame.personLoc.X - 2 >= 0 &&
                                        currentGame.map[currentGame.personLoc.X - 2, currentGame.personLoc.Y] == State.Empty)
                                    {
                                        currentGame.map[currentGame.personLoc.X - 2, currentGame.personLoc.Y] = State.Box;
                                        currentGame.map[currentGame.personLoc.X - 1, currentGame.personLoc.Y] = State.Person;
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] = State.Empty;
                                        currentGame.personLoc.X -= 1;
                                        CurrentStepCounts++;
                                    }
                                    //箱子上面是目标
                                    else if (currentGame.personLoc.X - 2 >= 0 &&
                                        currentGame.map[currentGame.personLoc.X - 2, currentGame.personLoc.Y] == State.Target)
                                    {
                                        currentGame.map[currentGame.personLoc.X - 2, currentGame.personLoc.Y] = State.BoxTarget;
                                        currentGame.map[currentGame.personLoc.X - 1, currentGame.personLoc.Y] = State.Person;
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] = State.Empty;
                                        currentGame.personLoc.X -= 1;
                                        CurrentStepCounts++;
                                    }
                                }
                            }
                            #endregion
                        }

                        #endregion

                        #region 上方是目标
                        else if (currentGame.personLoc.X - 1 >= 0 &&
                            currentGame.map[currentGame.personLoc.X - 1, currentGame.personLoc.Y] == State.Target)
                        {
                            //人在目标上
                            if (currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] == State.PersonTarget)
                            {
                                currentGame.map[currentGame.personLoc.X - 1, currentGame.personLoc.Y] = State.PersonTarget;
                                currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] = State.Target;
                                currentGame.personLoc.X -= 1;
                            }
                            //人不在目标上
                            else if (currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] == State.Person)
                            {
                                currentGame.map[currentGame.personLoc.X - 1, currentGame.personLoc.Y] = State.PersonTarget;
                                currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] = State.Empty;
                                currentGame.personLoc.X -= 1;
                            }
                        }
                        #endregion

                        #region 上方是空地
                        else if (currentGame.personLoc.X - 1 >= 0 &&
                            currentGame.map[currentGame.personLoc.X - 1, currentGame.personLoc.Y] == State.Empty)
                        {
                            //人在目标上
                            if (currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] == State.PersonTarget)
                            {
                                currentGame.map[currentGame.personLoc.X - 1, currentGame.personLoc.Y] = State.Person;
                                currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] = State.Target;
                                currentGame.personLoc.X -= 1;
                            }
                            //人不在目标上
                            else if (currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] == State.Person)
                            {
                                currentGame.map[currentGame.personLoc.X - 1, currentGame.personLoc.Y] = State.Person;
                                currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] = State.Empty;
                                currentGame.personLoc.X -= 1;
                            }
                        }
                        #endregion

                    }
                    break;

                case Direction.Down:   //方向向下
                    {
                        #region 下方是箱子
                        if (currentGame.personLoc.X + 1 <= Game.Row - 1 &&
                            (currentGame.map[currentGame.personLoc.X + 1, currentGame.personLoc.Y] == State.Box ||
                            currentGame.map[currentGame.personLoc.X + 1, currentGame.personLoc.Y] == State.BoxTarget))
                        {
                            #region 在目标上的箱子
                            //在目标上的箱子，又分为两种情况，人在目标上推在目标上的箱子，人不在目标上推在目标上的箱子
                            if (currentGame.map[currentGame.personLoc.X + 1, currentGame.personLoc.Y] == State.BoxTarget)
                            {
                                //人在目标上推
                                if (currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] == State.PersonTarget)
                                {
                                    //箱子上面是空地
                                    if (currentGame.personLoc.X + 2 <= Game.Row - 1 && currentGame.map[currentGame.personLoc.X + 2, currentGame.personLoc.Y] == State.Empty)
                                    {
                                        currentGame.map[currentGame.personLoc.X + 2, currentGame.personLoc.Y] = State.Box;
                                        currentGame.map[currentGame.personLoc.X + 1, currentGame.personLoc.Y] = State.PersonTarget;
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] = State.Target;
                                        currentGame.personLoc.X += 1;
                                        CurrentStepCounts++;
                                    }

                                    //箱子上面是目标
                                    else if (currentGame.personLoc.X + 2 <= Game.Row - 1 && currentGame.map[currentGame.personLoc.X + 2, currentGame.personLoc.Y] == State.Target)
                                    {
                                        currentGame.map[currentGame.personLoc.X + 2, currentGame.personLoc.Y] = State.BoxTarget;
                                        currentGame.map[currentGame.personLoc.X + 1, currentGame.personLoc.Y] = State.PersonTarget;
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] = State.Target;
                                        currentGame.personLoc.X += 1;
                                        CurrentStepCounts++;
                                    }
                                }

                                //人不在目标上推目标上的箱子
                                else if (currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] == State.Person)
                                {
                                    //箱子上面是空地
                                    if (currentGame.personLoc.X + 2 <= Game.Row - 1 && currentGame.map[currentGame.personLoc.X + 2, currentGame.personLoc.Y] == State.Empty)
                                    {
                                        currentGame.map[currentGame.personLoc.X + 2, currentGame.personLoc.Y] = State.Box;
                                        currentGame.map[currentGame.personLoc.X + 1, currentGame.personLoc.Y] = State.PersonTarget;
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] = State.Empty;
                                        currentGame.personLoc.X += 1;
                                        CurrentStepCounts++;
                                    }
                                    //箱子上面是目标
                                    else if (currentGame.personLoc.X + 2 <= Game.Row - 1 && currentGame.map[currentGame.personLoc.X + 2, currentGame.personLoc.Y] == State.Target)
                                    {
                                        currentGame.map[currentGame.personLoc.X + 2, currentGame.personLoc.Y] = State.BoxTarget;
                                        currentGame.map[currentGame.personLoc.X + 1, currentGame.personLoc.Y] = State.PersonTarget;
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] = State.Empty;
                                        currentGame.personLoc.X += 1;
                                        CurrentStepCounts++;
                                    }
                                }
                            }
                            #endregion

                            #region 不在目标上的箱子

                            if (currentGame.map[currentGame.personLoc.X + 1, currentGame.personLoc.Y] == State.Box)
                            {
                                //人在目标上推不在目标上的箱子
                                if (currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] == State.PersonTarget)
                                {
                                    //箱子下面是空地
                                    if (currentGame.personLoc.X + 2 <= Game.Row - 1 && currentGame.map[currentGame.personLoc.X + 2, currentGame.personLoc.Y] == State.Empty)
                                    {
                                        currentGame.map[currentGame.personLoc.X + 2, currentGame.personLoc.Y] = State.Box;
                                        currentGame.map[currentGame.personLoc.X + 1, currentGame.personLoc.Y] = State.Person;
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] = State.Target;
                                        currentGame.personLoc.X += 1;
                                        CurrentStepCounts++;
                                    }
                                    //箱子下面是目标
                                    else if (currentGame.personLoc.X + 2 <= Game.Row - 1 && currentGame.map[currentGame.personLoc.X + 2, currentGame.personLoc.Y] == State.Target)
                                    {
                                        currentGame.map[currentGame.personLoc.X + 2, currentGame.personLoc.Y] = State.BoxTarget;
                                        currentGame.map[currentGame.personLoc.X + 1, currentGame.personLoc.Y] = State.Person;
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] = State.Target;
                                        currentGame.personLoc.X += 1;
                                        CurrentStepCounts++;

                                    }
                                }
                                //人不在目标上推不在目标上的箱子
                                else if (currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] == State.Person)
                                {
                                    //箱子下面是空地
                                    if (currentGame.personLoc.X + 2 <= Game.Row - 1 &&
                                        currentGame.map[currentGame.personLoc.X + 2, currentGame.personLoc.Y] == State.Empty)
                                    {
                                        currentGame.map[currentGame.personLoc.X + 2, currentGame.personLoc.Y] = State.Box;
                                        currentGame.map[currentGame.personLoc.X + 1, currentGame.personLoc.Y] = State.Person;
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] = State.Empty;
                                        currentGame.personLoc.X += 1;
                                        CurrentStepCounts++;
                                    }
                                    //箱子下面是目标
                                    else if (currentGame.personLoc.X + 2 <= Game.Row - 1 &&
                                        currentGame.map[currentGame.personLoc.X + 2, currentGame.personLoc.Y] == State.Target)
                                    {
                                        currentGame.map[currentGame.personLoc.X + 2, currentGame.personLoc.Y] = State.BoxTarget;
                                        currentGame.map[currentGame.personLoc.X + 1, currentGame.personLoc.Y] = State.Person;
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] = State.Empty;
                                        currentGame.personLoc.X += 1;
                                        CurrentStepCounts++;
                                    }
                                }
                            }
                            #endregion
                        }

                        #endregion

                        #region 下方是目标
                        else if (currentGame.personLoc.X + 1 <= Game.Row - 1 &&
                            currentGame.map[currentGame.personLoc.X + 1, currentGame.personLoc.Y] == State.Target)
                        {
                            //人在目标上
                            if (currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] == State.PersonTarget)
                            {
                                currentGame.map[currentGame.personLoc.X + 1, currentGame.personLoc.Y] = State.PersonTarget;
                                currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] = State.Target;
                                currentGame.personLoc.X += 1;
                            }

                            //人不在目标上
                            else if (currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] == State.Person)
                            {
                                currentGame.map[currentGame.personLoc.X + 1, currentGame.personLoc.Y] = State.PersonTarget;
                                currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] = State.Empty;
                                currentGame.personLoc.X += 1;
                            }
                        }
                        #endregion

                        #region 下方是空地
                        else if (currentGame.personLoc.X + 1 <= Game.Row - 1 &&
                           currentGame.map[currentGame.personLoc.X + 1, currentGame.personLoc.Y] == State.Empty)
                        {
                            //人在目标上
                            if (currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] == State.PersonTarget)
                            {
                                currentGame.map[currentGame.personLoc.X + 1, currentGame.personLoc.Y] = State.Person;
                                currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] = State.Target;
                                currentGame.personLoc.X += 1;
                            }
                            //人不在目标上
                            else if (currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] == State.Person)
                            {
                                currentGame.map[currentGame.personLoc.X + 1, currentGame.personLoc.Y] = State.Person;
                                currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] = State.Empty;
                                currentGame.personLoc.X += 1;
                            }
                        }
                        #endregion
                    }
                    break;

                case Direction.Left:   //方向向左
                    {
                        #region 左边是箱子
                        if (currentGame.personLoc.Y - 1 >= 0 &&
                            (currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y - 1] == State.Box ||
                            currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y - 1] == State.BoxTarget))
                        {
                            #region 在目标上的箱子
                            //在目标上的箱子，又分为两种情况，人在目标上推在目标上的箱子，人不在目标上推在目标上的箱子
                            if (currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y - 1] == State.BoxTarget)
                            {
                                //人在目标上推
                                if (currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] == State.PersonTarget)
                                {
                                    //箱子左边是空地
                                    if (currentGame.personLoc.Y - 2 >= 0 && currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y - 2] == State.Empty)
                                    {
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y - 2] = State.Box;
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y - 1] = State.PersonTarget;
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] = State.Target;
                                        currentGame.personLoc.Y -= 1;
                                        CurrentStepCounts++;
                                    }

                                    //箱子左边是目标
                                    else if (currentGame.personLoc.Y - 2 >= 0 && currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y - 2] == State.Target)
                                    {
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y - 2] = State.BoxTarget;
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y - 1] = State.PersonTarget;
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] = State.Target;
                                        currentGame.personLoc.Y -= 1;
                                        CurrentStepCounts++;
                                    }
                                }

                                //人不在目标上推在目标上的箱子
                                else if (currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] == State.Person)
                                {
                                    //箱子左边是空地
                                    if (currentGame.personLoc.Y - 2 >= 0 && currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y - 2] == State.Empty)
                                    {
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y - 2] = State.Box;
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y - 1] = State.PersonTarget;
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] = State.Empty;
                                        currentGame.personLoc.Y -= 1;
                                        CurrentStepCounts++;
                                    }
                                    //箱子左边是目标
                                    else if (currentGame.personLoc.Y - 2 >= 0 && currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y - 2] == State.Target)
                                    {
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y - 2] = State.BoxTarget;
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y - 1] = State.PersonTarget;
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] = State.Empty;
                                        currentGame.personLoc.Y -= 1;
                                        CurrentStepCounts++;
                                    }
                                }
                            }
                            #endregion

                            #region 不在目标上的箱子

                            else if (currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y - 1] == State.Box)
                            {
                                //人在目标上推不在目标上的箱子
                                if (currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] == State.PersonTarget)
                                {
                                    //箱子左边是空地
                                    if (currentGame.personLoc.Y - 2 >= 0 && currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y - 2] == State.Empty)
                                    {
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y - 2] = State.Box;
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y - 1] = State.Person;
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] = State.Target;
                                        currentGame.personLoc.Y -= 1;
                                        CurrentStepCounts++;
                                    }
                                    //箱子左边是目标
                                    else if (currentGame.personLoc.Y - 2 >= 0 && currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y - 2] == State.Target)
                                    {
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y - 2] = State.BoxTarget;
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y - 1] = State.Person;
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] = State.Target;
                                        currentGame.personLoc.Y -= 1;
                                        CurrentStepCounts++;

                                    }
                                }
                                //人不在目标上推不在目标上的箱子
                                else if (currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] == State.Person)
                                {
                                    //箱子左边是空地
                                    if (currentGame.personLoc.Y - 2 >= 0 &&
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y - 2] == State.Empty)
                                    {
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y - 2] = State.Box;
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y - 1] = State.Person;
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] = State.Empty;
                                        currentGame.personLoc.Y -= 1;
                                        CurrentStepCounts++;
                                    }
                                    //箱子左边是目标
                                    else if (currentGame.personLoc.Y - 2 >= 0 &&
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y - 2] == State.Target)
                                    {
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y - 2] = State.BoxTarget;
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y - 1] = State.Person;
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] = State.Empty;
                                        currentGame.personLoc.Y -= 1;
                                        CurrentStepCounts++;
                                    }
                                }
                            }
                            #endregion
                        }

                        #endregion

                        #region 左边是目标
                        else if (currentGame.personLoc.Y - 1 >= 0 &&
                                  currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y - 1] == State.Target)
                        {
                            //人在目标上
                            if (currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] == State.PersonTarget)
                            {
                                currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y - 1] = State.PersonTarget;
                                currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] = State.Target;
                                currentGame.personLoc.Y -= 1;
                            }
                            //人不在目标上
                            else if (currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] == State.Person)
                            {
                                currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y - 1] = State.PersonTarget;
                                currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] = State.Empty;
                                currentGame.personLoc.Y -= 1;
                            }
                        }
                        #endregion

                        #region 左边是空地

                        else if (currentGame.personLoc.Y - 1 >= 0 &&
                                   currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y - 1] == State.Empty)
                        {
                            //人在目标上
                            if (currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] == State.PersonTarget)
                            {
                                currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y - 1] = State.Person;
                                currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] = State.Target;
                                currentGame.personLoc.Y -= 1;
                            }
                            //人不在目标上
                            else if (currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] == State.Person)
                            {
                                currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y - 1] = State.Person;
                                currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] = State.Empty;
                                currentGame.personLoc.Y -= 1;
                            }
                        }
                        #endregion
                    }
                    break;

                case Direction.Right:  //方向向右
                    {
                        #region 右边是箱子
                        if (currentGame.personLoc.Y + 1 <= Game.Col - 1 &&
                            (currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y + 1] == State.Box ||
                            currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y + 1] == State.BoxTarget))
                        {
                            #region 在目标上的箱子
                            //在目标上的箱子，又分为两种情况，人在目标上推在目标上的箱子，人不在目标上推在目标上的箱子
                            if (currentGame.personLoc.Y + 1 <= Game.Col - 1 &&
                                                currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y + 1] == State.BoxTarget)
                            {
                                //人在目标上推在目标上的箱子
                                if (currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] == State.PersonTarget)
                                {
                                    //箱子右边是空地
                                    if (currentGame.personLoc.Y + 2 <= Game.Col - 1 && currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y + 2] == State.Empty)
                                    {
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y + 2] = State.Box;
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y + 1] = State.PersonTarget;
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] = State.Target;
                                        currentGame.personLoc.Y += 1;
                                        CurrentStepCounts++;
                                    }

                                    //箱子右边是目标
                                    else if (currentGame.personLoc.Y + 2 <= Game.Col - 1 && currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y + 2] == State.Target)
                                    {
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y + 2] = State.BoxTarget;
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y + 1] = State.PersonTarget;
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] = State.Target;
                                        currentGame.personLoc.Y += 1;
                                        CurrentStepCounts++;
                                    }
                                }

                                //人不在目标上推在目标上的箱子
                                else if (currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] == State.Person)
                                {
                                    //箱子右边是空地
                                    if (currentGame.personLoc.Y + 2 <= Game.Col - 1 && currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y + 2] == State.Empty)
                                    {
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y + 2] = State.Box;
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y + 1] = State.PersonTarget;
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] = State.Empty;
                                        currentGame.personLoc.Y += 1;
                                        CurrentStepCounts++;
                                    }
                                    //箱子右边是目标
                                    else if (currentGame.personLoc.Y + 2 <= Game.Col - 1 && currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y + 2] == State.Target)
                                    {
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y + 2] = State.BoxTarget;
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y + 1] = State.PersonTarget;
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] = State.Empty;
                                        currentGame.personLoc.Y += 1;
                                        CurrentStepCounts++;
                                    }
                                }
                            }
                            #endregion

                            #region 不在目标上的箱子

                            else if (currentGame.personLoc.Y + 1 <= Game.Col - 1 &&
                                currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y + 1] == State.Box)
                            {
                                //人在目标上推不在目标上的箱子
                                if (currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] == State.PersonTarget)
                                {
                                    //箱子右边是空地
                                    if (currentGame.personLoc.Y + 2 <= Game.Col - 1 && currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y + 2] == State.Empty)
                                    {
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y + 2] = State.Box;
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y + 1] = State.Person;
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] = State.Target;
                                        currentGame.personLoc.Y += 1;
                                        CurrentStepCounts++;
                                    }
                                    //箱子右边是目标
                                    else if (currentGame.personLoc.Y + 2 <= Game.Col - 1 && currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y + 2] == State.Target)
                                    {
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y + 2] = State.BoxTarget;
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y + 1] = State.Person;
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] = State.Target;
                                        currentGame.personLoc.Y += 1;
                                        CurrentStepCounts++;

                                    }
                                }
                                //人不在目标上推不在目标上的箱子
                                else if (currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] == State.Person)
                                {
                                    //箱子右边是空地
                                    if (currentGame.personLoc.Y + 2 <= Game.Col - 1 &&
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y + 2] == State.Empty)
                                    {
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y + 2] = State.Box;
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y + 1] = State.Person;
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] = State.Empty;
                                        currentGame.personLoc.Y += 1;
                                        CurrentStepCounts++;
                                    }
                                    //箱子右边是目标
                                    else if (currentGame.personLoc.Y + 2 <= Game.Col - 1 &&
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y + 2] == State.Target)
                                    {
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y + 2] = State.BoxTarget;
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y + 1] = State.Person;
                                        currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] = State.Empty;
                                        currentGame.personLoc.Y += 1;
                                        CurrentStepCounts++;
                                    }
                                }
                            }
                            #endregion
                        }


                        #endregion

                        #region 右边是目标
                        else if (currentGame.personLoc.Y + 1 <= Game.Col - 1 &&
                                  currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y + 1] == State.Target)
                        {
                            //人在目标上
                            if (currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] == State.PersonTarget)
                            {
                                currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y + 1] = State.PersonTarget;
                                currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] = State.Target;
                                currentGame.personLoc.Y += 1;
                            }
                            //人不在目标上
                            else if (currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] == State.Person)
                            {
                                currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y + 1] = State.PersonTarget;
                                currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] = State.Empty;
                                currentGame.personLoc.Y += 1;
                            }
                        }
                        #endregion

                        #region 右边是空地
                        else if (currentGame.personLoc.Y + 1 <= Game.Col - 1 &&
                                 currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y + 1] == State.Empty)
                        {
                            //人在目标上
                            if (currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] == State.PersonTarget)
                            {
                                currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y + 1] = State.Person;
                                currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] = State.Target;
                                currentGame.personLoc.Y += 1;
                            }
                            //人不在目标上
                            else if (currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] == State.Person)
                            {
                                currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y + 1] = State.Person;
                                currentGame.map[currentGame.personLoc.X, currentGame.personLoc.Y] = State.Empty;
                                currentGame.personLoc.Y += 1;
                            }
                        }
                        #endregion
                    }
                    break;
            }
            #endregion

            pictureBox_GameForm.Refresh();
            toolStripStatusLabel1.Text = "当前模式:" + CurrentMode + "         当前关卡:" + (CurrentLevel + 1) + "           步数:" + CurrentStepCounts;

            #region 通过关卡消息提示
            if (IsPassedCurrentLevels(CurrentLevel) && CurrentMode == "闯关模式")
            {
                if (MessageBox.Show("是否进行下一局？", "恭喜你，闯关成功!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Currentgamescore.levelStepCount[CurrentLevel] = CurrentStepCounts;
                    CurrentLevel++;
                    CurrentStepCounts = 0;
                    currentGame = (Game)gameList[CurrentLevel].Clone();
                    game = (Game)gameList[CurrentLevel].Clone();
                    pictureBox_GameForm.Refresh();
                }
                else
                {
                    GameScore Currentgamescore = new GameScore();        //与editMapForm的是一致的，都要新建一个对象，然后把它加进去
                    GetPlayerName getplayname = new GetPlayerName();
                    //this.Hide();
                    getplayname.ShowDialog();
                    Currentgamescore.PlayerMode = CurrentMode;                         //玩家进行的模式
                    Currentgamescore.PlayerName = getplayname.PlayerName;              //玩家的名字
                    Currentgamescore.levelStepCount[CurrentLevel] = CurrentStepCounts; //玩家通过的关卡的步数
                    gameScoreList.Add(Currentgamescore);
                    CanWrite = true;
                    this.Close();
                }
            }
            else if (IsPassedCurrentLevels(CurrentLevel) && CurrentMode == "选关模式")
            {
                if (MessageBox.Show("是否继续选关？", "恭喜你，通过本关!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    CurrentStepCounts = 0;
                    currentGame = (Game)gameList[CurrentLevel].Clone();
                    game = (Game)gameList[CurrentLevel].Clone();
                    pictureBox_GameForm.Refresh();
                }
                else
                {
                    GameScore Currentgamescore = new GameScore();
                    GetPlayerName getplayname = new GetPlayerName();
                    //this.Hide();
                    getplayname.ShowDialog();
                    Currentgamescore.PlayerMode = CurrentMode;                         //玩家进行的模式
                    Currentgamescore.PlayerName = getplayname.PlayerName;              //玩家的名字
                    Currentgamescore.levelStepCount[CurrentLevel] = CurrentStepCounts; //玩家通过的关卡的步数
                    gameScoreList.Add(Currentgamescore);
                    CanWrite = true;
                    this.Close();
                }
            }
            else if (IsPassedCurrentLevels(CurrentLevel) && CurrentMode == "我的地图模式")
            {
                if (MessageBox.Show("是否继续下一关？", "恭喜你，通过本关!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (CurrentLevel + 1 < gameList.Count)
                    {
                        CurrentLevel++;
                        CurrentStepCounts = 0;
                        currentGame = (Game)gameList[CurrentLevel].Clone();
                        game = (Game)gameList[CurrentLevel].Clone();
                        pictureBox_GameForm.Refresh();
                    }
                    else MessageBox.Show("已是最后一关了!");
                }
            }
            toolStripStatusLabel1.Text = "当前模式:" + CurrentMode + "         当前关卡:" + (CurrentLevel + 1) + "           步数:" + CurrentStepCounts;
            #endregion
        }

        private void toolStripBtn_Replay_Click(object sender, EventArgs e)
        {
            currentGame = (Game)game1.Clone();
            pictureBox_GameForm.Refresh();
            CurrentStepCounts = 0;
            toolStripStatusLabel1.Text = "当前模式:" + CurrentMode + "         当前关卡:" + (CurrentLevel + 1) + "           步数:" + CurrentStepCounts; 
            BackStepMap.Clear();
            BackStepMove.Clear();
        }

        private void toolStripBtn_Home_Click(object sender, EventArgs e)
        {
            if (CurrentMode == "我的地图模式")
            {
                WelcomeForm welcomeForm = new WelcomeForm();
                this.Hide();
                welcomeForm.ShowDialog();
            }
            else
            {
                this.Hide();
            }
        }
        private void toolStripComboBoxLevels_SelectedIndexChanged(object sender, EventArgs e)
        {
            toolStripComboBox_Levels.Enabled = false;
            currentGame = (Game)gameList[toolStripComboBox_Levels.SelectedIndex].Clone();
            game = (Game)gameList[toolStripComboBox_Levels.SelectedIndex].Clone();
            game1 = (Game)gameList[toolStripComboBox_Levels.SelectedIndex].Clone();
            CurrentLevel = toolStripComboBox_Levels.SelectedIndex;
            CurrentStepCounts = 0;
            pictureBox_GameForm.Refresh();
            toolStripComboBox_Levels.Enabled = true;
            toolStripStatusLabel1.Text = "当前模式:" + CurrentMode + "         当前关卡:" + (CurrentLevel + 1) + "           步数:" + CurrentStepCounts;
        }

        private void GameForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (CanWrite)
            {
                WriteGameScoreList();
            }
        }

        private void toolStripBtn_BackStep_Click(object sender, EventArgs e)
        {
            if (BackStepMap.Count - 1 >= 0)
            {
                currentGame = (Game)BackStepMap[BackStepMap.Count - 1].Clone();
                game = (Game)BackStepMap[BackStepMap.Count - 1].Clone();
                BackStepMap.Remove(BackStepMap[BackStepMap.Count-1]);
            }
            if (BackStepMove.Count - 1 >= 0)
            {
                CurrentStepCounts = BackStepMove[BackStepMove.Count - 1];
                BackStepMove.Remove(BackStepMove[BackStepMove.Count - 1]);
            } 
            toolStripStatusLabel1.Text = "当前模式:" + CurrentMode + "         当前关卡:" + (CurrentLevel + 1) + "           步数:" + CurrentStepCounts;
            pictureBox_GameForm.Refresh();
        }

    }
}
