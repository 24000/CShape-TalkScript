using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TalkScript.Viewer.Controls;
using TalkScript.Viewer.PopUp;

namespace TalkScript.Viewer.ControlManage
{
    /// <summary>
    /// コントロール生成クラス
    /// </summary>
    public sealed class ControlFactory
    {
        private const int Interval = 10;
        private const string ComboName = "ScriptSelectCmb";
        private const string TextBoxName = "OpeningTxt";

        private Form1 _view;
        private TextBox mark;
        /// <summary>
        /// コンストラクタ。初期配置コントロールの作成。
        /// </summary>
        public ControlFactory(Form1 view)
        {
            _view = view;
        }
        
        /// <summary>
        /// 初期配置コントロール：オープニングトーク表示TextBox作成
        /// </summary>
        /// <param name="top"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public ScriptTextBox MakeOpeningTextBox(int top )
        {
            string talk = _view.TSVData.OpeningTalk;
            ScriptTextBox txt = new ScriptTextBox(_view, top, TextBoxName,talk);
            _view.Controls.Add(txt);
            return txt;
        }

        /// <summary>
        /// コンテキストメニュー作成
        /// </summary>
        /// <param name="isFirstForm"></param>
        /// <returns></returns>
        public MyContextMenu MakeContextMenu(bool isFirstForm)
        {
            MyContextMenu menu = new MyContextMenu(_view, isFirstForm);
            _view.ContextMenuStrip = menu;
            return menu;
        }

        /// <summary>
        /// 初期配置コントロール：スクリプト選択ComboBox作成
        /// </summary>
        /// <param name="top"></param>
        public void MakeScriptSelectComboBox(int top)
        {
            ComboBoxEX ScriptSelectCombo 
                = new ComboBoxEX(_view, _view.TSVData.ScriptTitles, top, ComboName);
            _view.Controls.Add(ScriptSelectCombo);
        }

        /// <summary>
        /// 次のグループのコントロール群を作成
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="groupNum"></param>
        /// <param name="branchNum"></param>
        public void MakeNextArea(Control ctrl,  int groupNum = 0, int branchNum = 0)
        {
            ControlPopUp(_view,groupNum);
            ScriptTextBox txt = MakeScriptTextBox(ctrl,groupNum);
            TextBox mark = MakeMark(txt, groupNum);
            int top = txt.Top + txt.Height ;

            int choiceCount = _view.TSVData.GetNextGroupChoiceCount(groupNum);
            if(choiceCount != 0)
            {
                MakeContinueArea(choiceCount,txt, groupNum, branchNum);
            }
            else
            {
                MakeEndArea(top, groupNum);
                try
                {

                }
                catch
                {

                }
            }
        }

        /// <summary>
        /// 継続エリアを作成
        /// </summary>
        /// <param name="choiceCount"></param>
        /// <param name="top"></param>
        /// <param name="groupNum"></param>
        /// <param name="branchNum"></param>
        private void MakeContinueArea( int choiceCount,TextBox txt, int groupNum = 0, int branchNum = 0)
        {
            string name = (groupNum + 1).ToString() + "\\1" + "\\tlp";
            int top = txt.Top + txt.Height + Interval;
            ChoiceTableLayoutPanel panel = MakeChoiceTableLayoutPanel(top, name, choiceCount);
            List<string[]> talkNames = _view.TSVData.GetNextGroupTalkNames(groupNum);
            MakeChoiceRadioButtons(panel, groupNum, talkNames, choiceCount);
            _view.Scrolling(groupNum, panel,txt);
        }

        /// <summary>
        /// 最終エリアを生成
        /// </summary>
        /// <param name="top"></param>
        /// <param name="groupNum"></param>
        /// <param name="branchNum"></param>
        private void MakeEndArea(int top, int groupNum = 0, int branchNum = 0)
        {
            ScriptTextBox txt = MakeEndingTextBox(top+Interval,groupNum);
            EndAreaFlowPanel panel = new EndAreaFlowPanel(groupNum);
            _view.Controls.Add(panel);
            LogButton logButton = new LogButton(_view, groupNum);
            panel.Controls.Add(logButton);
            ClearButton clearButton = new ClearButton( _view,groupNum, logButton);
            panel.Controls.Add(clearButton);
            Control[] c = _view.Controls.Find(groupNum.ToString() + "\\1\\lbl", true);
            if (c.Count() != 0)
            {
                _view.Controls.Remove(c[0]);
                _view.ScrollControlIntoView(panel);
            }
            
        }

        /// <summary>
        /// PopUpの操作
        /// </summary>
        /// <param name="_view"></param>
        /// <param name="groupNum"></param>
        private void ControlPopUp(Form1 _view,int groupNum)
        {
            PopUpControler popUpCtrler = new PopUpControler(_view, groupNum);
            popUpCtrler.ClosePreviousePopUpIfShow();
            if (popUpCtrler.IsNeedNewPopUp())
            {
                PopUpForm popUp = popUpCtrler.MakePopUp();
               
            }
        }

        /// <summary>
        /// スクリプト表示用TextBox作成
        /// </summary>
        /// <param name="top"></param>
        /// <param name="name"></param>
        /// <param name="groupNum"></param>
        /// <returns></returns>
        private ScriptTextBox MakeScriptTextBox(Control ctrl, int groupNum = 0)
        {
            
            int top = ctrl.Top + ctrl.Height + Interval;
            string name = (groupNum + 1).ToString() + "\\1" + "\\txt";
            string talk = _view.TSVData.GetTalk(groupNum);
            ScriptTextBox txt =new ScriptTextBox(_view,top, name,talk);
            _view.Controls.Add(txt);
            return txt;
        }

        /// <summary>
        /// 現在のGroupのテキストボックスにつけるマークを作成
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="groupNum"></param>
        /// <returns></returns>
        private TextBox MakeMark(ScriptTextBox txt,int groupNum)
        {         
            _view.Controls.Remove(mark);
            string name = (groupNum + 1).ToString() + "\\1" + "\\mrk";
            mark = new TextBox();
            mark.Name = name;
            mark.Top = txt.Top;
            mark.Width = 10;
            mark.Height = (int)txt.Height;
            //mark.BorderStyle = BorderStyle.None;
            mark.BackColor = Color.Red;
            mark.Left = 0;
            _view.Controls.Add(mark);
            return mark;
        }

        /// <summary>
        /// 選択肢ラジオボタン配置用TableLayoutPanel作成
        /// </summary>
        /// <param name="top"></param>
        /// <param name="name"></param>
        /// <param name="choiceCount">次のグループの選択肢の数</param>
        /// <returns></returns>
        private ChoiceTableLayoutPanel MakeChoiceTableLayoutPanel
                                                    (int top,string name,int choiceCount)
        {
            var panel = new ChoiceTableLayoutPanel(_view, top, name, choiceCount);
            _view.Controls.Add(panel);
            return panel;
        }

        /// <summary>
        /// 選択肢ラジオボタン作成
        /// </summary>
        /// <param name="panel"></param>
        /// <param name="groupNum"></param>
        /// <param name="talkNames"></param>
        /// <param name="choiceCount"></param>
        private void MakeChoiceRadioButtons(ChoiceTableLayoutPanel panel, 
                                                                int groupNum, List<string[]> talkNames,
                                                                int choiceCount)
        {
            for (int i = 0; i <= choiceCount-1; i++)
            {
                RadioButtonEX radio 
                    =new RadioButtonEX(_view,groupNum,i+1,panel,talkNames[i]);
                if (i <= 3)
                {
                    panel.Controls.Add(radio, i, 0);
                }
                else
                {
                    panel.Controls.Add(radio, i - 4, 1);
                }
            }
        }

        /// <summary>
        /// エンディングトーク表示TextBox作成
        /// </summary>
        /// <param name="top"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private ScriptTextBox MakeEndingTextBox(int top,int groupNum)
        {
            string name = (groupNum + 1).ToString() + "\\1\\txt";
            string talk = _view.TSVData.EndingTalk;
            ScriptTextBox txt= new ScriptTextBox(_view, top, name,talk );
            _view.Controls.Add(txt);
            return txt;
        }

    }
}
