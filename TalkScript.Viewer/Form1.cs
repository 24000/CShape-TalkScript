using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TalkScript.Viewer.Controls;
using TalkScript.Viewer.ControlManage;
using TalkScript.Viewer.PopUp;

namespace TalkScript.Viewer
{
    public partial class Form1 : Form
    {
        private const string ViewerName1 = "ScriptViewer";
        private const string ViewerName2 = "ScriptViewer2";

        public ControlFactory CtrlFactory { get; private set; } 
        public ControlDestroyer CtrlDestroyer { get; private set; }

        public TSVDataGetter TSVData { get; private set; }
        public int CurrentGroup { get; set; }
        public int PreviousGroup { get; set; }
        public PopUpForm PopUpF { get; set; }
        public string LogMsg { get; set; }
        public Font MyFont { get; set; }
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Form1(TSVDataGetter tsvData,bool isFirstForm)
        {
            InitializeComponent();
            MyFont = new Font("Meiryo UI", 12, FontStyle.Regular, GraphicsUnit.Point, 128);
            CtrlFactory = new ControlFactory(this);
            CtrlDestroyer = new ControlDestroyer(this);

            if (isFirstForm)
            {
                Text = ViewerName1;
            }
            else
            {
                Text = ViewerName2;
                StartPosition = FormStartPosition.CenterParent;
                BackColor = Color.Gainsboro;
            }
            MyContextMenu menu = CtrlFactory.MakeContextMenu(isFirstForm);

            TSVData = tsvData;
            ScriptTextBox txt =CtrlFactory.MakeOpeningTextBox(40);
            CtrlFactory.MakeScriptSelectComboBox(txt.Top+txt.Height + 10);
        }
   
        /// <summary>
        /// 現在選択されているGroupを更新する。
        /// </summary>
        /// <param name="groupNum"></param>
        public void UpdateCurrentGroup(int groupNum)
        {
            if( CurrentGroup!= 0)
            {
                PreviousGroup = CurrentGroup;
            }
            CurrentGroup = groupNum;
        }

        /// <summary>
        /// フォームをスクロールさせる。
        /// </summary>
        /// <param name="groupNum"></param>
        /// <param name="ctrl">コンボボックスかChoiceTableLayoutPanel</param>
        public void Scrolling(int groupNum, Control ctrl, TextBox txt)///ChoiceTableLayoutPanel panel,TextBox txt)
        {
            Label lbl = new Label();
            lbl.Name = (groupNum + 1).ToString() + "\\1\\lbl";
            lbl.Height = 1;
            lbl.Width = 1;
            lbl.Text = groupNum.ToString();
            //lbl.Top = txt.Location.Y + Height;
            lbl.Top = ctrl.Location.Y  -10 + ClientSize.Height;
            Controls.Add(lbl);

            ScrollingAnimation( ctrl, txt);
        }

        /// <summary>
        /// アニメーションでスクロールする。
        /// </summary>
        /// <param name="ctrl"></param>
        public void ScrollingAnimation(Control ctrl,Control txt = null)
        {
            int StartPos = AutoScrollPosition.Y;
            int i = -StartPos ;
     
                while(ctrl.Location.Y > 10)
                {
                    i += 10;
                    if (i % 20 == 0)
                    {
                        Refresh();
                    }
                    AutoScrollPosition = new Point(0, i );
                }
                //ScrollControlIntoView(ctrl);
                //int TargetPos = AutoScrollPosition.Y;
                //TargetPos -= txt.Location.Y - 90;
                //AutoScrollPosition = new Point(0, -StartPos);
                //for (int i = StartPos; i >= TargetPos; i -= 4)
                //{
                //    if (i % 10 == 0)
                //    {
                //        Refresh();
                //    }
                //    AutoScrollPosition = new Point(0, -i);
                //}
            //}
            //else
            //{
            //    if(panel != null)
            //    {
            //        ScrollControlIntoView(panel);
            //    }
            //}
        }

        /// <summary>
        /// コンボボックスでスクリプト選択時またはクリアの際に関連情報初期化
        /// </summary>
        public void InitializeViewInfo()
        {
            CurrentGroup = 0;
            PreviousGroup = 0;
            TSVData.Script.Clear();
            TSVData.SelectedTalks.Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
