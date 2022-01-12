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
        private Color TextBackColor = Color.White;

        public ScriptTextBox(Form _view, int top, string name,string talk)
        {
            Name = name;
            Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Top = top;
            Left = 10;
            MinimumSize = new Size(50, 30);
            //Height = ((int)(_view.Height * 0.3));
            Width = ((int)(_view.ClientSize.Width * 0.85));
            Font = _view.Font;
            ReadOnly = true;
            BackColor = TextBackColor;
            Multiline = true;
            WordWrap = true;
            Text = talk;
            AutoSize = false;
            ScrollBars = ScrollBars.Vertical;
            Height = GetTextBoxHeght(_view.Font);
        }

        public int GetTextBoxHeght(Font font)
        {
            int lineCount = 0;
            int firstChar = 0;
            int lineHight = 0;
            int textHeight = 0;
            do
            {
                firstChar =  GetFirstCharIndexFromLine(lineCount);
                if(firstChar == -1)
                {
                    break;
                }
                
                if(firstChar > Text.Length-1)
                {
                    break;
                }

                lineHight = TextRenderer.MeasureText(Text[firstChar].ToString(), font).Height;
                textHeight += lineHight;
                lineCount++;
            } while (true);

            textHeight += Margin.Vertical *2;
            return textHeight;
        }

    }
}
