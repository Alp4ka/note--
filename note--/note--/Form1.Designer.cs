namespace note__
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.FileButton = new System.Windows.Forms.ToolStripMenuItem();
            this.createButton = new System.Windows.Forms.ToolStripMenuItem();
            this.openButton = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsButton = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAllButton = new System.Windows.Forms.ToolStripMenuItem();
            this.closeFileButton = new System.Windows.Forms.ToolStripMenuItem();
            this.EditButton = new System.Windows.Forms.ToolStripMenuItem();
            this.formatButton = new System.Windows.Forms.ToolStripMenuItem();
            this.undoButton = new System.Windows.Forms.ToolStripMenuItem();
            this.redoButton = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpButton = new System.Windows.Forms.ToolStripMenuItem();
            this.settingButton = new System.Windows.Forms.ToolStripMenuItem();
            this.colorTrigger = new System.Windows.Forms.ToolStripMenuItem();
            this.bwButton = new System.Windows.Forms.ToolStripMenuItem();
            this.wbButton = new System.Windows.Forms.ToolStripMenuItem();
            this.timerButton = new System.Windows.Forms.ToolStripMenuItem();
            this.thirtySecButton = new System.Windows.Forms.ToolStripMenuItem();
            this.minuteButton = new System.Windows.Forms.ToolStripMenuItem();
            this.fiveMinuteButton = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.boldButton = new System.Windows.Forms.ToolStripMenuItem();
            this.underlineButton = new System.Windows.Forms.ToolStripMenuItem();
            this.italicButton = new System.Windows.Forms.ToolStripMenuItem();
            this.crossedButton = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.menuStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileButton,
            this.EditButton,
            this.HelpButton,
            this.settingButton});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // FileButton
            // 
            this.FileButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.FileButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createButton,
            this.openButton,
            this.saveAsButton,
            this.saveAllButton,
            this.closeFileButton});
            this.FileButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FileButton.ForeColor = System.Drawing.Color.White;
            this.FileButton.Name = "FileButton";
            this.FileButton.Size = new System.Drawing.Size(39, 21);
            this.FileButton.Text = "File";
            this.FileButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // createButton
            // 
            this.createButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.createButton.ForeColor = System.Drawing.Color.White;
            this.createButton.Name = "createButton";
            this.createButton.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.createButton.Size = new System.Drawing.Size(206, 22);
            this.createButton.Text = "Create New";
            this.createButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.createButton.Click += new System.EventHandler(this.CreateButtonClick);
            // 
            // openButton
            // 
            this.openButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.openButton.ForeColor = System.Drawing.Color.White;
            this.openButton.Name = "openButton";
            this.openButton.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openButton.Size = new System.Drawing.Size(206, 22);
            this.openButton.Text = "Open";
            this.openButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.openButton.Click += new System.EventHandler(this.OpenButtonClick);
            // 
            // saveAsButton
            // 
            this.saveAsButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.saveAsButton.ForeColor = System.Drawing.Color.White;
            this.saveAsButton.Name = "saveAsButton";
            this.saveAsButton.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveAsButton.Size = new System.Drawing.Size(206, 22);
            this.saveAsButton.Text = "Save As";
            this.saveAsButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.saveAsButton.Click += new System.EventHandler(this.SaveAsButtonClick);
            // 
            // saveAllButton
            // 
            this.saveAllButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.saveAllButton.ForeColor = System.Drawing.Color.White;
            this.saveAllButton.Name = "saveAllButton";
            this.saveAllButton.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.saveAllButton.Size = new System.Drawing.Size(206, 22);
            this.saveAllButton.Text = "Save All";
            this.saveAllButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.saveAllButton.Click += new System.EventHandler(this.SaveAll);
            // 
            // closeFileButton
            // 
            this.closeFileButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.closeFileButton.ForeColor = System.Drawing.Color.White;
            this.closeFileButton.Name = "closeFileButton";
            this.closeFileButton.Size = new System.Drawing.Size(206, 22);
            this.closeFileButton.Text = "Close File";
            this.closeFileButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.closeFileButton.Click += new System.EventHandler(this.CloseButtonClick);
            // 
            // EditButton
            // 
            this.EditButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.EditButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.formatButton,
            this.undoButton,
            this.redoButton});
            this.EditButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.EditButton.ForeColor = System.Drawing.Color.White;
            this.EditButton.Name = "EditButton";
            this.EditButton.Size = new System.Drawing.Size(42, 21);
            this.EditButton.Text = "Edit";
            this.EditButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // formatButton
            // 
            this.formatButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.formatButton.ForeColor = System.Drawing.Color.White;
            this.formatButton.Name = "formatButton";
            this.formatButton.Size = new System.Drawing.Size(192, 22);
            this.formatButton.Text = "Format File";
            this.formatButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.formatButton.Click += new System.EventHandler(this.FormatCurrentFile);
            // 
            // undoButton
            // 
            this.undoButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.undoButton.ForeColor = System.Drawing.Color.White;
            this.undoButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.undoButton.Name = "undoButton";
            this.undoButton.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoButton.Size = new System.Drawing.Size(192, 22);
            this.undoButton.Text = "Undo";
            this.undoButton.Click += new System.EventHandler(this.UndoButtonClick);
            // 
            // redoButton
            // 
            this.redoButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.redoButton.ForeColor = System.Drawing.Color.White;
            this.redoButton.Name = "redoButton";
            this.redoButton.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.Z)));
            this.redoButton.Size = new System.Drawing.Size(192, 22);
            this.redoButton.Text = "Redo";
            this.redoButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.redoButton.Click += new System.EventHandler(this.RedoButtonClick);
            // 
            // HelpButton
            // 
            this.HelpButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.HelpButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.HelpButton.ForeColor = System.Drawing.Color.White;
            this.HelpButton.Name = "HelpButton";
            this.HelpButton.Size = new System.Drawing.Size(47, 21);
            this.HelpButton.Text = "Help";
            this.HelpButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // settingButton
            // 
            this.settingButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.settingButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.settingButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.colorTrigger,
            this.timerButton});
            this.settingButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.settingButton.ForeColor = System.Drawing.Color.White;
            this.settingButton.Name = "settingButton";
            this.settingButton.Size = new System.Drawing.Size(66, 21);
            this.settingButton.Text = "Settings";
            this.settingButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // colorTrigger
            // 
            this.colorTrigger.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.colorTrigger.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bwButton,
            this.wbButton});
            this.colorTrigger.ForeColor = System.Drawing.Color.White;
            this.colorTrigger.Name = "colorTrigger";
            this.colorTrigger.Size = new System.Drawing.Size(171, 22);
            this.colorTrigger.Text = "Color Scheme";
            this.colorTrigger.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bwButton
            // 
            this.bwButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.bwButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.bwButton.ForeColor = System.Drawing.Color.White;
            this.bwButton.Name = "bwButton";
            this.bwButton.Size = new System.Drawing.Size(147, 22);
            this.bwButton.Text = "HardCoding";
            this.bwButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bwButton.Click += new System.EventHandler(this.BwClick);
            // 
            // wbButton
            // 
            this.wbButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.wbButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.wbButton.ForeColor = System.Drawing.Color.White;
            this.wbButton.Name = "wbButton";
            this.wbButton.Size = new System.Drawing.Size(147, 22);
            this.wbButton.Text = "4head";
            this.wbButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.wbButton.Click += new System.EventHandler(this.WbClick);
            // 
            // timerButton
            // 
            this.timerButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.timerButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.thirtySecButton,
            this.minuteButton,
            this.fiveMinuteButton});
            this.timerButton.ForeColor = System.Drawing.Color.White;
            this.timerButton.Name = "timerButton";
            this.timerButton.Size = new System.Drawing.Size(171, 22);
            this.timerButton.Text = "Auto-Save Delay";
            this.timerButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // thirtySecButton
            // 
            this.thirtySecButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.thirtySecButton.ForeColor = System.Drawing.Color.White;
            this.thirtySecButton.Name = "thirtySecButton";
            this.thirtySecButton.Size = new System.Drawing.Size(143, 22);
            this.thirtySecButton.Text = "30 Seconds";
            this.thirtySecButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // minuteButton
            // 
            this.minuteButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.minuteButton.ForeColor = System.Drawing.Color.White;
            this.minuteButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.minuteButton.Name = "minuteButton";
            this.minuteButton.Size = new System.Drawing.Size(143, 22);
            this.minuteButton.Text = "1 Minute";
            this.minuteButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // fiveMinuteButton
            // 
            this.fiveMinuteButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.fiveMinuteButton.ForeColor = System.Drawing.Color.White;
            this.fiveMinuteButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.fiveMinuteButton.Name = "fiveMinuteButton";
            this.fiveMinuteButton.Size = new System.Drawing.Size(143, 22);
            this.fiveMinuteButton.Text = "5 Minutes";
            this.fiveMinuteButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Location = new System.Drawing.Point(0, 53);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(0, 0);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(800, 397);
            this.tabControl1.TabIndex = 1;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "txt";
            // 
            // menuStrip2
            // 
            this.menuStrip2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.menuStrip2.AutoSize = false;
            this.menuStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.boldButton,
            this.underlineButton,
            this.italicButton,
            this.crossedButton});
            this.menuStrip2.Location = new System.Drawing.Point(0, 22);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(800, 36);
            this.menuStrip2.TabIndex = 2;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // boldButton
            // 
            this.boldButton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.boldButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.boldButton.Name = "boldButton";
            this.boldButton.Size = new System.Drawing.Size(36, 32);
            this.boldButton.Text = "B";
            this.boldButton.Click += new System.EventHandler(this.ToggleFont);
            // 
            // underlineButton
            // 
            this.underlineButton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point);
            this.underlineButton.Name = "underlineButton";
            this.underlineButton.Size = new System.Drawing.Size(37, 32);
            this.underlineButton.Text = "U";
            this.underlineButton.Click += new System.EventHandler(this.ToggleFont);
            // 
            // italicButton
            // 
            this.italicButton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.italicButton.Name = "italicButton";
            this.italicButton.Size = new System.Drawing.Size(29, 32);
            this.italicButton.Text = "I";
            this.italicButton.Click += new System.EventHandler(this.ToggleFont);
            // 
            // crossedButton
            // 
            this.crossedButton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Strikeout, System.Drawing.GraphicsUnit.Point);
            this.crossedButton.Name = "crossedButton";
            this.crossedButton.Size = new System.Drawing.Size(34, 32);
            this.crossedButton.Text = "S";
            this.crossedButton.Click += new System.EventHandler(this.ToggleFont);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.menuStrip2);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Note--";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem FileButton;
        private System.Windows.Forms.ToolStripMenuItem EditButton;
        private System.Windows.Forms.ToolStripMenuItem HelpButton;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.ToolStripMenuItem createButton;
        private System.Windows.Forms.ToolStripMenuItem openButton;
        private System.Windows.Forms.ToolStripMenuItem saveAsButton;
        private System.Windows.Forms.ToolStripMenuItem formatButton;
        private System.Windows.Forms.ToolStripMenuItem settingButton;
        private System.Windows.Forms.ToolStripMenuItem closeFileButton;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem undoButton;
        private System.Windows.Forms.ToolStripMenuItem redoButton;
        private System.Windows.Forms.ToolStripMenuItem colorTrigger;
        private System.Windows.Forms.ToolStripMenuItem bwButton;
        private System.Windows.Forms.ToolStripMenuItem wbButton;
        private System.Windows.Forms.ToolStripMenuItem saveAllButton;
        private System.Windows.Forms.ToolStripMenuItem timerButton;
        private System.Windows.Forms.ToolStripMenuItem thirtySecButton;
        private System.Windows.Forms.ToolStripMenuItem minuteButton;
        private System.Windows.Forms.ToolStripMenuItem fiveMinuteButton;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem boldButton;
        private System.Windows.Forms.ToolStripMenuItem underlineButton;
        private System.Windows.Forms.ToolStripMenuItem italicButton;
        private System.Windows.Forms.ToolStripMenuItem crossedButton;
    }
}

