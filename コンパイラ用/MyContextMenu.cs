using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TalkScript.Viewer.Controls
{
    public sealed class MyContextMenu:ContextMenuStrip
    {
        private Form _view;
        public MyContextMenu(Form view,bool isFirstForm)
        {
            _view = view;
            ImageScalingSize = new System.Drawing.Size(24, 24);
            Name = "contextMenuStrip1";
            Size = new System.Drawing.Size(118, 36);

            ToolStripMenuItem menuItem1 = new ToolStripMenuItem();
            menuItem1.Name = "StripMenuItem1";
            menuItem1.Size = new System.Drawing.Size(117, 32);
            menuItem1.Text = "font";
            menuItem1.Click += new System.EventHandler(ToolStripMenu1_Click);
            Items.AddRange(new ToolStripItem[] {menuItem1});

            if (isFirstForm)
            {
                ToolStripMenuItem menuItem2 = new ToolStripMenuItem();
                menuItem2.Name = "StripMenuItem2";
                menuItem2.Size = new System.Drawing.Size(117, 32);
                menuItem2.Text = "ツールを追加起動";
                menuItem2.Click += new System.EventHandler(ToolStripMenu2_Click);
                Items.AddRange(new ToolStripItem[] { menuItem2 });
            }
            
        }

        /// <summary>
        /// フォントサイズ変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenu1_Click(object sender, EventArgs e)
        {
            using (FontDialog fd = new FontDialog())
            {
                fd.Font = _view.Font;
                fd.Color = _view.ForeColor;
                fd.MaxSize = 28;
                fd.MinSize = 8;
                fd.FontMustExist = true;
                fd.AllowVerticalFonts = false;
                fd.ShowColor = true;
                fd.ShowEffects = false;

                if (fd.ShowDialog() != DialogResult.Cancel)
                {
                    _view.Font = fd.Font;
                    foreach (Control ctrl in _view.Controls)
                    {
                        if (ctrl is TextBox)
                        {
                            ctrl.Font = fd.Font;
                        }
                    }
                }  
            }  

        }

        /// <summary>
        /// ツール追加起動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenu2_Click(object sender, EventArgs e)
        {
            
            FormCollection fCollection = Application.OpenForms;
            foreach(Form form in fCollection)
            {
                if(form.Text == "ScriptViewer2")
                {
                    MessageBox.Show("追加起動は1台のみしかできません。");
                    return;
                }
            }
            
            TSVDataGetter tsvData = new TSVDataGetter();
            tsvData.GetInitFileData();
            var addedForm = new Form1(tsvData, false);
            addedForm.Show();
        }

    }
}
