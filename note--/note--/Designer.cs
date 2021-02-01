using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;

namespace note__
{
    public class Designer
    {
        public readonly Color DarkGray = Color.FromArgb(42,42,42);
        public readonly Color SpaceGray = Color.FromArgb(32, 32, 32);
        public readonly Color Gray = Color.FromArgb(50, 50, 50);
        public readonly Color White = Color.White;
        public readonly Color LightGray = Color.FromArgb(227, 227, 227);
        private Form1 form;
        public CScheme ColorScheme { get; set; }
        public enum CScheme
        {
            BW = 1,
            WB = 2
        }
        public Color FontColorDependOnScheme()
        {
            if(ColorScheme == CScheme.BW)
            {
                return Color.White;
            }
            else
            {
                return DarkGray;
            }
        }
        public  void BlackAndWhiteScheme()
        {
            form.BackColor = SpaceGray;
            foreach(Control control in form.Controls)
            {
                if(control is MenuStrip)
                {
                    control.BackColor = Gray;
                    foreach(var b in (control as MenuStrip).Items)
                    {
                        if (b is ToolStripMenuItem)
                        {
                            (b as ToolStripMenuItem).BackColor = Gray;
                            (b as ToolStripMenuItem).ForeColor = White;
                            foreach (var c in (b as ToolStripMenuItem).DropDownItems)
                            {
                                if (c is ToolStripMenuItem)
                                {
                                    (c as ToolStripMenuItem).BackColor = DarkGray;
                                    (c as ToolStripMenuItem).ForeColor = White;
                                    foreach (var j in (c as ToolStripMenuItem).DropDownItems)
                                    {
                                        if (j is ToolStripMenuItem)
                                        {
                                            (j as ToolStripMenuItem).BackColor = DarkGray;
                                            (j as ToolStripMenuItem).ForeColor = White;

                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else if(control is TabControl)
                {
                    control.BackColor = SpaceGray;
                    foreach (var t in (control as TabControl).TabPages)
                    {
                        (t as TabPage).BackColor = DarkGray;
                        (t as TabPage).ForeColor = White;
                        foreach (var r in (t as TabPage).Controls)
                        {
                            if(r is RichTextBox)
                            {
                                (r as RichTextBox).BackColor = SpaceGray;
                                (r as RichTextBox).ForeColor = White;
                                var cms = (r as RichTextBox).ContextMenuStrip;
                                cms.BackColor = SpaceGray;
                                cms.ForeColor = SpaceGray;
                                foreach (var i in cms.Items)
                                {
                                    if (i is ToolStripMenuItem)
                                    {
                                        (i as ToolStripMenuItem).BackColor = SpaceGray;
                                        (i as ToolStripMenuItem).ForeColor = White;
                                    }
                                }
                            }
                        }
                    }
                }
                else if(control is RichTextBox)
                {
                    (control as RichTextBox).BackColor = DarkGray;
                    (control as RichTextBox).ForeColor = Color.Coral;
                }

            }
            form.Refresh();
            form.Update();
            ColorScheme = CScheme.BW;
        }
        public void WhiteAndBlackScheme()
        {
            form.BackColor = White;
            foreach (Control control in form.Controls)
            {
                if (control is MenuStrip)
                {
                    control.BackColor = White;
                    foreach (var b in (control as MenuStrip).Items)
                    {
                        if (b is ToolStripMenuItem)
                        {
                            (b as ToolStripMenuItem).BackColor = White;
                            (b as ToolStripMenuItem).ForeColor = DarkGray;
                            foreach (var c in (b as ToolStripMenuItem).DropDownItems)
                            {
                                if (c is ToolStripMenuItem)
                                {
                                    (c as ToolStripMenuItem).BackColor = LightGray;
                                    (c as ToolStripMenuItem).ForeColor = DarkGray;
                                    foreach (var j in (c as ToolStripMenuItem).DropDownItems)
                                    {
                                        if (j is ToolStripMenuItem)
                                        {
                                            (j as ToolStripMenuItem).BackColor = LightGray;
                                            (j as ToolStripMenuItem).ForeColor = DarkGray;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else if (control is TabControl)
                {
                    control.BackColor = White;
                    foreach (var t in (control as TabControl).TabPages)
                    {
                        (t as TabPage).BackColor = LightGray;
                        (t as TabPage).ForeColor = DarkGray;
                        foreach (var r in (t as TabPage).Controls)
                        {
                            if (r is RichTextBox)
                            {
                                (r as RichTextBox).BackColor = LightGray;
                                (r as RichTextBox).ForeColor = DarkGray;
                            }
                            var cms = (r as RichTextBox).ContextMenuStrip;
                            cms.BackColor = White;
                            cms.ForeColor = DarkGray;
                            foreach (var i in cms.Items)
                            {
                                if (i is ToolStripMenuItem)
                                {
                                    (i as ToolStripMenuItem).BackColor = White;
                                    (i as ToolStripMenuItem).ForeColor = DarkGray;
                                }
                            }
                        }
                    }
                }
                else if(control is RichTextBox)
                {
                    (control as RichTextBox).BackColor = White;
                    (control as RichTextBox).ForeColor = Color.Coral;
                }
            }
            form.Refresh();
            form.Update();
            ColorScheme = CScheme.WB;
        }
        public void ActivateCurrent()
        {
            switch (ColorScheme)
            {
                case CScheme.BW:
                    BlackAndWhiteScheme();
                    return;
                case CScheme.WB:
                    WhiteAndBlackScheme();
                    return;
                default:
                    BlackAndWhiteScheme();
                    return;
            }
        }
        public Designer(Form1 form, CScheme scheme = CScheme.WB)
        {
            this.form = form;
            ColorScheme = scheme;
        }
    }
}
