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
    public partial class RankListForm : Form
    {
        public RankListForm()
        {
            InitializeComponent();
        }
        private string fileName2 = "GameScoreData.txt";     //读写排行榜数据文件名
        private List<GameScore> gameScoreList = new List<GameScore>();

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

        private void RankListForm_Load(object sender, EventArgs e)
        {
            ReadGameScoreList();
        }
        private void listBoxLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            string s;
            listBoxM_N_S.Items.Clear();
            for (int i = 0; i < gameScoreList.Count; i++)
            {
                if (gameScoreList[i].levelStepCount[listBoxLevel.SelectedIndex] > 0)
                {
                    s = gameScoreList[i].PlayerMode + "\t " + gameScoreList[i].PlayerName + "\t      " + gameScoreList[i].levelStepCount[listBoxLevel.SelectedIndex];
                    listBoxM_N_S.Items.Add(s);
                }
            } 
        }
    }
}
