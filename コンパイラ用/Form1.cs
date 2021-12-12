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

namespace TalkScript.Viewer
{
    public partial class Form1 : Form
    {
        private ComboBox _fontSize;
        public ControlFactory CtrlFactory { get; private set; } 
        public ControlDestroyer CtrlDestroyer { get; private set; }

        public TSVDataGetter TSVData { get; private set; }
        public int CurrentGroup { set; get; }
        public int PreviousGroup { set; get; }
        public string LogMsg { set; get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Form1(TSVDataGetter tsvData)
        {
            InitializeComponent();
            AutoScrollMargin = new Size(50, 0);
            Top = 0;
            Left = 0;
            CtrlFactory = new ControlFactory(this);
            CtrlDestroyer = new ControlDestroyer(this);
            FontSizeCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            for (int i = 8; i < 28; i++)
            {
                FontSizeCombo.Items.Add(i);
            }
            _fontSize = FontSizeCombo;
            FontSizeCombo.SelectedItem = 12;

            TSVData = tsvData;
            TextBox txt = CtrlFactory.MakeOpeningTextBox(60, "OpeningTxt");
            CtrlFactory.MakeScriptSelectComboBox(txt.Top+txt.Height + 10);       
        }

        /// <summary>
        /// フォントサイズComboBoxのSelectedIndexChangeｄ用ハンドラー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FontSizeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is TextBox)
                {
                    ctrl.Font = new Font("Meiryo UI", Convert.ToSingle(_fontSize.SelectedItem),
                                                    FontStyle.Regular, GraphicsUnit.Point);
                }
            }
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
                CurrentGroup = groupNum;
            }
        }

        /// <summary>
        /// フォームをスクロールさせる。
        /// </summary>
        /// <param name="groupNum"></param>
        /// <param name="panel"></param>
        public void Scrolling(int groupNum, ChoiceTableLayoutPanel panel)
        {
            Label lbl = new Label();
            lbl.Name = (groupNum + 1).ToString() + "\\1\\lbl";
            lbl.Height = 40;
            lbl.Width = 1;
            lbl.Text = "a";
            lbl.Top = panel.Top + (int)(Height * 0.4) + 40;

            Controls.Add(lbl);

            ScrollingAnimation(lbl);
        }

        /// <summary>
        /// アニメーションでスクロールする。
        /// </summary>
        /// <param name="ctrl"></param>
        public void ScrollingAnimation(Control ctrl)
        {
            int StartPos = AutoScrollPosition.Y;
            ScrollControlIntoView(ctrl);
            int TargetPos = AutoScrollPosition.Y;
            AutoScrollPosition = new Point(0, -StartPos);
            for (int i = StartPos; i >= TargetPos; i -= 4)
            {
                if (i % 10 == 0)
                {
                    Refresh();
                }
                AutoScrollPosition = new Point(0, -i);
            }
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
