using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace 推箱子
{
    public partial class EditMapForm : Form
    {
        private const string fileName = "Player's Data.txt"; 
        private State Selected_State;                       
        private State[,] editMap = new State[Game.Row, Game.Col];
        private int editMap_Targetnum = 0;                  //目标数
        private int editMap_Boxnum = 0;                     //箱子数
        private int PersonNum = 0;                          //人个数
        public System.Drawing.Point personLoc;              //人目标
        public List<Game> editgameList = new List<Game>();  //存储用户编辑的有效地图的线性表
        private bool canWriteMap = false;

        public bool CanWriteMap
        {
            get { return canWriteMap; }
            set { canWriteMap = value; }
        }
        Game currentGame = new Game();

        public EditMapForm()
        {
            InitializeComponent();
        }

        private bool IsValidMap()                  //判断地图是否有效
        {  
            PersonNum = editMap_Boxnum = editMap_Targetnum = 0;
            for(int i=0;i<Game.Row;i++)
            {
                for(int j=0;j<Game.Col;j++)
                {
                    if (editMap[i, j] == State.Person)
                    {
                        PersonNum++;
                    }
                    else if(editMap[i,j]==State.Box)
                    {
                        editMap_Boxnum++;
                    }
                    else if(editMap[i,j]==State.Target)
                    {
                        editMap_Targetnum++;
                    }
                }
            }
            if (PersonNum != 1 ||(editMap_Boxnum==0&&editMap_Targetnum==0)||(editMap_Boxnum!=editMap_Targetnum))
            {
                return false;
            }
            else return true;
        }

        private void InitEditMap()                //初始化地图
        {
            Selected_State = State.None;
            for(int i=0;i<Game.Row;i++)
            {
                for(int j=0;j<Game.Col;j++)
                {
                    editMap[i, j] = State.None;
                }
            }
            pictureBox_Map.Size = new Size(GameImages.sideLength * Game.Col, GameImages.sideLength * Game.Row);
            pictureBox_Map.Refresh();
        }

        private void SaveEditMap()               //保存当前地图
        {
                currentGame = new Game();                  
                for (int i = 0; i < Game.Row; i++)
                {
                    for (int j = 0; j < Game.Col; j++)
                    {
                        currentGame.map[i, j] = editMap[i, j];
                        if(editMap[i,j]==State.Person)
                        {
                            personLoc = new Point(i, j);
                        }
                    }
                }
                currentGame.TargetNum = editMap_Targetnum;
                currentGame.personLoc = personLoc;
                editgameList.Add(currentGame);
                MessageBox.Show("保存成功！" + "关卡:" + editgameList.Count);
                canWriteMap = true;
               // MessageBox.Show("保存成功！" + "线性表容量：" + editgameList.Count + "  目标数目：" +editMap_Targetnum);
        }
        private void EditMapForm_Load(object sender, EventArgs e)
        {
            SetStyle(ControlStyles.ResizeRedraw
                | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.OptimizedDoubleBuffer, true);
            UpdateStyles();

            this.Size = new Size
               (GameImages.sideLength * Game.Col,
               GameImages.sideLength * Game.Row + (this.Size.Height - this.ClientRectangle.Size.Height + 29));
            this.MaximumSize = new Size
                (GameImages.sideLength * Game.Col,
                GameImages.sideLength * Game.Row + (this.Size.Height - this.ClientRectangle.Size.Height + 29));
            this.MaximizeBox = false;

            InitEditMap();
            toolStripBtn_Save.Enabled = false;
            toolStripBtn_Person.Enabled = false;
            toolStripBtn_Target.Enabled = false;
            toolStripBtn_Wall.Enabled = false;
            toolStripBtn_Empty.Enabled = false;
            toolStripBtn_Box.Enabled = false;
            toolStripBtn_Clear.Enabled = false;
            
        } 
        private void EditMapForm_FormClosed(object sender, FormClosedEventArgs e)  //窗体关闭后保存当前editgameList数据
        {
            if (canWriteMap)
            {
                FileStream stream = null;
                stream = new FileStream(fileName, FileMode.Create);
                BinaryFormatter bFormatter = new BinaryFormatter();
                bFormatter.Serialize(stream, editgameList);
                stream.Close();
            }
            else MessageBox.Show("地图保存失败!");
        }

        private void toolStripBtn_Wall_Click(object sender, EventArgs e)   //墙
        {
            Selected_State = State.Wall;
        }

        private void toolStripBtn_Box_Click(object sender, EventArgs e)    //箱子
        {
            Selected_State = State.Box;
        }

        private void toolStripBtn_Target_Click(object sender, EventArgs e)  //目标
        {
            Selected_State = State.Target;
        }

        private void toolStripBtn_Person_Click(object sender, EventArgs e)  //人
        {
            Selected_State = State.Person;
        }

        private void toolStripBtn_Empty_Click(object sender, EventArgs e)   //空地
        {
            Selected_State = State.Empty;
        }

        private void toolStripBtn_Help_Click(object sender, EventArgs e)    //帮助
        {
            MessageBox.Show("操作说明：\n每点击一次保存即保存当前地图,点击新建即新建一幅新地图\n点击清空则清空当前地图\n\n玩家编辑的地图应该满足以下条件：\n1.一个人物\n2.目标数与箱子数相等(均不为零)\n3.墙壁是封闭的，且墙壁内均是空地\n4.箱子与目标均在墙壁内\n如果当前编辑地图不满足以上条件,则该地图无法保存或正常使用!");
        }

        private void toolStripBtn_New_Click(object sender, EventArgs e)     //新建
        {
            InitEditMap();
            toolStripBtn_New.Enabled = false;
            toolStripBtn_Save.Enabled = true;
            toolStripBtn_Person.Enabled = true;
            toolStripBtn_Target.Enabled = true;
            toolStripBtn_Wall.Enabled = true;
            toolStripBtn_Empty.Enabled = true;
            toolStripBtn_Box.Enabled = true;
            toolStripBtn_Clear.Enabled = true;
        }  

        private void toolStripBtn_Save_Click(object sender, EventArgs e)    //保存的是一局的地图，即当前的地图
        {
            if (IsValidMap())
            {
                SaveEditMap();
                editMap_Targetnum = 0;
                editMap_Boxnum = 0;
                PersonNum = 0;
                toolStripBtn_New.Enabled = true;
                toolStripBtn_Save.Enabled = false;
                toolStripBtn_Box.Enabled = false;
                toolStripBtn_Clear.Enabled = false;
                toolStripBtn_Empty.Enabled = false;
                toolStripBtn_Wall.Enabled = false;
                toolStripBtn_Person.Enabled = false;
                toolStripBtn_Target.Enabled = false;
                InitEditMap();
            }
            else
            {
                string ErrorString="",ErrorString1 = "", ErrorString2 = "", ErrorString3 = "";
                ErrorString = "地图无效,请重新设置!";
                if (PersonNum != 1)
                {
                    ErrorString1 = "人物数目应该为1个! ";
                }
                else if (editMap_Targetnum != editMap_Boxnum)
                {
                    ErrorString2 = "箱子数目与目标数目不一致! ";
                }
                else if(editMap_Boxnum == 0 && editMap_Targetnum == 0)
                {
                    ErrorString3 = "箱子与目标数目为零! ";
                }
                MessageBox.Show(ErrorString1 + ErrorString2 +ErrorString3+ ErrorString);
            }
        }

        private void pictureBox_Map_MouseDown(object sender, MouseEventArgs e)  //鼠标点击，绘制选中的toolstripBtn
        {
            int x, y;
            x = e.X / GameImages.sideLength;
            y = e.Y / GameImages.sideLength;
            editMap[y, x] = Selected_State;
            pictureBox_Map.Refresh();
        }

        private void pictureBox_Map_Paint(object sender, PaintEventArgs e)
        {
            Bitmap gameImage = new Bitmap(GameImages.sideLength * Game.Col, GameImages.sideLength * Game.Row);
            Graphics g = Graphics.FromImage(gameImage);
            this.DrawEditMap(currentGame, g);
            g.Dispose();
            g = e.Graphics;
            g.DrawImage(gameImage, 0, 0);
        }

        private void DrawEditMap(Game currentgame,Graphics g)                //画地图,重绘，与GameForm的画图一致
        {
            Rectangle cellRect = new Rectangle(0, 0, GameImages.sideLength, GameImages.sideLength);
            for (int i = 0; i < Game.Row; i++)
            {
                for (int j = 0; j < Game.Col; j++)
                {
                    cellRect.X = GameImages.sideLength * j;
                    cellRect.Y = GameImages.sideLength * i;
                    g.DrawImage(GameImages.getImage(editMap[i, j]), cellRect);
                }
            }
        }

        private void toolStripBtn_Clear_Click(object sender, EventArgs e)
        {
            InitEditMap();
        }

        private void ReadGameList()      //从二进制序列化文件中读取初始化的地图数据
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
            editgameList = (List<Game>)bFormatter.Deserialize(stream);
            stream.Close();
        }
    }
}
