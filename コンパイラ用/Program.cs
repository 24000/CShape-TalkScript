using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TalkScript.Viewer
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            TSVDataGetter tsvData = new TSVDataGetter();
            if (tsvData.GetInitFileData() == false)
            {
                return;
            }

            Application.Run(new Form1(tsvData));
           
        }
    }
}
