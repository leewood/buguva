namespace WindowsFormsApplication1
{
    partial class Form2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pictureBox11 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.richTextBox3 = new System.Windows.Forms.RichTextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox11)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(648, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ToolStripMenuItem
            // 
            this.ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGameToolStripMenuItem});
            this.ToolStripMenuItem.Name = "ToolStripMenuItem";
            this.ToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.ToolStripMenuItem.Text = "Game";
            // 
            // newGameToolStripMenuItem
            // 
            this.newGameToolStripMenuItem.Name = "newGameToolStripMenuItem";
            this.newGameToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.newGameToolStripMenuItem.Text = "Start";
            this.newGameToolStripMenuItem.Click += new System.EventHandler(this.newGameToolStripMenuItem_Click);
            // 
            // panel3
            // 
            this.panel3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel3.BackgroundImage")));
            this.panel3.Location = new System.Drawing.Point(31, 88);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(278, 300);
            this.panel3.TabIndex = 5;
            // 
            // pictureBox11
            // 
            this.pictureBox11.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox11.Image")));
            this.pictureBox11.Location = new System.Drawing.Point(31, 394);
            this.pictureBox11.Name = "pictureBox11";
            this.pictureBox11.Size = new System.Drawing.Size(10, 10);
            this.pictureBox11.TabIndex = 16;
            this.pictureBox11.TabStop = false;
            this.pictureBox11.Visible = false;
            this.pictureBox11.Click += new System.EventHandler(this.pictureBox11_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Location = new System.Drawing.Point(342, 88);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(277, 328);
            this.panel1.TabIndex = 17;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(509, 442);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 23);
            this.button1.TabIndex = 18;
            this.button1.Text = "Reverse";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(509, 471);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(110, 23);
            this.button2.TabIndex = 19;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(31, 444);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.ReadOnly = true;
            this.richTextBox2.Size = new System.Drawing.Size(472, 67);
            this.richTextBox2.TabIndex = 20;
            this.richTextBox2.Text = "";
            // 
            // panel4
            // 
            this.panel4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel4.BackgroundImage")));
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.richTextBox3);
            this.panel4.Controls.Add(this.richTextBox1);
            this.panel4.Controls.Add(this.button4);
            this.panel4.Controls.Add(this.button3);
            this.panel4.Location = new System.Drawing.Point(0, 27);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(648, 551);
            this.panel4.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(273, 495);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Server Port ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(244, 466);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Server IP Address";
            // 
            // richTextBox3
            // 
            this.richTextBox3.Location = new System.Drawing.Point(342, 490);
            this.richTextBox3.Name = "richTextBox3";
            this.richTextBox3.Size = new System.Drawing.Size(123, 23);
            this.richTextBox3.TabIndex = 3;
            this.richTextBox3.Text = "";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(342, 461);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(123, 23);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(492, 490);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(127, 23);
            this.button4.TabIndex = 1;
            this.button4.Text = "Create Game";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(492, 461);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(127, 23);
            this.button3.TabIndex = 0;
            this.button3.Text = "Join game";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // panel5
            // 
            this.panel5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel5.BackgroundImage")));
            this.panel5.Location = new System.Drawing.Point(340, 88);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(278, 300);
            this.panel5.TabIndex = 6;
            this.panel5.Visible = false;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(648, 575);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.pictureBox11);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.menuStrip1);
            this.MaximumSize = new System.Drawing.Size(656, 605);
            this.MinimumSize = new System.Drawing.Size(656, 605);
            this.Name = "Battle Ships";
            this.Text = "Battle Ships";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox11)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

            PanelUtils.prepareObjectsForBattleField(menuStrip1, panel1, button2, button1, panel5);
            PanelUtils.setGreetingPagePanel(panel4);
            PanelUtils.setMainPanel(panel1);
            PanelUtils.setPlayerMapPanel(panel3);
            PanelUtils.addBattleShips(panel1);
            PanelUtils.AddButtonsToPanel(this.panel3, 10, 10, 25, 23, false);
            PanelUtils.message = richTextBox2;
        }

        #endregion

        public void RedrawToBattleField()
        {
            PanelUtils.RedrawToBattleField(panel5, panel1);
        }

        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newGameToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox pictureBox11;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox richTextBox3;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Panel panel5;
    }
}