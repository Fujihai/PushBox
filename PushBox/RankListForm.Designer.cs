namespace 推箱子
{
    partial class RankListForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RankListForm));
            this.labelLevel = new System.Windows.Forms.Label();
            this.listBoxM_N_S = new System.Windows.Forms.ListBox();
            this.labelMode = new System.Windows.Forms.Label();
            this.listBoxLevel = new System.Windows.Forms.ListBox();
            this.labelPlayerName = new System.Windows.Forms.Label();
            this.labelSteps = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelLevel
            // 
            this.labelLevel.AutoSize = true;
            this.labelLevel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(145)))), ((int)(((byte)(106)))));
            this.labelLevel.Font = new System.Drawing.Font("宋体", 15F);
            this.labelLevel.Location = new System.Drawing.Point(63, 36);
            this.labelLevel.Name = "labelLevel";
            this.labelLevel.Size = new System.Drawing.Size(49, 20);
            this.labelLevel.TabIndex = 3;
            this.labelLevel.Text = "关卡";
            // 
            // listBoxM_N_S
            // 
            this.listBoxM_N_S.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(145)))), ((int)(((byte)(106)))));
            this.listBoxM_N_S.Font = new System.Drawing.Font("宋体", 15F);
            this.listBoxM_N_S.FormattingEnabled = true;
            this.listBoxM_N_S.ItemHeight = 20;
            this.listBoxM_N_S.Location = new System.Drawing.Point(179, 68);
            this.listBoxM_N_S.Name = "listBoxM_N_S";
            this.listBoxM_N_S.Size = new System.Drawing.Size(356, 344);
            this.listBoxM_N_S.TabIndex = 4;
            // 
            // labelMode
            // 
            this.labelMode.AutoSize = true;
            this.labelMode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(145)))), ((int)(((byte)(106)))));
            this.labelMode.Font = new System.Drawing.Font("宋体", 15F);
            this.labelMode.Location = new System.Drawing.Point(211, 36);
            this.labelMode.Name = "labelMode";
            this.labelMode.Size = new System.Drawing.Size(49, 20);
            this.labelMode.TabIndex = 5;
            this.labelMode.Text = "模式";
            // 
            // listBoxLevel
            // 
            this.listBoxLevel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(145)))), ((int)(((byte)(106)))));
            this.listBoxLevel.Font = new System.Drawing.Font("宋体", 15F);
            this.listBoxLevel.FormattingEnabled = true;
            this.listBoxLevel.ItemHeight = 20;
            this.listBoxLevel.Items.AddRange(new object[] {
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
            "关卡23",
            "关卡25",
            "关卡26",
            "关卡27",
            "关卡28"});
            this.listBoxLevel.Location = new System.Drawing.Point(23, 68);
            this.listBoxLevel.Name = "listBoxLevel";
            this.listBoxLevel.Size = new System.Drawing.Size(132, 344);
            this.listBoxLevel.TabIndex = 6;
            this.listBoxLevel.SelectedIndexChanged += new System.EventHandler(this.listBoxLevel_SelectedIndexChanged);
            // 
            // labelPlayerName
            // 
            this.labelPlayerName.AutoSize = true;
            this.labelPlayerName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(145)))), ((int)(((byte)(106)))));
            this.labelPlayerName.Font = new System.Drawing.Font("宋体", 15F);
            this.labelPlayerName.Location = new System.Drawing.Point(333, 36);
            this.labelPlayerName.Name = "labelPlayerName";
            this.labelPlayerName.Size = new System.Drawing.Size(89, 20);
            this.labelPlayerName.TabIndex = 7;
            this.labelPlayerName.Text = "玩家姓名";
            // 
            // labelSteps
            // 
            this.labelSteps.AutoSize = true;
            this.labelSteps.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(145)))), ((int)(((byte)(106)))));
            this.labelSteps.Font = new System.Drawing.Font("宋体", 15F);
            this.labelSteps.Location = new System.Drawing.Point(465, 36);
            this.labelSteps.Name = "labelSteps";
            this.labelSteps.Size = new System.Drawing.Size(49, 20);
            this.labelSteps.TabIndex = 8;
            this.labelSteps.Text = "步数";
            // 
            // RankListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.BackgroundImage = global::推箱子.Properties.Resources.rankList;
            this.ClientSize = new System.Drawing.Size(560, 441);
            this.Controls.Add(this.labelSteps);
            this.Controls.Add(this.labelPlayerName);
            this.Controls.Add(this.listBoxLevel);
            this.Controls.Add(this.labelMode);
            this.Controls.Add(this.listBoxM_N_S);
            this.Controls.Add(this.labelLevel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RankListForm";
            this.Text = "排行榜";
            this.Load += new System.EventHandler(this.RankListForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelLevel;
        private System.Windows.Forms.ListBox listBoxM_N_S;
        private System.Windows.Forms.Label labelMode;
        private System.Windows.Forms.ListBox listBoxLevel;
        private System.Windows.Forms.Label labelPlayerName;
        private System.Windows.Forms.Label labelSteps;

    }
}