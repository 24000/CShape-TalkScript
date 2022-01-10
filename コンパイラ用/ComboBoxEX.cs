using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TalkScript.Viewer.Controls
{
    
    public sealed class ComboBoxEX:ComboBox
    {
        public string SelectedScript { set; get; }
        private Form1 _view;
        private string[] _scriptNames;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="view">フォーム</param>
        /// <param name="scriptName">選択されたスクリプト名</param>
        public ComboBoxEX(Form1 view, string[] scriptName,int top,string name)
        {
            _view = view;
            _scriptNames = scriptName;
            Items.AddRange(scriptName);
            AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            AutoCompleteSource = AutoCompleteSource.ListItems;
            Name = name;
            Top = top;
            Left = 6;
            Width = 300;
            Height = 80;
            Font = _view.MyFont;
            KeyPress += Combo_KeyPress;
            SelectedIndexChanged += Combo_SelectedIndexChaneged;
        }

        /// <summary>
        /// コンボボックスに対する文字直接入力に対応。
        /// Enterで入力完了とみなし、SelectedIndexを変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Combo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Enter)
            {
                return;
            }

            string scriptName = _scriptNames.ToList().Find(x => x == Text);
            if (scriptName != null)
            {
                SelectedIndex = FindString(scriptName);
            }
        }

        /// <summary>
        /// Viewの関連情報初期化⇒
        /// コンボボックスで選択されたトークスクリプトの内容を取得⇒
        /// 次の表示エリアの生成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Combo_SelectedIndexChaneged(object sender, EventArgs e)
        {
            if (this.SelectedIndex != -1)
            {
                _view.InitializeViewInfo();
                _view.CtrlDestroyer.DeleteAllAddedControl();

                SelectedScript = SelectedItem.ToString();
                if (_view.TSVData.GetScript(SelectedScript) == false)
                {
                    SelectedIndex = -1;
                    return;
                }
                _view.TSVData.SelectedTalks.Add(0, "Start");

                _view.CtrlFactory.MakeNextArea(this);
            }
        }
        
    }
}
