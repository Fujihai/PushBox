using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 推箱子
{
    public partial class WelcomeForm : Form
    {
        public WelcomeForm()
        {
            InitializeComponent();
        }

        private int modeNum = 1;

        public int ModeNum
        {
            get { return modeNum; }
        }
        private void WelcomeForm_Load(object sender, EventArgs e)
        {
            comboBoxMode.SelectedIndex = 0;
            SetStyle(ControlStyles.ResizeRedraw
                | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.OptimizedDoubleBuffer, true);
            UpdateStyles();

            this.MaximizeBox = false;
        }

        private void comboBoxMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBoxMode.SelectedIndex==0)
            {
                modeNum = 0;
            }
            else if(comboBoxMode.SelectedIndex==1)
            {
                modeNum = 1;
            }
            else
            {
                modeNum = 2;
            }
        }

        private void btn_StartGame_Click(object sender, EventArgs e)
        {
            if(modeNum==0)
            {
                GameForm gameForm = new GameForm(ModeNum);
                this.Hide();
                gameForm.ShowDialog();
                this.Show();    
            }
            else if(modeNum==1)
            {
                GameForm gameForm = new GameForm(ModeNum);
                this.Hide();
                gameForm.ShowDialog(); 
                this.Show();
            }
            else if(modeNum==2)
            {
                EditMapForm editmapForm = new EditMapForm();
                this.Hide();
                editmapForm.ShowDialog();
                if (editmapForm.CanWriteMap == true)
                {
                    GameForm gameForm = new GameForm(ModeNum);
                    gameForm.ShowDialog();
                }
                else MessageBox.Show("对不起，你的地图设置无效，请返回主菜单重新进行游戏!");
                this.Show();
                
            }
            
            
        }

        private void btn_ExitGame_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_RankList_Click(object sender, EventArgs e)
        {
            RankListForm ranklistForm = new RankListForm();
            this.Hide();
            ranklistForm.ShowDialog();
            this.Show();
        }

        private void WelcomeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
