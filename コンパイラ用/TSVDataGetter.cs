using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TalkScript.Viewer
{
    public sealed class TSVDataGetter
    {
        const string InitFileName = "\\設定ファイル.txt";
        public string OpeningTalk { set; get; }
        public string EndingTalk { set; get; }
        public string ScriptFolderPath { set; get; }
        public string[] ScriptTitles { set; get; }
  
        //取得するスクリプト内容を保持（Key：Shapeの名前、Value：トーク内容（改行部が｜区切り）
        public Dictionary<string, string[]> Script { get; private set; } 
        //グループごとに選択されたトークを保持（Key:グループ番号、Value：Shape名
        public Dictionary<int, string> SelectedTalks { get; private set; }

        public TSVDataGetter()
        {
            Script = new Dictionary<string, string[]>();
            SelectedTalks = new Dictionary<int, string>();
        }

        /// <summary>
        /// 初期設定情報取得
        /// </summary>
        /// <returns></returns>
        public bool GetInitFileData()
        {
            string initFilePath = Application.StartupPath + InitFileName;
            if (File.Exists(initFilePath) == false)
            {
                MessageBox.Show("スクリプト管理シート.txtのファイルがまだ作成されていません。");
                return false;
            }

            using (StreamReader sr = new StreamReader
                                                        (initFilePath,
                                                         Encoding.GetEncoding("Shift_JIS")))
            {
                string line = sr.ReadToEnd();
                string[] contents = line.Split('\t');
                OpeningTalk = contents[0].Replace('|', '\n');
                EndingTalk = contents[1].Replace('|', '\n');
                ScriptFolderPath = contents[2].Trim('"');
                contents[3] = contents[3].Replace("\r", "").Replace("\n", "");
                ScriptTitles = contents[3].Split('|');
            }
            return true;
        }

        /// <summary>
        /// スクリプトファイル(TSV)の内容を取得し、開始トークを辞書へ格納
        /// </summary>
        public bool GetScript(string selectedScriptName)
        {
            string scriptFileFullPath = ScriptFolderPath + "\\" + selectedScriptName + ".txt";
            if (File.Exists(scriptFileFullPath)==false)
            {
                MessageBox.Show("選択された項目のスクリプトが存在しません。");
                return false;
            }

            string line = null;
            using (StreamReader sr = new StreamReader
                                                        (scriptFileFullPath,
                                                         Encoding.GetEncoding("Shift_JIS")))
            {
                string[] contents;
                while (sr.EndOfStream == false)
                {
                    line = sr.ReadLine();
                    if (line == "" ){break;}

                    contents = line.Split('\t');
                    //contents[0]にトークのテキストボックス名が入っている
                    Script.Add(contents[0], contents);
                }
            }

            if(line == "")
            {
                MessageBox.Show("選択された項目のスクリプトはまだ内容が作成されていません。");
                return false;
            }
            else
            {
                return true;
            }

        }

        /// <summary>
        /// 次グループの選択肢がいくつあるのかをカウントして返す。
        /// </summary>
        /// <param name="groupNum"></param>
        /// <returns></returns>
        public int GetNextGroupChoiceCount(int groupNum)
        {
            int count = 0;
            if (Script[SelectedTalks[groupNum]][2]  != "")
            {
                for (int i = 2; i <= 9; i++)
                {
                    if(Script[SelectedTalks[groupNum]][i] != "")
                    {
                        count++;
                    }
                       
                }
                return  count;
            }
            else
            {
                return 0;
            }
                
        }

        internal List<string[]> GetNextGroupTalkNames(int groupNum)
        {
            var capAndTalkNames = new List<string[]>();
            for (int i = 2; i <= 9; i++)
            {
                if (Script[SelectedTalks[groupNum]][i] != "")
                {
                    string[] strAry= Script[SelectedTalks[groupNum]][i].Split('|');
                    capAndTalkNames.Add(strAry);
                }
            }
            return capAndTalkNames;
        }

        /// <summary>
        /// 押されたボタンに対応するトーク名の辞書内容調整
        /// トークは同じグループに表示されるものであるため、自グループ「以上」の削除必要
        ///コレクション内で残したいIndex = 0 ～ グループNo 未満のグループ
        ///コレクション内で削除が必要なIndex = グループNo 以上のグループ
        ///※自グループのトーク名はこのメソッドのあと取得しなおしされる。
        /// </summary>
        /// <param name="groupNum"></param>
        public void AdjustSelectedTalks(int groupNum)
        {
            for (int i = SelectedTalks.Count - 1; i >= groupNum; i--)
            {
                SelectedTalks.Remove(i);
            }
        }

        /// <summary>
        ///  追加したTextBoxへｽｸﾘﾌﾟﾄを表示
        /// </summary>
        //public string SetScriptToTextBox(int groupNum)
        //{
        //    //return script(shpNames(groupNum))(1), "|", vbLf)
        //}


    }
}
