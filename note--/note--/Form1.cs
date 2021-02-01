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
using System.Diagnostics;
using System.Globalization;

namespace note__
{
    public partial class Form1 : Form
    {
        private List<CachedFile> cachedFiles = new List<CachedFile>();
        private List<TabPage> _pages = new List<TabPage>();
        private Designer _designer;
        private Timer _autoSave;
        private string _compilerPath = @"C:\Windows\Microsoft.NET\Framework\v3.5\csc.exe";
        private int _delay = Utils.delays[0];
        private Dictionary<int, ToolStripMenuItem> delayToTool = new Dictionary<int, ToolStripMenuItem>();
        public Form1(string[] files = null, Designer.CScheme scheme = Designer.CScheme.WB, int delay = 30000)
        {
            
            InitializeComponent();
            delayToTool.Add(Utils.delays[0], thirtySecButton);
            delayToTool.Add(Utils.delays[1], minuteButton);
            delayToTool.Add(Utils.delays[2], fiveMinuteButton);
            foreach (ToolStripMenuItem t in delayToTool[delay].Owner.Items)
            {
                t.Checked = false;
            }
            delayToTool[delay].Checked = true;
            _delay = delay;

            _designer = new Designer(this, scheme);
            _designer.ActivateCurrent();

            _autoSave = new Timer();
            _autoSave.Tick += new EventHandler(TimerEventProcessor);
            _autoSave.Interval = _delay;
            _autoSave.Start();
            if (files != null)
            {
                foreach (string path in files)
                {
                    OpenFileByPath(path);
                }
            }

            compilePathButton.Text = "Set Compiler " + _compilerPath;
        }
        private void ToggleOutline(object sender, EventArgs myEventArgs)
        {
            if((sender as ToolStripMenuItem).Checked)
            {
                (sender as ToolStripMenuItem).Checked = false;
            }
            else
            {
                    DrawOutline(true);

                (sender as ToolStripMenuItem).Checked = true;
            }
        }


        private void DrawOutline(bool state)
        {
            TabPage selectedTab = tabControl1.SelectedTab;
            if (selectedTab == null)
            {
                return;
            }
            var cached = cachedFiles.Find(x => x.FullPath == selectedTab.Text || selectedTab.Text == x.TempName);
            var rtb = GetRtbInTab(selectedTab);
            var cursorPosition = rtb.SelectionStart;
            rtb.SelectionStart = 0;
            rtb.SelectionLength = rtb.Text.Length;
            rtb.SelectionColor = _designer.FontColorDependOnScheme();
            if (state)
            {
                rtb.SuspendLayout();
                string text = rtb.Text;
                List<Color> colors = new List<Color>(new Color[] {Color.Blue, Color.Magenta, Color.Green});
                List<string[]> serviceWords = new List<string[]>();
                serviceWords.Add(Utils.blueWords);
                serviceWords.Add(Utils.magentaWords);
                serviceWords.Add(Utils.greenWords);
                for (int i = 0; i < serviceWords.Count(); ++i)
                {
                    foreach (string word in serviceWords[i])
                    {
                        int start = 0;
                        int startTemp = 0;
                        while (true)
                        {
                            try
                            {
                                start = text.IndexOf(word, startTemp);
                            }
                            catch 
                            {
                                break;
                            }
                            if (start != -1)
                            {
                                rtb.SelectionStart = start;
                                rtb.SelectionLength = word.Length;
                                rtb.SelectionColor = colors[i]; ;
                                startTemp += word.Length;
                                if (startTemp >= text.Length)
                                {
                                    break;
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                }

                int tempStart = 0;
                //strings
                while (true)
                {
                    if(tempStart >= text.Length)
                    {
                        break;
                    }
                    var indexStart = text.IndexOf('"', tempStart);
                    if(indexStart == -1)
                    {
                        break;
                    }
                    var indexEnd = -1;
                    if(indexStart + 1 >= text.Length)
                    {
                        indexEnd = text.Length;
                    }
                    else
                    {
                        indexEnd = text.IndexOf('"', indexStart + 1);
                    }
                    if(indexEnd == -1)
                    {
                        indexEnd = text.Length;
                        rtb.SelectionStart = indexStart;
                        rtb.SelectionLength = indexEnd - indexStart+1;
                        rtb.SelectionColor = Color.Coral;
                        break;
                    }
                    rtb.SelectionStart = indexStart;
                    rtb.SelectionLength = indexEnd - indexStart+1;
                    rtb.SelectionColor = Color.Coral;
                    tempStart = indexEnd+1;
                }
                tempStart = 0;
                while (true)
                {
                    if (tempStart >= text.Length)
                    {
                        break;
                    }
                    int indexStart = text.IndexOf("//", tempStart);
                    int indexEnd = -1;
                    if (indexStart == -1)
                    {
                        break;
                    }
                    indexEnd = text.IndexOf("\n", indexStart);
                    if (indexEnd == -1)
                    {

                        indexEnd = text.Length;
                        rtb.SelectionStart = indexStart;
                        rtb.SelectionLength = indexEnd - indexStart + 1;
                        rtb.SelectionColor = Color.Red;
                        break;
                    }
                    rtb.SelectionStart = indexStart;
                    rtb.SelectionLength = indexEnd - indexStart + 1;
                    rtb.SelectionColor = Color.Red;
                    tempStart = indexEnd + 1;
                }

                tempStart = 0;
                while (true)
                {
                    if (tempStart >= text.Length)
                    {
                        break;
                    }
                    int indexStart = text.IndexOf("/*", tempStart);
                    int indexEnd = -1;
                    if (indexStart == -1)
                    {
                        break;
                    }
                    indexEnd = text.IndexOf("*/", indexStart);
                    if (indexEnd == -1)
                    {

                        indexEnd = text.Length;
                        rtb.SelectionStart = indexStart;
                        rtb.SelectionLength = indexEnd - indexStart + 2;
                        rtb.SelectionColor = Color.Red;
                        break;
                    }
                    rtb.SelectionStart = indexStart;
                    rtb.SelectionLength = indexEnd - indexStart + 2;
                    rtb.SelectionColor = Color.Red;
                    tempStart = indexEnd + 2;
                }
                rtb.SelectionStart = cursorPosition;
                rtb.SelectionLength = 0;
                rtb.ResumeLayout();
            }
        }
        private void SetCompiler(object sender, EventArgs e)
        {
            var openFileDialog2 = new OpenFileDialog { Title = "Choose compiler", Filter = ".exe|*.exe" };
            var res = openFileDialog2.ShowDialog();
            if (res != DialogResult.OK)
                return;
            _compilerPath = openFileDialog2.FileName;
            (sender as ToolStripMenuItem).Text = "Set Compiler " + _compilerPath;
        }
        protected void TxtChanged(object sender, EventArgs e)
        {
            outlineButton.Checked = false;
        }
        private void TimerEventProcessor(object sender, EventArgs myEventArgs)
        {
            TabPage selectedTab = tabControl1.SelectedTab;
            if (selectedTab == null)
            {
                return;
            }
            var cached = cachedFiles.Find(x => x.FullPath == selectedTab.Text || selectedTab.Text == x.TempName);
            var rtb = GetRtbInTab(selectedTab);
            if (cached.FullPath != null && File.Exists(cached.FullPath))
            {
                string text = "";
                if(cached.Extension == ".rtf")
                {
                    var rb = (selectedTab.Controls.Find("rtb", true)[0] as RichTextBox);
                    rb.SaveFile(cached.FullPath);
                }
                else
                {
                    text = (selectedTab.Controls.Find("rtb", true)[0] as RichTextBox).Text;
                    File.WriteAllText(cached.FullPath, text);
                }
            }
            //_autoSave.Stop();
            //_autoSave.Start();
        }

        
        private void ToggleAutoSave(object sender, EventArgs e)
        {


            foreach(ToolStripMenuItem item in timerButton.DropDownItems)
            {
                item.Checked = false;
            }
            (sender as ToolStripMenuItem).Checked = true;
            _delay = Utils.GetMsByName(sender as ToolStripMenuItem);
            _autoSave.Stop();
            _autoSave.Interval = _delay;
            _autoSave.Start();
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
        private void GoToHelp(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process myProcess = new System.Diagnostics.Process();
                myProcess.StartInfo.UseShellExecute = true;
                myProcess.StartInfo.FileName = "https://psv4.userapi.com/c505536/u240698365/docs/d15/c008955b8c6c/help.png?extra=Sjb0UCV2QFXoLZ8krXuk4n1Pbe952jmiyJb-h48DEJ1nR1D1_RYPyxlUFxbNxAImXRahnOOb2q_Zq74f9tx1MsjJ7j0OybPgjIchK_ejYDT2FDJehAMAxR8GuppWMwK4du0PmKbNS7eMj_aqI4kSm2fb";
                myProcess.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"It was supposed you 'll be able to open this site in broweser but something went wrong :C. \n\nInfo:\n {ex.Message}", "Ooops...");
            }
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
            if (cached == null)
            {
                return;
            }
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
        private ContextMenuStrip CreateCms()
        {
            ContextMenuStrip cms = new ContextMenuStrip();
            ToolStripMenuItem copyItem = new ToolStripMenuItem("Copy");
            copyItem.Click += new EventHandler(CopySelected);
            ToolStripMenuItem cutItem = new ToolStripMenuItem("Cut");
            cutItem.Click += new EventHandler(CutSelected);
            ToolStripMenuItem pasteItem = new ToolStripMenuItem("Paste");
            pasteItem.Click += new EventHandler(Paste);
            ToolStripMenuItem selectAllItem = new ToolStripMenuItem("Select All");
            selectAllItem.Click += new EventHandler(SelectAllText);
            List<ToolStripMenuItem> temp = (new[] { copyItem, pasteItem, cutItem, selectAllItem, }).ToList();
            temp.ForEach(x => x.DisplayStyle = ToolStripItemDisplayStyle.Text);
            cms.Items.AddRange(temp.ToArray());
            return cms;
        }

        private void CompileAsCs(object sender, EventArgs e)
        {
            try
            {
                var openFileDialog2 = new OpenFileDialog { Title = "Отрыть файл", Filter = ".cs|*.cs" };
                var res = openFileDialog2.ShowDialog();
                if (res != DialogResult.OK)
                    return;
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                Process process = new Process
                {
                    StartInfo = new ProcessStartInfo()
                    {
                        StandardOutputEncoding = Encoding.GetEncoding(CultureInfo.CurrentCulture.TextInfo.OEMCodePage),
                        FileName = _compilerPath,
                        Arguments = @$"/t:exe /out:{Path.GetDirectoryName(openFileDialog2.FileName) + "\\temp_exec.exe"} {openFileDialog2.FileName}",
                        RedirectStandardError = true,
                        RedirectStandardOutput = true,
                        RedirectStandardInput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };
                File.Create(Path.GetDirectoryName(openFileDialog2.FileName) + "\\temp_exec.exe").Close();
                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                //MessageBox.Show(process.StandardOutput.ReadToEnd());
                process.Close();
                infoBox.Text = output;
            }
            catch
            {
                MessageBox.Show("Choose correct compiler. Maybe it's in C:/Windows/Microsoft.NET/Framework/v3.5/csc.exe", "Wrong Compiler");
            }
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
            rtb.TextChanged += new EventHandler(TxtChanged);
            var cms = CreateCms();
            rtb.ContextMenuStrip = cms;
            return rtb;
        }
        private RichTextBox RtbByPath(string path)
        {
            RichTextBox rtb = new RichTextBox();
            rtb.Name = "rtb";
            //rtb.LoadFile(path);
            try
            {
                rtb.Rtf = File.ReadAllText(path);
            }
            catch (ArgumentException e)
            {
                rtb.Text = File.ReadAllText(path);
            }
            rtb.BackColor = Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            //rtb.ForeColor = Color.White;
            rtb.BorderStyle = BorderStyle.None;
            rtb.Dock = DockStyle.Fill;
            rtb.Margin = new Padding(10, 0, 0, 0);
            //rtb.Font = new Font(rtb.Font.Name, 13.0F, rtb.Font.Style, rtb.Font.Unit);
            rtb.ShortcutsEnabled = false;
            rtb.KeyUp += new KeyEventHandler(Kdown);
            var cms = CreateCms();
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
        }

        private void Paste(object sender, EventArgs e)
        {
            
            try
            {
                var rtb = tabControl1.SelectedTab.Controls.Find("rtb", true)[0] as RichTextBox;
                rtb.SelectedText = Clipboard.GetData(DataFormats.Text).ToString();
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
            else
            {
                rtb = RtbByText(File.ReadAllText(fullpath));
            }
            tp.Controls.Add(rtb);
            tabControl1.TabPages.Add(tp);


            cachedFiles.Add(cachedFile);

            tp.BackColor = Color.FromArgb(42, 42, 42);
            tp.ForeColor = Color.FromArgb(42, 42, 42);
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
                dialog.Filter = "(*.txt)|*.txt|(*.rtf)|*.rtf|(*.cs)|*.cs";
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
                            File.WriteAllText(dialog.FileName, text);
                        }

                        else if (Path.GetExtension(dialog.FileName) == ".cs")
                        {
                            text = (selectedTab.Controls.Find("rtb", true)[0] as RichTextBox).Text;
                            File.WriteAllText(dialog.FileName, text);
                        }
                        else
                        {
                            var rb = (selectedTab.Controls.Find("rtb", true)[0] as RichTextBox);
                            rb.SaveFile(dialog.FileName);
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

        private void CloseMethod(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show(
                $"You sure want to exit?",
                "Information",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1);
            if (result == DialogResult.Yes)
            {
                SaveAll(null, null);
                e.Cancel = false;
                string schemeToSave = ((int)_designer.ColorScheme).ToString();
                string delayAsString = _delay.ToString();
                try
                {
                    File.WriteAllLines(Program.settingsPath, new string[] { schemeToSave , delayAsString});
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                try
                {
                    File.WriteAllLines(Program.cachedPath, cachedFiles.Select(x => x.FullPath).ToArray());
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else
            {
                e.Cancel = true;
            }
            
        }
    }
}
