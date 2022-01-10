using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TalkScript.Viewer.Controls
{
    class ClearButton:Button
    {
        private const string ButtonCaption = "Clear";
        private Form1 _view;
        private LogButton _logButton;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="view"></param>
        /// <param name="groupNum"></param>
        /// <param name="logButton"></param>
        public ClearButton(Form1 view, int groupNum,LogButton logButton)
        {
            _view = view;
            _logButton = logButton;
            Name = (groupNum + 1).ToString() + "\\2\\btn";
            Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            AutoSize = true;
            Location = new System.Drawing.Point(3, 3);
            Size = new System.Drawing.Size(116, 53);
            TabIndex = 0;
            Font = _view.MyFont;
            Text = ButtonCaption;
            UseVisualStyleBackColor = true;
            Click += Clear_Click;
        }

        /// <summary>
        /// クリックイベント：フォームの初期化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Clear_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("選択内容をクリアしますか？",
                "確認", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
            {
                return; 
            }

            if(_view.LogMsg != "")
            {
                result = MessageBox.Show("同じお客様の受付が続きますか？",
                    "確認", MessageBoxButtons.YesNo);
                if( result == DialogResult.No)
                {
                    MessageBox.Show("履歴は削除します。");
                    _view.LogMsg = "";
                }
                else
                {
                    MessageBox.Show("履歴は続けて記録します。");
                }
            }

            _view.InitializeViewInfo();
            _view.CtrlDestroyer.DeleteAllAddedControl();
        }
    }
}
