using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace TalkScript.Viewer.Controls
{
    class ReferenceLink:LinkLabel
    {
        private string[] _linkInfo;
        private string _linkAddress ="";
        private string _sheetName ="";
        private string _cellAddress ="";
        public ReferenceLink(int top ,string linkInfo,string name)
        {
            SetLinkDestination(linkInfo);

            AutoSize = true;
            Location = new System.Drawing.Point(10, top);
            Name = name;
            Size = new System.Drawing.Size(130, 30);
            Text = _linkInfo[0];


            LinkClicked += linkLabel1_LinkClicked;
        }

        private void SetLinkDestination(string linkInfo)
        {
            linkInfo = linkInfo.Replace('＞', '>');
            if (linkInfo.Contains('>'))
            {
                _linkInfo = linkInfo.Split('>');
                _linkAddress = _linkInfo[1].Replace("\"","").Replace("”","");
                if (_linkInfo.Count() == 3)
                {
                    _linkInfo[2] = _linkInfo[2].Replace('！', '!');
                    if (_linkInfo[2].Contains('!'))
                    {
                        _sheetName = _linkInfo[2].Split('!')[0];
                        _cellAddress = _linkInfo[2].Split('!')[1];
                    }
                }
            }
            else
            {
                _linkAddress = "";
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_linkAddress == "")
            {
                MessageBox.Show("リンク先のアドレスが正しくありません。");
                return;
            }

            if (_linkAddress.Contains("http"))
            {
                System.Diagnostics.Process.Start(_linkAddress);
                return;
            }

            if (_linkAddress.Contains("xlsx") || _linkAddress.Contains("xlsm"))
            {
                if (File.Exists(_linkAddress)==false)
                {
                    MessageBox.Show("リンク先のアドレスが正しくありません。");
                    return;
                }

                if (_sheetName == "")
                {
                    System.Diagnostics.Process.Start(_linkAddress);
                    return;
                }
                else
                {
                    OpenExcelFile();
                    return;
                }
            }

            if (File.Exists(_linkAddress) == false)
            {
                MessageBox.Show("リンク先のアドレスが正しくありません。");
                return;
            }
            else
            {
                System.Diagnostics.Process.Start(_linkAddress);
                return;
            } 
        }


        private void OpenExcelFile()
        {
            dynamic xlApp = null;
            dynamic xlBooks = null;
            dynamic xlBook = null;
            dynamic xlSheets = null;
            dynamic xlSheet = null;
            dynamic xlRange = null;
            bool isExistSheet = false;
            try
            {
                xlApp = Activator.CreateInstance(Type.GetTypeFromProgID("Excel.Application"));
                try
                {
                    xlBooks = xlApp.Workbooks;
                    try
                    {
                        xlBook = xlBooks.open(_linkAddress);
                        xlApp.Visible = true;
                        try
                        {

                            xlSheets = xlBook.Worksheets;
                            foreach(dynamic ws in xlSheets)
                            {
                                if (ws.Name == _sheetName)
                                {
                                    isExistSheet = true;
                                    try
                                    {
                                        xlSheet = xlSheets[_sheetName];
                                        xlSheet.Select();
                                        if (xlApp.Evaluate("ISREF(" + _cellAddress + ")"))
                                        {
                                            try
                                            {
                                                xlRange = xlSheet.Range[_cellAddress];
                                                xlRange.Show();
                                                xlRange.Select();
                                            }
                                            finally
                                            {
                                                Marshal.ReleaseComObject(xlRange);
                                            }
                                        }
                                    }
                                    finally
                                    {
                                        Marshal.ReleaseComObject(xlSheet);
                                    }
                                }
                            }
                            if(isExistSheet== false)
                            {
                                MessageBox.Show("指定されたシート名のシートが存在しないため、\n" +
                                                            "ファイルのみ開きました。");
                            }
                        }
                        finally
                        {
                            Marshal.ReleaseComObject(xlSheets);
                        }
                    }
                    finally
                    {
                        Marshal.ReleaseComObject(xlBook);
                    }
                }
                finally
                {
                    Marshal.ReleaseComObject(xlBooks);
                }
            }
            finally
            {
                Marshal.ReleaseComObject(xlApp);
            }              
        }
    }
}
