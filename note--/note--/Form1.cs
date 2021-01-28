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
        private Designer _designer;
        private string _buffer = "";

        public Form1()
        {
            InitializeComponent();
            _designer = new Designer(this);
            _designer.ActivateCurrent();
        }

        public void ToggleFont(object sender, EventArgs e)
        {
            TabPage selectedTab = this.tabControl1.SelectedTab;
            if (selectedTab == null)
            {
                return;
            }
            var cached = cachedFiles.Find(x => x.FullPath == selectedTab.Text || selectedTab.Text == x.TempName);
            var rtb = GetRtbInTab(selectedTab);
            Font font = rtb.SelectionFont;
            FontStyle fontStyle;
            FontStyle temp;
            switch ((sender as ToolStripMenuItem).Text)
            {
                case "B":
                    temp = FontStyle.Bold;
                    break;
                case "I":
                    temp = FontStyle.Italic;
                    break;
                case "U":
                    temp = FontStyle.Underline;
                    break;
                case "S":
                    temp = FontStyle.Strikeout;
                    break;
                default:
                    return;
            }

                
            if ((rtb.SelectionFont.Bold && temp == FontStyle.Bold) || 
                (rtb.SelectionFont.Italic && temp == FontStyle.Italic) || 
                (rtb.SelectionFont.Underline && temp == FontStyle.Underline ) || 
                (rtb.SelectionFont.Strikeout && temp == FontStyle.Strikeout))
            {

                fontStyle = FontStyle.Regular;
            }
            else
            {
                fontStyle = temp;
            }
            rtb.SelectionFont = new Font(font.FontFamily,font.Size, fontStyle);
        }


        private void UndoButtonClick(object sender, EventArgs e)
        {

            TabPage selectedTab = this.tabControl1.SelectedTab;
            if(selectedTab == null)
            {
                MessageBox.Show(
                $"There is nothing to undo.",
                "Error!",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1);
                return;
            }
            var cached = cachedFiles.Find(x => x.FullPath == selectedTab.Text || selectedTab.Text == x.TempName);
            var rtb = GetRtbInTab(selectedTab);
            if (rtb == null)
            {
                MessageBox.Show(
                $"There is nothing to undo.",
                "Error!",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1);
                return;
            }
            //string text = rtb.Rtf;
            rtb.Rtf = cached.Undo();
            rtb.SelectionStart = rtb.Text.Length;

        }

        private void RedoButtonClick(object sender, EventArgs e)
        {
            TabPage selectedTab = this.tabControl1.SelectedTab;
            if(selectedTab == null)
            {
                MessageBox.Show(
                $"There is nothing to redo.",
                "Error!",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1);
                return;
            }
            var cached = cachedFiles.Find(x => x.FullPath == selectedTab.Text || selectedTab.Text == x.TempName);
            var rtb = GetRtbInTab(selectedTab);
            if (rtb == null)
            {
                MessageBox.Show(
                $"There is nothing to redo.",
                "Error!",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1);
                return;
            }
            string text = rtb.Rtf;
            rtb.Rtf = cached.Redo();
            rtb.SelectionStart = rtb.Text.Length;
        }
        private RichTextBox GetRtbInTab(TabPage tp)
        {
            if (tp.Controls.Find("rtb", true).Length > 0)
            {
                return tp.Controls.Find("rtb", true)[0] as RichTextBox;
            }
            else
            {
                return null;
            }
        }
        private void FormatCurrentFile(object sender, EventArgs e)
        {
            TabPage selectedTab = this.tabControl1.SelectedTab;
            if(selectedTab == null)
            {
                MessageBox.Show(
                $"There is nothing to edit.",
                "Error!",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1);
                return;
            }
            RichTextBox rtb = new RichTextBox();
            rtb = GetRtbInTab(selectedTab);
            if (rtb == null)
            {
                MessageBox.Show(
                $"There is nothing to edit.",
                "Error!",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1);
                return;
            }
            string text = rtb.Text;
            text = text.Replace("{", "\n{\n");
            text = text.Replace("}", "\n}\n");
            text = text.Replace(";", ";\n");
            string newText = "";
            while (true)
            {
                newText = text.Replace("\n\n", "\n");
                if (newText == text)
                {
                    break;
                }
                else
                {
                    text = newText;
                }
            }
            string[] textArray = text.Split("\n");

            List<string> newStringArray = new List<string>();

            /*for(int i =0; i < textArray.Length; ++i)
            {
                textArray[i] = textArray[i].Trim();
                var indexL = textArray[i].IndexOf('{')
                if (indexL != -1 && textArray[i].Length != 1)
                {
                    textArray[i].Replace("{", "\n{\n");
                    newStringArray.Add(textArra)
                }

                else if()
            }
            MessageBox.Show(text);*/
            for (int i = 0; i < textArray.Length; ++i)
            {
                if (!String.IsNullOrWhiteSpace(textArray[i].Trim()))
                {
                    newStringArray.Add(textArray[i]);
                }
            }
            int tabCnter = 0;
            for (int i = 0; i < newStringArray.Count; ++i)
            {
                if (newStringArray[i].Contains('}'))
                {
                    tabCnter = Utils.ReturnEdged(0, 100, tabCnter - 1);
                }
                newStringArray[i] = new String(' ', 4 * tabCnter) + newStringArray[i].TrimStart();
                if (newStringArray[i].Contains('{'))
                {
                    tabCnter = Utils.ReturnEdged(0, 100, tabCnter + 1);
                }

            }
            text = String.Join("\n", newStringArray);
            rtb.Text = text;
            Kdown(rtb, null);
        }
        private void SaveAll(object sender, EventArgs e)
        {
            foreach(TabPage tab in tabControl1.TabPages)
            {
                tabControl1.SelectedTab = tab;
                SaveAsButtonClick(null ,null);
            }
        }
        private void Kdown(object sender, KeyEventArgs e)
        {
            if (e != null)
            {
                if ((e.Control && e.KeyCode == Keys.Z) || (e.Control && e.Shift && e.KeyCode == Keys.Z))
                {
                }
            }

            if (e == null || e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                TabPage selectedTab = this.tabControl1.SelectedTab;
                var cached = cachedFiles.Find(x => x.FullPath == selectedTab.Text || selectedTab.Text == x.TempName);
                var rtb = sender as RichTextBox;
                string text = rtb.Rtf;
                cached.SaveChange(text);
            }
        }
        private void BwClick(object sender, EventArgs e)
        {
            _designer.BlackAndWhiteScheme();
        }
        private void WbClick(object sender, EventArgs e)
        {
            _designer.WhiteAndBlackScheme();
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
            _designer.ActivateCurrent();

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
            //rtb.ShortcutsEnabled = false;
            rtb.KeyUp += new KeyEventHandler(Kdown);
            ContextMenuStrip cms = new ContextMenuStrip();
            ToolStripMenuItem copyItem = new ToolStripMenuItem("Copy");
            copyItem.Click += new EventHandler(CopySelected);
            //copyItem.ShortcutKeys = (Keys.Control | Keys.C);
            ToolStripMenuItem cutItem = new ToolStripMenuItem("Cut");
            cutItem.Click += new EventHandler(CutSelected);
            ToolStripMenuItem pasteItem = new ToolStripMenuItem("Paste");
            //pasteItem.ShortcutKeys = (Keys.Control | Keys.V);
            pasteItem.Click += new EventHandler(Paste);
            ToolStripMenuItem selectAllItem = new ToolStripMenuItem("Select All");
            selectAllItem.Click += new EventHandler(SelectAllText);
            List<ToolStripMenuItem> temp = (new[] { copyItem, pasteItem, cutItem, selectAllItem, }).ToList();
            temp.ForEach(x => x.DisplayStyle = ToolStripItemDisplayStyle.Text);
            cms.Items.AddRange(temp.ToArray());
            rtb.ContextMenuStrip = cms;
            return rtb;
        }
        private RichTextBox RtbByPath(string path)
        {
            RichTextBox rtb = new RichTextBox();
            rtb.Name = "rtb";
            rtb.LoadFile(path);
            rtb.BackColor = Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            //rtb.ForeColor = Color.White;
            rtb.BorderStyle = BorderStyle.None;
            rtb.Dock = DockStyle.Fill;
            rtb.Margin = new Padding(10, 0, 0, 0);
            //rtb.Font = new Font(rtb.Font.Name, 13.0F, rtb.Font.Style, rtb.Font.Unit);
            rtb.ShortcutsEnabled = false;
            rtb.KeyUp += new KeyEventHandler(Kdown);
            ContextMenuStrip cms = new ContextMenuStrip();
            ToolStripMenuItem copyItem = new ToolStripMenuItem("Copy");
            copyItem.Click += new EventHandler(CopySelected);
            copyItem.ShortcutKeys = (Keys.Control | Keys.C);
            ToolStripMenuItem cutItem = new ToolStripMenuItem("Cut");
            ToolStripMenuItem pasteItem = new ToolStripMenuItem("Paste");
            pasteItem.ShortcutKeys = (Keys.Control | Keys.V);
            pasteItem.Click += new EventHandler(Paste);
            ToolStripMenuItem selectAllItem = new ToolStripMenuItem("Select All");
            List<ToolStripMenuItem> temp = (new[] { copyItem, pasteItem, cutItem, selectAllItem, }).ToList();
            temp.ForEach(x => x.DisplayStyle = ToolStripItemDisplayStyle.Text);
            cms.Items.AddRange(temp.ToArray());
            rtb.ContextMenuStrip = cms;
            return rtb;

        }


        private void CopySelected(object sender, EventArgs e)
        {
            try
            {
                var rtb = tabControl1.SelectedTab.Controls.Find("rtb", true)[0] as RichTextBox;
                Clipboard.SetText(rtb.SelectedText);
            }
            catch
            {
                return;
            }
            // MessageBox.Show(Clipboard.GetData(DataFormats.Rtf).ToString());
        }

        private void SelectAllText(object sender, EventArgs e)
        {
            try
            {
                var rtb = tabControl1.SelectedTab.Controls.Find("rtb", true)[0] as RichTextBox;
                rtb.SelectAll();
            }
            catch
            {
                return;
            }
            // MessageBox.Show(Clipboard.GetData(DataFormats.Rtf).ToString());
        }

        private void Paste(object sender, EventArgs e)
        {
            
            try
            {
                var rtb = tabControl1.SelectedTab.Controls.Find("rtb", true)[0] as RichTextBox;
                rtb.SelectedText = Clipboard.GetData(DataFormats.Text).ToString();
                //MessageBox.Show(Clipboard.GetData(DataFormats.Text).ToString());
            }
            catch
            {
                return;
            }
        }
        private void CutSelected(object sender, EventArgs e)
        {
            var rtb = tabControl1.SelectedTab.Controls.Find("rtb", true)[0] as RichTextBox;
            try
            {
                Clipboard.SetText(rtb.SelectedText);
                rtb.SelectedText = "";
            }
            catch
            {
                return;
            }
        }
        private void OpenFileByPath(string fullpath)
        {
            if (cachedFiles.Select(x => x.FullPath).Contains(fullpath))
            {
                foreach (TabPage t in tabControl1.TabPages)
                {
                    if (t.Text == fullpath)
                    {
                        tabControl1.SelectedTab = t;
                        return;
                    }
                }
            }
            TabPage tp = new TabPage();
            CachedFile cachedFile = new CachedFile(fullpath);
            string ext = Path.GetExtension(fullpath);
            RichTextBox rtb;
            if (ext == ".rtf")
            {
                rtb = RtbByPath(fullpath);
            }
            else if (ext == ".txt")
            {
                rtb = RtbByText(File.ReadAllText(fullpath));
            }
            else
            {
                return;
            }
            tp.Controls.Add(rtb);
            tabControl1.TabPages.Add(tp);


            cachedFiles.Add(cachedFile);

            tp.BackColor = Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            tp.ForeColor = Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            tp.UseVisualStyleBackColor = false;
            tp.Text = cachedFile.FullPath;

            tabControl1.SelectedTab = tp;
            _designer.ActivateCurrent();
        }
        private void OpenButtonClick(object sender, EventArgs e)
        {


            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fullpath = openFileDialog1.FileName;
                OpenFileByPath(fullpath);
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
                            //MessageBox.Show(text);
                            File.WriteAllText(dialog.FileName, text);
                        }
                        else
                        {
                            var rb = (selectedTab.Controls.Find("rtb", true)[0] as RichTextBox);
                            //rb.ForeColor = Color.Black;
                            rb.SaveFile(dialog.FileName);
                            //rb.ForeColor = Color.White;
                        }


                        tabControl1.TabPages.Remove(selectedTab);
                        cachedFiles.Remove(cached);
                        OpenFileByPath(dialog.FileName);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Непредвиденная ошибка во время сохранения файла." + ex.Message);
                }
            }
        }
    }
}
