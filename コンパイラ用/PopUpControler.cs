using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TalkScript.Viewer.Controls;

namespace TalkScript.Viewer.PopUp
{
    class PopUpControler
    {
        private Form1 _view;
        private int _groupNum;
        private List<string> _warning;
        private List<string> _linkInfos;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="view"></param>
        /// <param name="groupNum"></param>
        public PopUpControler(Form1 view,int groupNum)
        {
            _view = view;
            _groupNum = groupNum;
            _warning = _view.TSVData.GetWarning(_groupNum);
            _linkInfos = _view.TSVData.GetLinkInfos(_groupNum);
        }
        
        /// <summary>
        /// ポップアップ消去
        /// </summary>
        public void ClosePreviousePopUpIfShow()
        {
            if (_view.PopUpF != null)
            {
                _view.PopUpF.Close();
            }
        }

        /// <summary>
        /// ポップアップの表示が必要か判別
        /// </summary>
        /// <returns></returns>
        public bool IsNeedNewPopUp()
        {
            if(_warning.Count+_linkInfos.Count > 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// ポップアップの作成と表示
        /// </summary>
        /// <returns></returns>
        public PopUpForm MakePopUp()
        {
            var popUp =  new PopUpForm();
            popUp.AutoSize = true;
            popUp.Top = _view.Top;
            popUp.Left = _view.Left + _view.Width;
            popUp.Show(_view);
            _view.PopUpF = popUp;

            int top = 10;
            if (_warning.Count > 0)
            {
                ScriptTextBox txt = new ScriptTextBox(popUp, top, "WarningTxt", _warning[0]);
                txt.Width -= 15;
                popUp.Controls.Add(txt);
                top = txt.Top + txt.Height + 10;
            }

            {
                if (_linkInfos.Count > 0)
                {
                    for(int i = 0; i < _linkInfos.Count; i++)
                    {
                        string name = _groupNum.ToString() + "\\" + i.ToString() + "\\lbl";
                        var lnkLabel = new ReferenceLink(top, _linkInfos[i],name);
                        popUp.Controls.Add(lnkLabel);
                        top = lnkLabel.Top + lnkLabel.Height + 10;
                    }
                }
            }

            Label adjustHight = new Label();
            adjustHight.Top = top;
            adjustHight.Height = 10;
            adjustHight.Width = 0;
            popUp.Controls.Add(adjustHight);
            return popUp;
        }


    }
}
