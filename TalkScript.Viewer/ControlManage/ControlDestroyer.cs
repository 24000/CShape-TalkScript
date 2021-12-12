using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TalkScript.Viewer.ControlManage
{
    public sealed class ControlDestroyer
    {
        private Form1 _view;
        public ControlDestroyer(Form1 view)
        {
            _view = view;
        }

        /// <summary>
        /// 追加で作成されたコントロールを全て削除
        /// （コントロールの名前を￥でSplitして[0]が”数値”のものが追加で作成されたもの）
        /// </summary>
        public void DeleteAllAddedControl()
        {
            int num;
            for (int i = _view.Controls.Count - 1; i >= 0; i--) {
                string[] tmpNameSplit = _view.Controls[i].Name.Split('\\');
                if (Int32.TryParse(tmpNameSplit[0], out num) == false)
                {
                    continue;
                }

                if (_view.Controls[i].GetType().Name == "ChoiceTableLayoutPanel"
                    || _view.Controls[i].GetType().Name == "EndAreaFlowPanel")
                {
                    for (int j = _view.Controls[i].Controls.Count - 1; j >= 0; j--)
                    {
                        _view.Controls[i].Controls.Remove(_view.Controls[i].Controls[j]);
                    }
                    _view.Controls.Remove(_view.Controls[i]);
                }
                else
                {
                    _view.Controls.Remove(_view.Controls[i]);
                }

            }
        }

        /// <summary>
        ///ｸﾞﾙｰﾌﾟを戻って選択(例：４まで進んだが１まで戻って選択した場合等)した場合に
        ///選択したｸﾞﾙｰﾌﾟより先のｸﾞﾙｰﾌﾟのｺﾝﾄﾛｰﾙは不要となるため、不要なｺﾝﾄﾛｰﾙ削除
        ///※選択されたグループは消さない、選択されたグループ番号超のグループが削除対象
        /// </summary>
        public void DeleteNotNeedCtrls(int groupNum)
        {
            int num;
            for (int i = _view.Controls.Count - 1;  i >= 0;  i--)
            {
                string s = _view.Controls[i].GetType().Name;
                string[] tmpNameSplit = _view.Controls[i].Name.Split('\\');
                bool canParse = Int32.TryParse(tmpNameSplit[0], out num);
                if (canParse == false)
                {
                    continue;
                }

                if (num <= groupNum)
                {
                    continue;
                }
                
                if (_view.Controls[i].GetType().Name == "ChoiceTableLayoutPanel"
                    || _view.Controls[i].GetType().Name == "EndAreaFlowPanel")
                {
                    for (int j = _view.Controls[i].Controls.Count - 1; j >= 0; j--)
                    {
                        _view.Controls[i].Controls.Remove(_view.Controls[i].Controls[j]);
                    }
                    _view.Controls.Remove(_view.Controls[i]);
                }
                else
                {
                    _view.Controls.Remove(_view.Controls[i]);
                }
            }
        }
            


    }
}
