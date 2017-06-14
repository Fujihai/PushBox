namespace 推箱子
{
    partial class GameForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameForm));
            this.toolStripGameForm = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel_Level = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBox_Levels = new System.Windows.Forms.ToolStripComboBox();
            this.statusStripGameForm = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripBtn_Replay = new System.Windows.Forms.ToolStripButton();
            this.toolStripBtn_BackStep = new System.Windows.Forms.ToolStripButton();
            this.toolStripBtn_Home = new System.Windows.Forms.ToolStripButton();
            this.pictureBox_GameForm = new System.Windows.Forms.PictureBox();
            this.toolStripGameForm.SuspendLayout();
            this.statusStripGameForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_GameForm)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripGameForm
            // 
            this.toolStripGameForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripBtn_Replay,
            this.toolStripBtn_BackStep,
            this.toolStripBtn_Home,
            this.toolStripSeparator1,
            this.toolStripLabel_Level,
            this.toolStripComboBox_Levels});
            this.toolStripGameForm.Location = new System.Drawing.Point(0, 0);
            this.toolStripGameForm.Name = "toolStripGameForm";
            this.toolStripGameForm.Size = new System.Drawing.Size(535, 25);
            this.toolStripGameForm.TabIndex = 3;
            this.toolStripGameForm.Text = "toolStrip1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel_Level
            // 
            this.toolStripLabel_Level.Name = "toolStripLabel_Level";
            this.toolStripLabel_Level.Size = new System.Drawing.Size(35, 22);
            this.toolStripLabel_Level.Text = "选关:";
            // 
            // toolStripComboBox_Levels
            // 
            this.toolStripComboBox_Levels.Items.AddRange(new object[] {
            "关卡1",
            "关卡2",
            "关卡3",
            "关卡4",
            "关卡5",
            "关卡6",
            "关卡7",
            "关卡8",
            "关卡9",
            "关卡10",
            "关卡11",
            "关卡12",
            "关卡13",
            "关卡14",
            "关卡15",
            "关卡16",
            "关卡17",
            "关卡18",
            "关卡19",
            "关卡20",
            "关卡21",
            "关卡22",
            "关卡23",
            "关卡24",
            "关卡25",
            "关卡26",
            "关卡27",
            "关卡28"});
            this.toolStripComboBox_Levels.Name = "toolStripComboBox_Levels";
            this.toolStripComboBox_Levels.Size = new System.Drawing.Size(121, 25);
            this.toolStripComboBox_Levels.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxLevels_SelectedIndexChanged);
            // 
            // statusStripGameForm
            // 
            this.statusStripGameForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStripGameForm.Location = new System.Drawing.Point(0, 410);
            this.statusStripGameForm.Name = "statusStripGameForm";
            this.statusStripGameForm.Size = new System.Drawing.Size(535, 22);
            this.statusStripGameForm.TabIndex = 4;
            this.statusStripGameForm.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            this.toolStripStatusLabel1.ToolTipText = "123";
            // 
            // toolStripBtn_Replay
            // 
            this.toolStripBtn_Replay.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtn_Replay.Image")));
            this.toolStripBtn_Replay.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtn_Replay.Name = "toolStripBtn_Replay";
            this.toolStripBtn_Replay.Size = new System.Drawing.Size(52, 22);
            this.toolStripBtn_Replay.Text = "重玩";
            this.toolStripBtn_Replay.Click += new System.EventHandler(this.toolStripBtn_Replay_Click);
            // 
            // toolStripBtn_BackStep
            // 
            this.toolStripBtn_BackStep.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtn_BackStep.Image")));
            this.toolStripBtn_BackStep.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtn_BackStep.Name = "toolStripBtn_BackStep";
            this.toolStripBtn_BackStep.Size = new System.Drawing.Size(52, 22);
            this.toolStripBtn_BackStep.Text = "后退";
            this.toolStripBtn_BackStep.Click += new System.EventHandler(this.toolStripBtn_BackStep_Click);
            // 
            // toolStripBtn_Home
            // 
            this.toolStripBtn_Home.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtn_Home.Image")));
            this.toolStripBtn_Home.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtn_Home.Name = "toolStripBtn_Home";
            this.toolStripBtn_Home.Size = new System.Drawing.Size(64, 22);
            this.toolStripBtn_Home.Text = "主菜单";
            this.toolStripBtn_Home.Click += new System.EventHandler(this.toolStripBtn_Home_Click);
            // 
            // pictureBox_GameForm
            // 
            this.pictureBox_GameForm.Location = new System.Drawing.Point(0, 28);
            this.pictureBox_GameForm.Name = "pictureBox_GameForm";
            this.pictureBox_GameForm.Size = new System.Drawing.Size(100, 50);
            this.pictureBox_GameForm.TabIndex = 0;
            this.pictureBox_GameForm.TabStop = false;
            this.pictureBox_GameForm.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_Paint);
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 432);
            this.Controls.Add(this.statusStripGameForm);
            this.Controls.Add(this.toolStripGameForm);
            this.Controls.Add(this.pictureBox_GameForm);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GameForm";
            this.Text = "推箱子";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GameForm_FormClosed);
            this.Load += new System.EventHandler(this.GameForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GameForm_KeyDown);
            this.toolStripGameForm.ResumeLayout(false);
            this.toolStripGameForm.PerformLayout();
            this.statusStripGameForm.ResumeLayout(false);
            this.statusStripGameForm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_GameForm)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_GameForm;
        private System.Windows.Forms.ToolStrip toolStripGameForm;
        private System.Windows.Forms.ToolStripButton toolStripBtn_Replay;
        private System.Windows.Forms.ToolStripButton toolStripBtn_Home;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.StatusStrip statusStripGameForm;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox_Levels;
        private System.Windows.Forms.ToolStripLabel toolStripLabel_Level;
        private System.Windows.Forms.ToolStripButton toolStripBtn_BackStep;
    }
}

