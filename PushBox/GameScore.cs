using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 推箱子
{
    [Serializable]        //记住要加，否则序列化会有错误
    class GameScore
    {
        private string playerName;

        public string PlayerName
        {
            get { return playerName; }
            set { playerName = value; }
        }
        private string playerMode;

        public string PlayerMode
        {
            get { return playerMode; }
            set { playerMode = value; }
        }
        public int [] levelStepCount = new int[28];

    }
}
