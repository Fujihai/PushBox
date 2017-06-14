namespace 推箱子
{
    partial class WelcomeForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WelcomeForm));
            this.btn_StartGame = new System.Windows.Forms.Button();
            this.btn_RankList = new System.Windows.Forms.Button();
            this.btn_ExitGame = new System.Windows.Forms.Button();
            this.comboBoxMode = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btn_StartGame
            // 
            this.btn_StartGame.BackgroundImage = global::推箱子.Properties.Resources.Empty;
            this.btn_StartGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_StartGame.Font = new System.Drawing.Font("宋体", 15F);
            this.btn_StartGame.Location = new System.Drawing.Point(341, 246);
            this.btn_StartGame.Name = "btn_StartGame";
            this.btn_StartGame.Size = new System.Drawing.Size(129, 36);
            this.btn_StartGame.TabIndex = 0;
            this.btn_StartGame.Text = "开始游戏";
            this.btn_StartGame.UseVisualStyleBackColor = true;
            this.btn_StartGame.Click += new System.EventHandler(this.btn_StartGame_Click);
            // 
            // btn_RankList
            // 
            this.btn_RankList.BackgroundImage = global::推箱子.Properties.Resources.Empty;
            this.btn_RankList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_RankList.Font = new System.Drawing.Font("宋体", 15F);
            this.btn_RankList.Location = new System.Drawing.Point(144, 323);
            this.btn_RankList.Name = "btn_RankList";
            this.btn_RankList.Size = new System.Drawing.Size(129, 36);
            this.btn_RankList.TabIndex = 1;
            this.btn_RankList.Text = "排行榜";
            this.btn_RankList.UseVisualStyleBackColor = true;
            this.btn_RankList.Click += new System.EventHandler(this.btn_RankList_Click);
            // 
            // btn_ExitGame
            // 
            this.btn_ExitGame.BackgroundImage = global::推箱子.Properties.Resources.Empty;
            this.btn_ExitGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ExitGame.Font = new System.Drawing.Font("宋体", 15F);
            this.btn_ExitGame.Location = new System.Drawing.Point(341, 323);
            this.btn_ExitGame.Name = "btn_ExitGame";
            this.btn_ExitGame.Size = new System.Drawing.Size(129, 36);
            this.btn_ExitGame.TabIndex = 2;
            this.btn_ExitGame.Text = "退出游戏";
            this.btn_ExitGame.UseVisualStyleBackColor = true;
            this.btn_ExitGame.Click += new System.EventHandler(this.btn_ExitGame_Click);
            // 
            // comboBoxMode
            // 
            this.comboBoxMode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(145)))), ((int)(((byte)(106)))));
            this.comboBoxMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxMode.Font = new System.Drawing.Font("宋体", 15F);
            this.comboBoxMode.FormattingEnabled = true;
            this.comboBoxMode.ItemHeight = 20;
            this.comboBoxMode.Items.AddRange(new object[] {
            "闯关模式",
            "选关模式",
            "我的地图模式"});
            this.comboBoxMode.Location = new System.Drawing.Point(144, 251);
            this.comboBoxMode.Name = "comboBoxMode";
            this.comboBoxMode.Size = new System.Drawing.Size(129, 28);
            this.comboBoxMode.TabIndex = 4;
            this.comboBoxMode.SelectedIndexChanged += new System.EventHandler(this.comboBoxMode_SelectedIndexChanged);
            // 
            // WelcomeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(560, 441);
            this.Controls.Add(this.comboBoxMode);
            this.Controls.Add(this.btn_ExitGame);
            this.Controls.Add(this.btn_RankList);
            this.Controls.Add(this.btn_StartGame);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "WelcomeForm";
            this.Text = "推箱子";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.WelcomeForm_FormClosed);
            this.Load += new System.EventHandler(this.WelcomeForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_StartGame;
        private System.Windows.Forms.Button btn_RankList;
        private System.Windows.Forms.Button btn_ExitGame;
        private System.Windows.Forms.ComboBox comboBoxMode;
    }
}