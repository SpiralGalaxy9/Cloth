namespace Cloth
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSideToolbar = new System.Windows.Forms.ToolStrip();
            this.tsBtnManufacturers = new System.Windows.Forms.ToolStripButton();
            this.tsBtnColors = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1.SuspendLayout();
            this.tsSideToolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "msMenu";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // tsSideToolbar
            // 
            this.tsSideToolbar.Dock = System.Windows.Forms.DockStyle.Left;
            this.tsSideToolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsBtnManufacturers,
            this.tsBtnColors});
            this.tsSideToolbar.Location = new System.Drawing.Point(0, 24);
            this.tsSideToolbar.Name = "tsSideToolbar";
            this.tsSideToolbar.Size = new System.Drawing.Size(149, 426);
            this.tsSideToolbar.TabIndex = 2;
            this.tsSideToolbar.Text = "tsTools";
            // 
            // tsBtnManufacturers
            // 
            this.tsBtnManufacturers.CheckOnClick = true;
            this.tsBtnManufacturers.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsBtnManufacturers.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnManufacturers.Image")));
            this.tsBtnManufacturers.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnManufacturers.Name = "tsBtnManufacturers";
            this.tsBtnManufacturers.Size = new System.Drawing.Size(146, 27);
            this.tsBtnManufacturers.Text = "Manufacturers";
            this.tsBtnManufacturers.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.tsBtnManufacturers.ToolTipText = "manage maufactureers record";
            this.tsBtnManufacturers.Click += new System.EventHandler(this.tsBtnManufacturers_Click);
            // 
            // tsBtnColors
            // 
            this.tsBtnColors.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsBtnColors.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnColors.Image")));
            this.tsBtnColors.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnColors.Name = "tsBtnColors";
            this.tsBtnColors.Size = new System.Drawing.Size(146, 27);
            this.tsBtnColors.Text = "Colors";
            this.tsBtnColors.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.tsBtnColors.Click += new System.EventHandler(this.tsBtnColors_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tsSideToolbar);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.Text = "Main";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMain_FormClosed);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tsSideToolbar.ResumeLayout(false);
            this.tsSideToolbar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStrip tsSideToolbar;
        private System.Windows.Forms.ToolStripButton tsBtnManufacturers;
        private System.Windows.Forms.ToolStripButton tsBtnColors;
    }
}

