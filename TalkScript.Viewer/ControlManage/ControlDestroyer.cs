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
            List < Control > DeleteTarget = new List<Control>();

            foreach (Control  c in _view.Controls) {
                string[] tmpNameSplit = c.Name.Split('\\');
                if (Int32.TryParse(tmpNameSplit[0], out num) == false)
                {
                    continue;
                }

                if (c.GetType().Name == "ChoiceTableLayoutPanel"
                    ||c.GetType().Name == "EndAreaFlowPanel")
                {
                    for (int j = c.Controls.Count - 1; j >= 0; j--)
                    {
                        c.Controls.Remove(c.Controls[j]);
                    }
                    DeleteTarget.Add(c);
                }
                else
                {
                    DeleteTarget.Add(c);
                }
            }
            DeleteTarget.ForEach(x => _view.Controls.Remove(x));
        }

        /// <summary>
        ///ｸﾞﾙｰﾌﾟを戻って選択(例：４まで進んだが１まで戻って選択した場合等)した場合に
        ///選択したｸﾞﾙｰﾌﾟより先のｸﾞﾙｰﾌﾟのｺﾝﾄﾛｰﾙは不要となるため、不要なｺﾝﾄﾛｰﾙ削除
        ///※選択されたグループは消さない、選択されたグループ番号超のグループが削除対象
        /// </summary>
        public void DeleteNotNeedCtrls(int groupNum)
        {
            Control[] ctrls = _view.Controls.Find("99\\1\\flp", true);
            if (ctrls.Count() != 0)
            {
                MessageBox.Show("a");
            }
            int num;
            List<Control> DeleteTarget = new List<Control>();
            foreach ( Control c in _view.Controls)
            {
                string s = c.GetType().Name;
                string[] tmpNameSplit = c.Name.Split('\\');
                bool canParse = Int32.TryParse(tmpNameSplit[0], out num);
                if (canParse == false)
                {
                    continue;
                }

                if (num <= groupNum)
                {
                    continue;
                }

                if (c.GetType().Name == "ChoiceTableLayoutPanel"
                    || c.GetType().Name == "EndAreaFlowPanel")
                {
                    for (int j = c.Controls.Count - 1; j >= 0; j--)
                    {
                        c.Controls.Remove(c.Controls[j]);
                    }
                    DeleteTarget.Add(c);
                }
                else
                {
                    DeleteTarget.Add(c);
                }
            }
            
            DeleteTarget.ForEach(x => _view.Controls.Remove(x));
        }
            


    }
}
