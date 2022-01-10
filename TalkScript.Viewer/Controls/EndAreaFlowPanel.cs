using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace TalkScript.Viewer.Controls
{
    class EndAreaFlowPanel:FlowLayoutPanel
    {
        public EndAreaFlowPanel(int GroupNum)
        {
            //Name = (GroupNum + 1).ToString() + "\\1\\flp";
            Name =  "99\\1\\flp";
            Dock = DockStyle.Bottom;
            //Location = new System.Drawing.Point(0, 476);
            Size = new System.Drawing.Size(678, 58);
        }
        
    }
}
