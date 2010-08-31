namespace Operacine_sistema
{
    partial class ResourcesList
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
            this.components = new System.ComponentModel.Container();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newResourceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteResourceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newElementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteElementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Location = new System.Drawing.Point(173, 2);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(188, 319);
            this.propertyGrid1.TabIndex = 0;
            // 
            // treeView1
            // 
            this.treeView1.ContextMenuStrip = this.contextMenuStrip1;
            this.treeView1.Location = new System.Drawing.Point(1, 2);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(166, 319);
            this.treeView1.TabIndex = 1;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newResourceToolStripMenuItem,
            this.deleteResourceToolStripMenuItem,
            this.newElementToolStripMenuItem,
            this.deleteElementToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(162, 92);
            this.contextMenuStrip1.VisibleChanged += new System.EventHandler(this.contextMenuStrip1_VisibleChanged);
            // 
            // newResourceToolStripMenuItem
            // 
            this.newResourceToolStripMenuItem.Name = "newResourceToolStripMenuItem";
            this.newResourceToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.newResourceToolStripMenuItem.Text = "New resource";
            this.newResourceToolStripMenuItem.Click += new System.EventHandler(this.newResourceToolStripMenuItem_Click);
            // 
            // deleteResourceToolStripMenuItem
            // 
            this.deleteResourceToolStripMenuItem.Name = "deleteResourceToolStripMenuItem";
            this.deleteResourceToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.deleteResourceToolStripMenuItem.Text = "Delete resource";
            this.deleteResourceToolStripMenuItem.Click += new System.EventHandler(this.deleteResourceToolStripMenuItem_Click);
            // 
            // newElementToolStripMenuItem
            // 
            this.newElementToolStripMenuItem.Name = "newElementToolStripMenuItem";
            this.newElementToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.newElementToolStripMenuItem.Text = "New element";
            this.newElementToolStripMenuItem.Click += new System.EventHandler(this.newElementToolStripMenuItem_Click);
            // 
            // deleteElementToolStripMenuItem
            // 
            this.deleteElementToolStripMenuItem.Name = "deleteElementToolStripMenuItem";
            this.deleteElementToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.deleteElementToolStripMenuItem.Text = "Delete element";
            this.deleteElementToolStripMenuItem.VisibleChanged += new System.EventHandler(this.deleteElementToolStripMenuItem_VisibleChanged);
            this.deleteElementToolStripMenuItem.Click += new System.EventHandler(this.deleteElementToolStripMenuItem_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ResourcesList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 322);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.propertyGrid1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "ResourcesList";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ResourcesList";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem newElementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteElementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newResourceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteResourceToolStripMenuItem;
    }
}