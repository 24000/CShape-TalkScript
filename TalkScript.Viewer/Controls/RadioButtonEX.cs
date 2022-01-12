using System;
using System.Drawing;
using System.Windows.Forms;
using TalkScript.Viewer;

namespace TalkScript.Viewer.Controls
{
    /// <summary>
    /// スクリプト分岐の選択肢用オプションボタン
    /// 押された選択肢のトーク表示と次のエリアを作成する。
    /// </summary>
    public sealed class RadioButtonEX:RadioButton
    {
        const int OPB_TOP_POSITION_ROW1 = 3;
        const int OPB_TOP_POSITION_ROW2 = 40;
        const int OPB_LEFT = 6;
        const int OPB_HEIGHT = 30;

        internal int _groupNum { get; private set; }
        internal int _branchNum { get; private set; }
        internal string _talkName { get; private set; }
        internal string _caption { get; private set; }
        Form1 _view;
        ChoiceTableLayoutPanel _panel;
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="view"></param>
        /// <param name="groupNum">オプションボタンの所属グループ番号</param>
        /// <param name="branchNum">オプションボタンのグループ内番号</param>
        /// <param name="panel">オプションボタン配置グループボックス</param>
        /// <param name="talkName">対象トークのテキストボックス名</param>
        /// <param name="caption">表示する選択肢名</param>
        public RadioButtonEX(Form1 view,
                                           int groupNum ,int branchNum ,ChoiceTableLayoutPanel panel,
                                           string[] CapAndTalkName)
        {
            _view = view;
            _groupNum = groupNum+1;
            _branchNum = branchNum;
            this.Name = (groupNum + 1).ToString() + "\\" + _branchNum.ToString() + "\\Rdo";
            _panel = panel;
            _caption = CapAndTalkName[0];
            _talkName = CapAndTalkName[1];
            if (_caption.Length >= 7)
            {
                _caption.Insert(7, Environment.NewLine);
            }
            Height = 70;
            Width = 200;
            Anchor = AnchorStyles.Left | AnchorStyles.Top;
            Font = new Font("Meiryo UI", 9, FontStyle.Regular, GraphicsUnit.Point, 128);
            Text = _caption;
            Click += radioButton1_CheckedChanged;
        }

        /// <summary>
        /// ①現在いるグループ番号を更新
        /// ②もし前のグループに戻る選択の場合、不要コントロール削除と辞書(selecteTalk)調整
        /// ③押された選択肢のグループ番号と選択肢に対応するトーク名を辞書(selectedTalk)に登録
        /// ④次のグループを作成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            _view.UpdateCurrentGroup(_groupNum);

            if(_view.CurrentGroup <= _view.PreviousGroup)
            {
                _view.CtrlDestroyer.DeleteNotNeedCtrls(_groupNum);
                _view.TSVData.AdjustSelectedTalks(_groupNum);
            }

            _view.TSVData.SelectedTalks.Add(_groupNum, _talkName);
            _view.CtrlFactory.MakeNextArea( _panel,_groupNum, _branchNum); 
        }
    }

}
