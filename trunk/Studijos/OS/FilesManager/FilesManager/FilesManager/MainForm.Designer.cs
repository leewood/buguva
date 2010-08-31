namespace FilesManager
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.leftSide = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.rightSide = new System.Windows.Forms.ListView();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.leftSideDrive = new System.Windows.Forms.ComboBox();
            this.rightSideDrive = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // leftSide
            // 
            this.leftSide.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            this.leftSide.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.leftSide.Location = new System.Drawing.Point(12, 33);
            this.leftSide.Name = "leftSide";
            this.leftSide.Size = new System.Drawing.Size(253, 228);
            this.leftSide.SmallImageList = this.imageList1;
            this.leftSide.TabIndex = 1;
            this.leftSide.UseCompatibleStateImageBehavior = false;
            this.leftSide.View = System.Windows.Forms.View.Details;
            this.leftSide.ItemActivate += new System.EventHandler(this.listView1_ItemActivate);
            this.leftSide.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.leftSide.KeyDown += new System.Windows.Forms.KeyEventHandler(this.leftSide_KeyDown);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "File Name";
            this.columnHeader1.Width = 170;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Type";
            this.columnHeader2.Width = 63;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "folder.bmp");
            this.imageList1.Images.SetKeyName(1, "none.bmp");
            // 
            // rightSide
            // 
            this.rightSide.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            this.rightSide.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4});
            this.rightSide.Location = new System.Drawing.Point(288, 33);
            this.rightSide.Name = "rightSide";
            this.rightSide.Size = new System.Drawing.Size(257, 228);
            this.rightSide.SmallImageList = this.imageList1;
            this.rightSide.TabIndex = 2;
            this.rightSide.UseCompatibleStateImageBehavior = false;
            this.rightSide.View = System.Windows.Forms.View.Details;
            this.rightSide.ItemActivate += new System.EventHandler(this.listView1_ItemActivate);
            this.rightSide.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.rightSide_ItemSelectionChanged);
            this.rightSide.KeyDown += new System.Windows.Forms.KeyEventHandler(this.leftSide_KeyDown);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "File Name";
            this.columnHeader3.Width = 170;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Type";
            this.columnHeader4.Width = 63;
            // 
            // leftSideDrive
            // 
            this.leftSideDrive.FormattingEnabled = true;
            this.leftSideDrive.Location = new System.Drawing.Point(12, 7);
            this.leftSideDrive.Name = "leftSideDrive";
            this.leftSideDrive.Size = new System.Drawing.Size(253, 21);
            this.leftSideDrive.TabIndex = 3;
            this.leftSideDrive.SelectedIndexChanged += new System.EventHandler(this.leftSideDrive_SelectedIndexChanged);
            // 
            // rightSideDrive
            // 
            this.rightSideDrive.FormattingEnabled = true;
            this.rightSideDrive.Location = new System.Drawing.Point(288, 6);
            this.rightSideDrive.Name = "rightSideDrive";
            this.rightSideDrive.Size = new System.Drawing.Size(257, 21);
            this.rightSideDrive.TabIndex = 4;
            this.rightSideDrive.SelectedIndexChanged += new System.EventHandler(this.leftSideDrive_SelectedIndexChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 321);
            this.Controls.Add(this.rightSideDrive);
            this.Controls.Add(this.leftSideDrive);
            this.Controls.Add(this.rightSide);
            this.Controls.Add(this.leftSide);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Image Files Manager";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView leftSide;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ListView rightSide;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ComboBox leftSideDrive;
        private System.Windows.Forms.ComboBox rightSideDrive;
    }
}

