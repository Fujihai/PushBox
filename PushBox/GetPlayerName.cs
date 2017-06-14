using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 推箱子
{
    public partial class GetPlayerName : Form
    {
        public GetPlayerName()
        {
            InitializeComponent();
        }

        private string playerName = "";

        public string PlayerName
        {
            get { return playerName; }
            set { playerName = value; }
        }

        private void buttonYes_Click(object sender, EventArgs e)
        {
            if (textBoxPlayerName.Text != "")
            {
                playerName = textBoxPlayerName.Text;
                this.Close();
            }
            else MessageBox.Show("输入内容不能为空!请重新输入!");
        }

        private void buttonNo_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
    }
}
