using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace TalkScript.Viewer.Controls
{
    class LogButton:Button
    {
        Form1 _view;
        private const string ButtonCaption = "Log Copy";
        public LogButton(Form1 view ,int groupNum)
        {
            _view = view;
            string name = (groupNum + 1).ToString() + "\\1\\btn";
            Anchor =AnchorStyles.Top | AnchorStyles.Left|AnchorStyles.Right;
            AutoSize = true;
            Location = new System.Drawing.Point(3, 3);
            Name = name;
            Size = new System.Drawing.Size(116, 53);
            TabIndex = 0;
            Font = _view.MyFont;
            Text = ButtonCaption;
            UseVisualStyleBackColor = true;
            Click += LogCopy_Click;
            _view.Controls.Add(this);
        }

        private void LogCopy_Click(object sender, EventArgs e)
        {
            string logMsg = _view.Controls.Find("ScriptSelectCmb", true)[0].Text + "→";
            foreach (Control c  in _view.Controls)
            {
                if (c.GetType().Name == "ChoiceTableLayoutPanel")
                {
                    foreach(RadioButton rdo in c.Controls)
                    {
                        if (rdo.Checked)
                        {
                            logMsg += rdo.Text + "→";
                            break;
                        }
                    }
                }
            }

            logMsg = logMsg.TrimEnd('→') + "\n\n";

            if(_view.LogMsg == "")
            {
                _view.LogMsg = logMsg;
            }
            else if(logMsg != _view.LogMsg)
            {
                _view.LogMsg += logMsg;
            }

            Clipboard.SetText(_view.LogMsg);
            MessageBox.Show("受付内容を履歴として作成しました。(コピー済み。)");
        }
    }
}
