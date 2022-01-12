using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TalkScript.Viewer.Controls
{
    public sealed class ChoiceTableLayoutPanel:TableLayoutPanel
    {
        const int ColumnCounts = 4;
        public ChoiceTableLayoutPanel(Form1 _view,int top,string name,int choiceCount)
        {
            
            for (int i = 1; i <= ColumnCounts; i++)
            {
                ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            }

            if (choiceCount <= 4)
            {
                RowCount = 0;
                RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
                Size = new Size(((int)(_view.Width * 0.92)), 60);
            }
            else
            {
                RowCount = 1;
                RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
                RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
                Size = new Size(((int)(_view.Width * 0.92)), 80);
            }
            Location = new Point(10, top);
            Name = name;
            CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
            //AutoScroll = true;
        }
    }
        
}
