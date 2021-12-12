using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TalkScript.Viewer.Controls
{
    public sealed class ScriptTextBox : TextBox
    {
        private const int TextHeight = 100;
        private Color TextBackColor = Color.White;
        private Font TextFont = new Font("Meiryo UI", 12, FontStyle.Regular, GraphicsUnit.Point, 128);

        public ScriptTextBox(Form1 _view, int top, string name,string script)
        {
            Name = name;
            Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Top = top;
            Left = 10;
            MinimumSize = new Size(50, 30);
            Height = ((int)(_view.Height * 0.3));
            Width = ((int)(_view.Width * 0.92));
            Font = TextFont;
            ReadOnly = true;
            BackColor = TextBackColor;
            Multiline = true;
            WordWrap = true;
            Text = script;
            AutoSize = true;
            ScrollBars = ScrollBars.Vertical;
        }
    }
}
