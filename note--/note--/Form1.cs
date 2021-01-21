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

namespace note__
{
    public partial class Form1 : Form
    {
        private List<CachedFile> cachedFiles = new List<CachedFile>();
        private List<TabPage> pages = new List<TabPage>();
        private Designer designer;
        
        public Form1()
        {
            InitializeComponent();
            designer = new Designer(this);
            designer.ActivateCurrent();
        }

        private void UndoButtonClick(object sender, EventArgs e)
        {
            TabPage selectedTab = this.tabControl1.SelectedTab;
            var cached = cachedFiles.Find(x => x.FullPath == selectedTab.Text || selectedTab.Text == x.TempName);
            var rtb = (selectedTab.Controls.Find("rtb", true)[0] as RichTextBox);
            string text = rtb.Text;
            rtb.Text = cached.Undo();
            rtb.SelectionStart = rtb.Text.Length;
        }

        private void RedoButtonClick(object sender, EventArgs e)
        {
            TabPage selectedTab = this.tabControl1.SelectedTab;
            var cached = cachedFiles.Find(x => x.FullPath == selectedTab.Text || selectedTab.Text == x.TempName);
            var rtb = (selectedTab.Controls.Find("rtb", true)[0] as RichTextBox);
            string text = rtb.Text;
            rtb.Text = cached.Redo();
            rtb.SelectionStart = rtb.Text.Length;
        }

        private void Kdown(object sender, KeyEventArgs e)
        {
            if(e!= null)
            {
                if ((e.Control && e.KeyCode == Keys.Z) || (e.Control && e.Shift && e.KeyCode == Keys.Z))
                {
                }
            }
            
            if ( e == null || e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                TabPage selectedTab = this.tabControl1.SelectedTab;
                var cached = cachedFiles.Find(x => x.FullPath == selectedTab.Text || selectedTab.Text == x.TempName);
                var rtb = sender as RichTextBox;
                string text = rtb.Text;
                cached.SaveChange(text);
            }
        }
        private void BwClick(object sender, EventArgs e)
        {
            designer.BlackAndWhiteScheme();
        }
        private void WbClick(object sender, EventArgs e)
        {
            designer.WhiteAndBlackScheme();
        }
        private void SaveChanges(object sender, EventArgs e)
        {
                
                TabPage selectedTab = this.tabControl1.SelectedTab;
                var cached = cachedFiles.Find(x => x.FullPath == selectedTab.Text || selectedTab.Text == x.TempName);
                var rtb = sender as RichTextBox;
                string text = rtb.Text;
                cached.SaveChange(text);
        }
        private void CreateButtonClick(object sender, EventArgs e)
        {
            var tp = new TabPage();
            var cachedFile = new CachedFile(cachedFiles);
            cachedFiles.Add(cachedFile);
            var rtb = RtbByText("");
            tabControl1.TabPages.Add(tp);
            

            tp.BackColor = Color.FromArgb(42, 42, 42);
            tp.ForeColor = Color.FromArgb(42, 42, 42);
            tp.UseVisualStyleBackColor = false;
            tp.Text = cachedFile.TempName;
            tp.Controls.Add(rtb);

            tabControl1.SelectedTab = tp;
            designer.ActivateCurrent();

        }
        private void CloseButtonClick(object sender, EventArgs e)
        {
            TabPage selectedTab = this.tabControl1.SelectedTab;
            var cached = cachedFiles.Find(x => x.FullPath == selectedTab.Text || selectedTab.Text == x.TempName);
            DialogResult result = MessageBox.Show(
                $"Want to save changes on {cached.FullName}",
                "Information",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
                SaveAsButtonClick(null, null);

            try
            {
                ForceClose(selectedTab, cached);
            }
            catch (ArgumentNullException)
            {
                return;
            }
        }

        private void ForceClose(TabPage selectedTab, CachedFile cf)
        {
            
            tabControl1.TabPages.Remove(selectedTab);
            cachedFiles.Remove(cf);
        }
        private RichTextBox RtbByText(string text)
        {
            RichTextBox rtb = new RichTextBox();
            rtb.Name = "rtb";
            rtb.Text = text;
            rtb.BackColor = Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            rtb.ForeColor = Color.White;
            rtb.BorderStyle = BorderStyle.None;
            rtb.Dock = DockStyle.Fill;
            rtb.Margin = new Padding(10, 0, 0, 0);
            rtb.Font = new Font(rtb.Font.Name, 13.0F, rtb.Font.Style, rtb.Font.Unit);
            rtb.KeyUp += new KeyEventHandler(Kdown);
            return rtb;
        }
        private void OpenFileByPath(string fullpath)
        {
            TabPage tp = new TabPage();
            CachedFile cachedFile = new CachedFile(fullpath);
            var rtb = RtbByText(cachedFile.Text);
            tp.Controls.Add(rtb);
            tabControl1.TabPages.Add(tp);
            

            cachedFiles.Add(cachedFile);

            tp.BackColor = Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            tp.ForeColor = Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            tp.UseVisualStyleBackColor = false;
            tp.Text = cachedFile.FullPath;

            tabControl1.SelectedTab = tp;
            designer.ActivateCurrent();
        }
        private void OpenButtonClick(object sender, EventArgs e)
        {
            
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string fullpath = openFileDialog1.FileName;
                    OpenFileByPath(fullpath);
                }
            }
            catch
            {
                MessageBox.Show("Непредвиденная ошибка во время открытия файла.");
            }
            
        }
        private void SaveAsButtonClick(object sender, EventArgs e)
        {
            TabPage selectedTab = this.tabControl1.SelectedTab;
            var cached = cachedFiles.Find(x => x.FullPath == selectedTab.Text || selectedTab.Text == x.TempName);

            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = "(*.txt)|*.txt|(*.rtf)|*.rtf";
                dialog.FileName = cached.TempName;
                dialog.DefaultExt = cached.Extension;
                dialog.FilterIndex = 1;
                dialog.RestoreDirectory = true;
                try
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        string text;
                        if (Path.GetExtension(dialog.FileName) == ".txt")
                        {
                            text = (selectedTab.Controls.Find("rtb", true)[0] as RichTextBox).Text;
                        }
                        else
                        {
                            var rb = (selectedTab.Controls.Find("rtb", true)[0] as RichTextBox);
                            rb.ForeColor = Color.Black;
                            text = rb.Rtf;
                            rb.ForeColor = Color.White;
                        }

                        File.WriteAllText(dialog.FileName, text);
                        tabControl1.TabPages.Remove(selectedTab);
                        cachedFiles.Remove(cached);
                        OpenFileByPath(dialog.FileName);
                    }
                }
                catch
                {
                    MessageBox.Show("Непредвиденная ошибка во время сохранения файла.");
                }
            }
        }
    }
}
